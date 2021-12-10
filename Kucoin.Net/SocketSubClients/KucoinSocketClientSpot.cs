using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Interfaces;
using Kucoin.Net.Objects;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Kucoin.Net.Converters;
using Kucoin.Net.Objects.Futures.Socket;
using Kucoin.Net.Objects.Socket;
using Kucoin.Net.Objects.Spot.Socket;
using CryptoExchange.Net.Interfaces;
using Kucoin.Net.SubClients;

namespace Kucoin.Net.SocketSubClients
{
    /// <summary>
    /// Spot subscriptions
    /// </summary>
    public class KucoinSocketClientSpot: SocketClient, IKucoinSocketClientSpot
    {
        internal KucoinSocketClientSpot(KucoinSocketClientOptions options): base("Kucoin[Spot]", options, options.ApiCredentials == null ? null : new KucoinAuthenticationProvider(options.ApiCredentials))
        {
            MaxSocketConnections = 10;

            SendPeriodic(TimeSpan.FromSeconds(30), (connection) => new KucoinPing()
            {
                Id = Math.Round((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds).ToString(CultureInfo.InvariantCulture),
                Type = "ping"
            });

            AddGenericHandler("Ping", (messageEvent) => { });
            AddGenericHandler("Welcome", (messageEvent) => { });
        }

        #region public
        /// <summary>
        /// Subscribe to updates for a symbol ticker
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamTick>> onData) => SubscribeToTickerUpdatesAsync(new[] { symbol }, onData);

        /// <summary>
        /// Subscribe to updates for a symbol ticker
        /// </summary>
        /// <param name="symbols">The symbols to subscribe to</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamTick>> onData)
        {
            symbols.ValidateNotNull(nameof(symbols));
            foreach (var symbol in symbols)
                symbol.ValidateKucoinSymbol();

            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                var data = GetData<KucoinStreamTick>(tokenData);
                if (data == null)
                    return;

                data.Symbol = TryGetSymbolFromTopic(tokenData)!;
                InvokeHandler(tokenData.As(data, data.Symbol), onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", "/market/ticker:" + string.Join(",", symbols), false);
            return await SubscribeAsync(request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to updates for all symbol tickers
        /// </summary>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllTickerUpdatesAsync(Action<DataEvent<KucoinStreamTick>> onData)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                var data = GetData<KucoinStreamTick>(tokenData);
                if (data == null)
                    return;

                data.Symbol = (string)tokenData.Data["subject"]!;
                InvokeHandler(tokenData.As(data, data.Symbol), onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", "/market/ticker:all", false);
            return await SubscribeAsync(request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to updates for symbol or market snapshots
        /// </summary>
        /// <param name="symbolOrMarket">The symbol (ie KCS-BTC) or market (ie BTC) to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public Task<CallResult<UpdateSubscription>> SubscribeToSnapshotUpdatesAsync(string symbolOrMarket,
            Action<DataEvent<KucoinStreamSnapshot>> onData)
            => SubscribeToSnapshotUpdatesAsync(new[] { symbolOrMarket }, onData);

        /// <summary>
        /// Subscribe to updates for symbol or market snapshots
        /// </summary>
        /// <param name="symbolOrMarkets">The symbols (ie KCS-BTC) or markets (ie BTC) to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToSnapshotUpdatesAsync(
            IEnumerable<string> symbolOrMarkets, Action<DataEvent<KucoinStreamSnapshot>> onData)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                var data = GetData<KucoinStreamSnapshotWrapper>(tokenData)?.Data;
                if(data == null)
                {
                    log.Write(LogLevel.Warning, "Failed to process snapshot update");
                    return;
                }

                InvokeHandler(tokenData.As(data, data?.Symbol), onData!);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", "/market/snapshot:" + string.Join(",", symbolOrMarkets), false);
            return await SubscribeAsync(request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to aggregated order book updates
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public Task<CallResult<UpdateSubscription>> SubscribeToAggregatedOrderBookUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamOrderBook>> onData) => SubscribeToAggregatedOrderBookUpdatesAsync(new[] { symbol }, onData);

        /// <summary>
        /// Subscribe to aggregated order book updates
        /// </summary>
        /// <param name="symbols">The symbols to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToAggregatedOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamOrderBook>> onData)
        {
            symbols.ValidateNotNull(nameof(symbols));
            foreach (var symbol in symbols)
                symbol.ValidateKucoinSymbol();

            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                var data = GetData<KucoinStreamOrderBook>(tokenData);
                InvokeHandler(tokenData.As(data, data.Symbol), onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", "/market/level2:" + string.Join(",", symbols), false);
            return await SubscribeAsync(request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to trade updates
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamMatch>> onData)
        {
            symbol.ValidateKucoinSymbol();

            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                InvokeHandler(tokenData.As(GetData<KucoinStreamMatch>(tokenData), symbol), onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", "/market/match:" + symbol, false);
            return await SubscribeAsync(request, null, false, innerHandler).ConfigureAwait(false);
        }


        /// <summary>
        /// Subscribe to kline updates
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="interval">Interval of the klines</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KucoinKlineInterval interval, Action<DataEvent<KucoinStreamCandle>> onData)
        {
            symbol.ValidateKucoinSymbol();

            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                InvokeHandler(tokenData.As(GetData<KucoinStreamCandle>(tokenData), symbol), onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", $"/market/candles:{symbol}_{JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false))}", false);
            return await SubscribeAsync(request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to full order book updates
        /// </summary>
        /// <param name="symbol">The symbol to subscribe</param>
        /// <param name="limit">The amount of levels to receive, either 5 or 50</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int limit,
            Action<DataEvent<KucoinStreamOrderBookChanged>> onData) =>
            SubscribeToOrderBookUpdatesAsync(new[] { symbol }, limit, onData);

        /// <summary>
        /// Subscribe to full order book updates
        /// </summary>
        /// <param name="symbols">The symbols to subscribe</param>
        /// <param name="limit">The amount of levels to receive, either 5 or 50</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int limit, Action<DataEvent<KucoinStreamOrderBookChanged>> onData)
        {
            foreach (var symbol in symbols)
                symbol.ValidateKucoinSymbol();
            limit.ValidateIntValues(nameof(limit), 5, 50);

            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                var book = GetData<KucoinStreamOrderBookChanged>(tokenData);                
                InvokeHandler(tokenData.As(book, TryGetSymbolFromTopic(tokenData)), onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", $"/spotMarket/level2Depth{limit}:" + string.Join(",", symbols), false);
            return await SubscribeAsync(request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to index price updates
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToIndexPriceUpdates(string symbol, Action<DataEvent<KucoinStreamIndicatorPrice>> onData) => SubscribeToIndexPriceUpdates(new string[] { symbol }, onData);

        /// <summary>
        /// Subscribe to index price updates
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamIndicatorPrice>> onData) => SubscribeToIndexPriceUpdatesAsync(new string[] { symbol }, onData);

        /// <summary>
        /// Subscribe to index price updates
        /// </summary>
        /// <param name="symbols">Symbols to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToIndexPriceUpdates(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamIndicatorPrice>> onData) => SubscribeToIndexPriceUpdatesAsync(symbols, onData).Result;

        /// <summary>
        /// Subscribe to index price updates
        /// </summary>
        /// <param name="symbols">Symbols to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamIndicatorPrice>> onData)
        {
            foreach (var symbol in symbols)
                symbol.ValidateKucoinSymbol();

            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                var data = GetData<KucoinStreamIndicatorPrice>(tokenData);
                InvokeHandler(tokenData.As(data, data.Symbol), onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", $"/indicator/index:" + string.Join(",", symbols), false);
            return await SubscribeAsync(request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to mark price updates
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToMarkPriceUpdates(string symbol, Action<DataEvent<KucoinStreamIndicatorPrice>> onData) => SubscribeToMarkPriceUpdates(new string[] { symbol }, onData);

        /// <summary>
        /// Subscribe to mark price updates
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamIndicatorPrice>> onData) => SubscribeToMarkPriceUpdatesAsync(new string[] { symbol }, onData);

        /// <summary>
        /// Subscribe to mark price updates
        /// </summary>
        /// <param name="symbols">Symbols to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToMarkPriceUpdates(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamIndicatorPrice>> onData) => SubscribeToMarkPriceUpdatesAsync(symbols, onData).Result;

        /// <summary>
        /// Subscribe to mark price updates
        /// </summary>
        /// <param name="symbols">Currency to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamIndicatorPrice>> onData)
        {
            foreach (var symbol in symbols)
                symbol.ValidateKucoinSymbol();

            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                var data = GetData<KucoinStreamIndicatorPrice>(tokenData);
                InvokeHandler(tokenData.As(data, data.Symbol), onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", $"/indicator/markPrice:" + string.Join(",", symbols), false);
            return await SubscribeAsync(request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to funding book updates
        /// </summary>
        /// <param name="currency">Currency to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToFundingBookUpdates(string currency, Action<DataEvent<KucoinStreamFundingBookUpdate>> onData) => SubscribeToFundingBookUpdates(new string[] { currency }, onData);

        /// <summary>
        /// Subscribe to funding book updates
        /// </summary>
        /// <param name="currency">Currencies to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public Task<CallResult<UpdateSubscription>> SubscribeToFundingBookUpdatesAsync(string currency, Action<DataEvent<KucoinStreamFundingBookUpdate>> onData) => SubscribeToFundingBookUpdatesAsync(new string[] { currency }, onData);

        /// <summary>
        /// Subscribe to funding book updates
        /// </summary>
        /// <param name="currencies">Symbols to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToFundingBookUpdates(IEnumerable<string> currencies, Action<DataEvent<KucoinStreamFundingBookUpdate>> onData) => SubscribeToFundingBookUpdatesAsync(currencies, onData).Result;

        /// <summary>
        /// Subscribe to funding book updates
        /// </summary>
        /// <param name="currencies">Currencies to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToFundingBookUpdatesAsync(IEnumerable<string> currencies, Action<DataEvent<KucoinStreamFundingBookUpdate>> onData)
        {
            foreach (var currency in currencies)
                currency.ValidateNotNull(currency);

            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                var data = GetData<KucoinStreamFundingBookUpdate>(tokenData);
                InvokeHandler(tokenData.As(data, data.Currency), onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", $"/margin/fundingBook:" + string.Join(",", currencies), false);
            return await SubscribeAsync(request, null, false, innerHandler).ConfigureAwait(false);
        }



        /// <summary>
        /// <para>Subscribe to match engine updates. There are different update types with classes derived from <see cref="KucoinStreamOrderBaseUpdate" /></para>
        /// <para><see cref="KucoinStreamMatchEngineUpdate" />: A valid order is received by the matching engine</para>
        /// <para><see cref="KucoinStreamMatchEngineOpenUpdate" />: A limit order is opened on the order book</para>
        /// <para><see cref="KucoinStreamMatchEngineDoneUpdate" />: An order is no longer on the order book</para>
        /// <para><see cref="KucoinStreamMatchEngineMatchUpdate" />: An order is matched with another order</para>
        /// <para><see cref="KucoinStreamMatchEngineChangeUpdate" />: An order is changed (decreased) in size</para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public Task<CallResult<UpdateSubscription>> SubscribeToMatchEngineUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamMatchEngineUpdate>> onData) => SubscribeToMatchEngineUpdatesAsync(new[] { symbol }, onData);

        /// <summary>
        /// <para>Subscribe to match engine updates. There are different update types with classes derived from <see cref="KucoinStreamOrderBaseUpdate" /></para>
        /// <para><see cref="KucoinStreamMatchEngineUpdate" />: A valid order is received by the matching engine</para>
        /// <para><see cref="KucoinStreamMatchEngineOpenUpdate" />: A limit order is opened on the order book</para>
        /// <para><see cref="KucoinStreamMatchEngineDoneUpdate" />: An order is no longer on the order book</para>
        /// <para><see cref="KucoinStreamMatchEngineMatchUpdate" />: An order is matched with another order</para>
        /// <para><see cref="KucoinStreamMatchEngineChangeUpdate" />: An order is changed (decreased) in size</para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToMatchEngineUpdatesAsync(IEnumerable<string> symbols,
            Action<DataEvent<KucoinStreamMatchEngineUpdate>> onData)
        {
            symbols.ValidateNotNull(nameof(symbols));
            foreach (var symbol in symbols)
                symbol.ValidateKucoinSymbol();

            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                KucoinStreamMatchEngineUpdate? data = null;
                var subject = tokenData.Data["subject"]?.ToString();
                switch (subject)
                {
                    case "received":
                        data = GetData<KucoinStreamMatchEngineUpdate>(tokenData);
                        break;
                    case "open":
                        data = GetData<KucoinStreamMatchEngineOpenUpdate>(tokenData);
                        break;
                    case "done":
                        data = GetData<KucoinStreamMatchEngineDoneUpdate>(tokenData);
                        break;
                    case "match":
                        data = GetData<KucoinStreamMatchEngineMatchUpdate>(tokenData);
                        break;
                    case "update":
                        data = GetData<KucoinStreamMatchEngineChangeUpdate>(tokenData);
                        break;
                    default:
                        log.Write(LogLevel.Warning, "Unknown match engine update type: " + subject);
                        return;
                }

                InvokeHandler(tokenData.As<KucoinStreamMatchEngineUpdate>(data, data.Symbol), onData!);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", "/spotMarket/level3:" + string.Join(",", symbols), false);
            return await SubscribeAsync(request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to order updates for your own orders 
        /// </summary>
        /// <param name="onOrderData">Data handler for order updates</param>
        /// <param name="onTradeData">Data handler for trade updates</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<DataEvent<KucoinStreamOrderBaseUpdate>> onOrderData,
            Action<DataEvent<KucoinStreamOrderMatchUpdate>> onTradeData)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                
                if(tokenData.Data["data"] == null || tokenData.Data["data"]!["type"] == null)
                {
                    log.Write(LogLevel.Warning, "Order update without type value");
                    return;
                }    


                var type = tokenData.Data["data"]!["type"]!.ToString();
                switch (type)
                {
                    case "canceled":
                    case "open":
                    case "filled":
                    case "update":
                        var orderData = GetData<KucoinStreamOrderBaseUpdate>(tokenData);
                        InvokeHandler(tokenData.As(orderData, orderData.Symbol), onOrderData!);
                        break;
                    case "match":
                        var tradeData = GetData<KucoinStreamOrderMatchUpdate>(tokenData);
                        InvokeHandler(tokenData.As(tradeData, tradeData.Symbol), onTradeData!);
                        break;
                }
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", "/spotMarket/tradeOrders", true);
            return await SubscribeAsync(request, null, true, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to balance updates
        /// </summary>
        /// <param name="onBalanceChange">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToBalanceChangesAsync(Action<DataEvent<KucoinBalanceUpdate>> onBalanceChange)
        {
            var innerHandler = new Action<DataEvent<JToken>>(data =>
            {
                var desResult = Deserialize<KucoinUpdateMessage<KucoinBalanceUpdate>>(data.Data, false);
                if (!desResult)
                {
                    log.Write(LogLevel.Warning, "Failed to deserialize balance update: " + desResult.Error);
                    return;
                }
                onBalanceChange(data.As(desResult.Data.Data, desResult.Data.Data.Currency));
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", "/account/balance", true);
            return await SubscribeAsync(request, null, true, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to updates for stop orders
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToStopOrderUpdatesAsync(Action<DataEvent<KucoinStreamStopOrderUpdateBase>> onData)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                var data = GetData<KucoinStreamStopOrderUpdate>(tokenData);
                InvokeHandler(tokenData.As((KucoinStreamStopOrderUpdateBase)data, data.Symbol), onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", $"/spotMarket/advancedOrders", true);
            return await SubscribeAsync(request, null, true, innerHandler).ConfigureAwait(false);
        }
        #endregion


        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> SubscribeAsync<T>(string url, object? request, string? identifier, bool authenticated, Action<DataEvent<T>> dataHandler)
        {
            SocketConnection? socketConnection;
            SocketSubscription subscription;
            var released = false;
            await semaphoreSlim.WaitAsync().ConfigureAwait(false);
            try
            {
                socketConnection = GetSocketConnection(url, authenticated);
                if (socketConnection == null)
                {
                    KucoinToken token;
                    var clientOptions = KucoinClient.DefaultOptions.Copy();
                    KucoinApiCredentials? thisCredentials = (KucoinApiCredentials?)authProvider?.Credentials;
                    if (thisCredentials != null)
                    {
                        clientOptions.ApiCredentials = new KucoinApiCredentials(thisCredentials.Key!.GetString(),
                            thisCredentials.Secret!.GetString(), thisCredentials.PassPhrase.GetString());
                    }

                    // Create new socket
                    IWebsocket socket;
                    if (SocketFactory is WebsocketFactory)
                    {
                        using (var restClient = new KucoinClient(clientOptions))
                        {
                            WebCallResult<KucoinToken> tokenResult = await ((KucoinClientSpot)restClient.Spot).GetWebsocketToken(authenticated).ConfigureAwait(false);
                            if (!tokenResult)
                                return new CallResult<UpdateSubscription>(null, tokenResult.Error);
                            token = tokenResult.Data;
                        }

                        socket = CreateSocket(token.Servers.First().Endpoint + "?token=" + token.Token);
                    }
                    else
                        socket = CreateSocket("test");

                    socketConnection = new SocketConnection(this, socket);
                    foreach (var kvp in genericHandlers)
                        socketConnection.AddSubscription(SocketSubscription.CreateForIdentifier(NextId(), kvp.Key, false, kvp.Value));
                }

                subscription = AddSubscription(request, identifier, true, socketConnection, dataHandler);
                if (SocketCombineTarget == 1)
                {
                    // Can release early when only a single sub per connection
                    semaphoreSlim.Release();
                    released = true;
                }

                var connectResult = await ConnectIfNeededAsync(socketConnection, authenticated).ConfigureAwait(false);
                if (!connectResult)
                    return new CallResult<UpdateSubscription>(null, connectResult.Error);
            }
            finally
            {
                //When the task is ready, release the semaphore. It is vital to ALWAYS release the semaphore when we are ready, or else we will end up with a Semaphore that is forever locked.
                //This is why it is important to do the Release within a try...finally clause; program execution may crash or take a different path, this way you are guaranteed execution
                if (!released)
                    semaphoreSlim.Release();
            }


            if (request != null)
            {
                var subResult = await SubscribeAndWaitAsync(socketConnection, request, subscription).ConfigureAwait(false);
                if (!subResult)
                {
                    await socketConnection.CloseAsync(subscription).ConfigureAwait(false);
                    return new CallResult<UpdateSubscription>(null, subResult.Error);
                }

            }
            else
            {
                subscription.Confirmed = true;
            }

            socketConnection.ShouldReconnect = true;
            return new CallResult<UpdateSubscription>(new UpdateSubscription(socketConnection, subscription), null);
        }

        /// <inheritdoc />
        protected override SocketConnection GetSocketConnection(string address, bool authenticated)
        {
            var socketResult = sockets.Where(s => (s.Value.Authenticated == authenticated || !authenticated) && s.Value.Connected).OrderBy(s => s.Value.SubscriptionCount).FirstOrDefault();
            var result = socketResult.Equals(default(KeyValuePair<int, SocketConnection>)) ? null : socketResult.Value;
            if (result != null)
            {
                if (result.SubscriptionCount < SocketCombineTarget || (sockets.Count >= MaxSocketConnections && sockets.All(s => s.Value.SubscriptionCount >= SocketCombineTarget)))
                {
                    // Use existing socket if it has less than target connections OR it has the least connections and we can't make new
                    return result;
                }
            }

            return null!;
        }

        /// <inheritdoc />
        protected override bool HandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override bool HandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object>? callResult)
        {
            callResult = null;
            if (message.Type != JTokenType.Object)
                return false;

            var id = message["id"];
            if (id == null)
                return false;

            var kRequest = (KucoinRequest)request;
            if (id!.ToString() != kRequest.Id)
                return false;

            var result = Deserialize<KucoinSubscribeResponse>(message, false);
            if (!result)
            {
                callResult = new CallResult<object>(null, result.Error);
                return true;
            }

            if (result.Data.Type != "ack")
            {
                callResult = new CallResult<object>(null, new ServerError(result.Data.Code, result.Data.Data));
                return true;
            }

            callResult = new CallResult<object>(result.Data, null);
            return true;
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(JToken message, object request)
        {
            if (message["type"] == null || message["type"]?.ToString() != "message")
                return false;

            if (message["topic"] == null)
                return false;

            var kRequest = (KucoinRequest)request;
            var topic = message["topic"]?.ToString();
            if (kRequest.Topic == topic)
                return true;

            var subject = message["subject"]?.ToString();
            if (topic != null)
            {
                if ((kRequest.Topic.StartsWith("/market/ticker:") && subject != null && subject == "trade.ticker")
                || (kRequest.Topic.StartsWith("/market/level2:") && topic.StartsWith("/market/level2"))
                || (kRequest.Topic.StartsWith("/spotMarket/level3:") && topic.StartsWith("/spotMarket/level3"))
                || (kRequest.Topic.StartsWith("/spotMarket/level2Depth5:") && topic.StartsWith("/spotMarket/level2Depth5"))
                || (kRequest.Topic.StartsWith("/spotMarket/level2Depth20:") && topic.StartsWith("/spotMarket/level2Depth20"))
                || (kRequest.Topic.StartsWith("/indicator/index:") && topic.StartsWith("/indicator/index"))
                || (kRequest.Topic.StartsWith("/indicator/markPrice:") && topic.StartsWith("/indicator/markPrice"))
                || (kRequest.Topic.StartsWith("/market/snapshot:") && topic.StartsWith("/market/snapshot")))
                {
                    var marketSplit = topic.Split(':');
                    if (marketSplit.Length > 1)
                    {
                        var market = marketSplit[1];
                        var subMarkets = kRequest.Topic.Split(':').Last().Split(',');
                        if (subMarkets.Contains(market))
                            return true;
                    }
                }
            }

            if (kRequest.Topic == "/account/balance" && subject != null && subject == "account.balance")
                return true;

            return false;
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(JToken message, string identifier)
        {
            if (message["type"] != null)
            {
                var type = message["type"]!.ToString();
                if (type == "pong" && identifier == "Ping")
                    return true;

                if (type == "welcome" && identifier == "Welcome")
                    return true;
            }

            return false;
        }

        /// <inheritdoc />
        protected override Task<CallResult<bool>> AuthenticateSocketAsync(SocketConnection s)
        {
            return Task.FromResult(new CallResult<bool>(true, null));
        }

        /// <inheritdoc />
        protected override async Task<bool> UnsubscribeAsync(SocketConnection connection, SocketSubscription s)
        {
            var kRequest = (KucoinRequest)s.Request!;
            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "unsubscribe", kRequest.Topic, false);

            var success = false;
            await connection.SendAndWaitAsync(request, TimeSpan.FromSeconds(5), message =>
            {
                var id = message["id"];
                if (id == null)
                    return false;

                if (id!.ToString() != request.Id)
                    return false;

                var result = Deserialize<KucoinSubscribeResponse>(message, false);
                if (!result)
                {
                    log.Write(LogLevel.Warning, "Failed to unsubscribe: " + result.Error);
                    success = false;
                    return true;
                }

                if (result.Data.Type != "ack")
                {
                    log.Write(LogLevel.Warning, "Failed to unsubscribe: " + new ServerError(result.Data.Code, result.Data.Data));
                    success = false;
                    return true;
                }

                success = true;
                return true;
            }).ConfigureAwait(false);

            return success;
        }

        internal static void InvokeHandler<T>(T data, Action<T> handler)
        {
            if (Equals(data, default(T)!))
                return;

            handler?.Invoke(data!);
        }

        internal T GetData<T>(DataEvent<JToken> tokenData)
        {
            var desResult = Deserialize<KucoinUpdateMessage<T>>(tokenData.Data, false);
            if (!desResult)
            {
                log.Write(LogLevel.Warning, "Failed to deserialize update: " + desResult.Error + ", data: " + tokenData);
                return default!;
            }
            return desResult.Data.Data;
        }

        private static string? TryGetSymbolFromTopic(DataEvent<JToken> data)
        {
            string? symbol = null;
            var topic = data.Data["topic"]?.ToString();
            if (topic != null && topic.Contains(':'))
                symbol = topic.Split(':').Last();
            return symbol;
        }

    }
}
