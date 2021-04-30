using CryptoExchange.Net;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Objects;
using Kucoin.Net.Objects.Sockets;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Kucoin.Net.Converts;
using Kucoin.Net.Interfaces;
using Newtonsoft.Json;

namespace Kucoin.Net
{
    /// <summary>
    /// Client for interacting with the Kucoin websocket API
    /// </summary>
    public class KucoinSocketClient: SocketClient, IKucoinSocketClient
    {
        #region fields
        private static KucoinSocketClientOptions defaultOptions = new KucoinSocketClientOptions();
        private static KucoinSocketClientOptions DefaultOptions => defaultOptions.Copy();
        #endregion

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of the KucoinSocketClient with the default options
        /// </summary>
        public KucoinSocketClient() : this(DefaultOptions)
        {
        }

        /// <summary>
        /// Create a new instance of the KucoinSocketClient with the provided options
        /// </summary>
        public KucoinSocketClient(KucoinSocketClientOptions options) : base("Kucoin", options, options.ApiCredentials == null ? null : new KucoinAuthenticationProvider(options.ApiCredentials))
        {
            MaxSocketConnections = 10;

            SendPeriodic(TimeSpan.FromSeconds(30), (connection) => new KucoinPing()
            {
                Id = Math.Round((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds).ToString(CultureInfo.InvariantCulture),
                Type = "ping"
            });

            AddGenericHandler("Ping", (con, msg) => { });
            AddGenericHandler("Welcome", (con, msg) => { });
        }
        #endregion

        #region methods    
        #region public
        /// <summary>
        /// Sets the default options to use for new clients
        /// </summary>
        /// <param name="options">The options to use for new clients</param>
        public static void SetDefaultOptions(KucoinSocketClientOptions options)
        {
            defaultOptions = options;
        }

        /// <summary>
        /// Subscribe to updates for a symbol ticker
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToTickerUpdates(string symbol, Action<KucoinStreamTick> onData) => SubscribeToTickerUpdatesAsync(new[] { symbol }, onData).Result;

        /// <summary>
        /// Subscribe to updates for a symbol ticker
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<KucoinStreamTick> onData) => SubscribeToTickerUpdatesAsync(new[] { symbol }, onData);

        /// <summary>
        /// Subscribe to updates for a symbol ticker
        /// </summary>
        /// <param name="symbols">The symbols to subscribe to</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToTickerUpdates(IEnumerable<string> symbols, Action<KucoinStreamTick> onData) => SubscribeToTickerUpdatesAsync(symbols, onData).Result;

        /// <summary>
        /// Subscribe to updates for a symbol ticker
        /// </summary>
        /// <param name="symbols">The symbols to subscribe to</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<KucoinStreamTick> onData)
        {
            symbols.ValidateNotNull(nameof(symbols));
            foreach(var symbol in symbols)
                symbol.ValidateKucoinSymbol();

            var innerHandler = new Action<JToken>(tokenData =>
            {
                var data = GetData<KucoinStreamTick>(tokenData);
                if (data == null)
                    return;

                data.Symbol = ((string)tokenData["topic"]).Split(':').Last();
                InvokeHandler(data, onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", "/market/ticker:" + string.Join(",", symbols), false);
            return await Subscribe(request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to updates for all symbol tickers
        /// </summary>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToAllTickerUpdates(Action<KucoinStreamTick> onData) => SubscribeToAllTickerUpdatesAsync(onData).Result;

        /// <summary>
        /// Subscribe to updates for all symbol tickers
        /// </summary>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllTickerUpdatesAsync(Action<KucoinStreamTick> onData)
        {
            var innerHandler = new Action<JToken>(tokenData =>
            {
                var data = GetData<KucoinStreamTick>(tokenData);
                if (data == null)
                    return;

                data.Symbol = (string)tokenData["subject"];
                InvokeHandler(data, onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", "/market/ticker:all", false);
            return await Subscribe(request, null, false, innerHandler).ConfigureAwait(false);
        }


        /// <summary>
        /// Subscribe to updates for symbol or market snapshots
        /// </summary>
        /// <param name="symbolOrMarket">The symbol (ie KCS-BTC) or market (ie BTC) to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToSnapshotUpdates(string symbolOrMarket, Action<KucoinStreamSnapshot> onData) => SubscribeToSnapshotUpdatesAsync(symbolOrMarket, onData).Result;

        /// <summary>
        /// Subscribe to updates for symbol or market snapshots
        /// </summary>
        /// <param name="symbolOrMarket">The symbol (ie KCS-BTC) or market (ie BTC) to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public Task<CallResult<UpdateSubscription>> SubscribeToSnapshotUpdatesAsync(string symbolOrMarket,
            Action<KucoinStreamSnapshot> onData)
            => SubscribeToSnapshotUpdatesAsync(new [] { symbolOrMarket }, onData);

        /// <summary>
        /// Subscribe to updates for symbol or market snapshots
        /// </summary>
        /// <param name="symbolOrMarkets">The symbols (ie KCS-BTC) or markets (ie BTC) to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToSnapshotUpdates(
            IEnumerable<string> symbolOrMarkets, Action<KucoinStreamSnapshot> onData) =>
            SubscribeToSnapshotUpdatesAsync(symbolOrMarkets, onData).Result;

        /// <summary>
        /// Subscribe to updates for symbol or market snapshots
        /// </summary>
        /// <param name="symbolOrMarkets">The symbols (ie KCS-BTC) or markets (ie BTC) to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToSnapshotUpdatesAsync(
            IEnumerable<string> symbolOrMarkets, Action<KucoinStreamSnapshot> onData)
        {
            var innerHandler = new Action<JToken>(tokenData => {
                InvokeHandler(GetData<KucoinStreamSnapshotWrapper>(tokenData)?.Data, onData!);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", "/market/snapshot:" + string.Join(",", symbolOrMarkets), false);
            return await Subscribe(request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to aggregated order book updates
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToAggregatedOrderBookUpdates(string symbol, Action<KucoinStreamOrderBook> onData) => SubscribeToAggregatedOrderBookUpdatesAsync(new[] { symbol }, onData).Result;

        /// <summary>
        /// Subscribe to aggregated order book updates
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public Task<CallResult<UpdateSubscription>> SubscribeToAggregatedOrderBookUpdatesAsync(string symbol, Action<KucoinStreamOrderBook> onData) => SubscribeToAggregatedOrderBookUpdatesAsync(new[] { symbol }, onData);

        /// <summary>
        /// Subscribe to aggregated order book updates
        /// </summary>
        /// <param name="symbols">The symbols to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToAggregatedOrderBookUpdates(IEnumerable<string> symbols, Action<KucoinStreamOrderBook> onData) => SubscribeToAggregatedOrderBookUpdatesAsync(symbols, onData).Result;

        /// <summary>
        /// Subscribe to aggregated order book updates
        /// </summary>
        /// <param name="symbols">The symbols to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToAggregatedOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<KucoinStreamOrderBook> onData)
        {
            symbols.ValidateNotNull(nameof(symbols));
            foreach (var symbol in symbols)
                symbol.ValidateKucoinSymbol();
                    
            var innerHandler = new Action<JToken>(tokenData => {
                InvokeHandler(GetData<KucoinStreamOrderBook>(tokenData), onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", "/market/level2:" + string.Join(",", symbols), false);
            return await Subscribe(request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to trade updates
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToTradeUpdates(string symbol, Action<KucoinStreamMatch> onData) => SubscribeToTradeUpdatesAsync(symbol, onData).Result;

        /// <summary>
        /// Subscribe to trade updates
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<KucoinStreamMatch> onData)
        {
            symbol.ValidateKucoinSymbol();

            var innerHandler = new Action<JToken>(tokenData => {
                InvokeHandler(GetData<KucoinStreamMatch>(tokenData), onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", "/market/match:" + symbol, false);
            return await Subscribe(request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to kline updates
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="interval">Interval of the klines</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToKlineUpdates(string symbol, KucoinKlineInterval interval,
            Action<KucoinStreamCandle> onData) => SubscribeToKlineUpdatesAsync(symbol, interval, onData).Result;

        /// <summary>
        /// Subscribe to kline updates
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="interval">Interval of the klines</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KucoinKlineInterval interval, Action<KucoinStreamCandle> onData)
        {
            symbol.ValidateKucoinSymbol();

            var innerHandler = new Action<JToken>(tokenData => {
                InvokeHandler(GetData<KucoinStreamCandle>(tokenData), onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", $"/market/candles:{symbol}_{JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false))}", false);
            return await Subscribe(request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to full order book updates
        /// </summary>
        /// <param name="symbol">The symbol to subscribe</param>
        /// <param name="limit">The amount of levels to receive, either 5 or 50</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToOrderBookUpdates(string symbol, int limit,
            Action<KucoinStreamOrderBookChanged> onData) =>
            SubscribeToOrderBookUpdatesAsync(new[] { symbol }, limit, onData).Result;

        /// <summary>
        /// Subscribe to full order book updates
        /// </summary>
        /// <param name="symbols">The symbols to subscribe</param>
        /// <param name="limit">The amount of levels to receive, either 5 or 50</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToOrderBookUpdates(IEnumerable<string> symbols, int limit,
            Action<KucoinStreamOrderBookChanged> onData) =>
            SubscribeToOrderBookUpdatesAsync(symbols, limit, onData).Result;

        /// <summary>
        /// Subscribe to full order book updates
        /// </summary>
        /// <param name="symbol">The symbol to subscribe</param>
        /// <param name="limit">The amount of levels to receive, either 5 or 50</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int limit,
            Action<KucoinStreamOrderBookChanged> onData) =>
            SubscribeToOrderBookUpdatesAsync(new[] {symbol}, limit, onData);

        /// <summary>
        /// Subscribe to full order book updates
        /// </summary>
        /// <param name="symbols">The symbols to subscribe</param>
        /// <param name="limit">The amount of levels to receive, either 5 or 50</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int limit, Action<KucoinStreamOrderBookChanged> onData)
        {
            foreach(var symbol in symbols)
                symbol.ValidateKucoinSymbol();
            limit.ValidateIntValues(nameof(limit), 5, 50);

            var innerHandler = new Action<JToken>(tokenData => {
                InvokeHandler(GetData<KucoinStreamOrderBookChanged>(tokenData), onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", $"/spotMarket/level2Depth{limit}:" + string.Join(",", symbols), false);
            return await Subscribe(request, null, false, innerHandler).ConfigureAwait(false);
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
        public CallResult<UpdateSubscription> SubscribeToMatchEngineUpdates(string symbol, Action<KucoinStreamMatchEngineUpdate> onData) => SubscribeToMatchEngineUpdatesAsync(new[] { symbol }, onData).Result;

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
        public Task<CallResult<UpdateSubscription>> SubscribeToMatchEngineUpdatesAsync(string symbol, Action<KucoinStreamMatchEngineUpdate> onData) => SubscribeToMatchEngineUpdatesAsync(new[] { symbol }, onData);

        /// <summary>
        /// <para>Subscribe to match engine updates. There are different update types with classes derived from <see cref="KucoinStreamOrderBaseUpdate" /></para>
        /// <para><see cref="KucoinStreamMatchEngineUpdate" />: A valid order is received by the matching engine</para>
        /// <para><see cref="KucoinStreamMatchEngineOpenUpdate" />: A limit order is opened on the order book</para>
        /// <para><see cref="KucoinStreamMatchEngineDoneUpdate" />: An order is no longer on the order book</para>
        /// <para><see cref="KucoinStreamMatchEngineMatchUpdate" />: An order is matched with another order</para>
        /// <para><see cref="KucoinStreamMatchEngineChangeUpdate" />: An order is changed (decreased) in size</para>
        /// </summary>
        /// <param name="symbols">The symbol to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToMatchEngineUpdates(IEnumerable<string> symbols, Action<KucoinStreamMatchEngineUpdate> onData) => SubscribeToMatchEngineUpdatesAsync(symbols, onData).Result;

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
        public async Task<CallResult<UpdateSubscription>> SubscribeToMatchEngineUpdatesAsync(IEnumerable<string> symbols, Action<KucoinStreamMatchEngineUpdate> onData)
        {
            symbols.ValidateNotNull(nameof(symbols));
            foreach (var symbol in symbols)
                symbol.ValidateKucoinSymbol();

            var innerHandler = new Action<JToken>(tokenData => {
                KucoinStreamMatchEngineUpdate? data = null;
                var subject = (string)tokenData["subject"];
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
                }

                InvokeHandler(data, onData!);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", "/spotMarket/level3:" + string.Join(",", symbols), false);
            return await Subscribe(request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to order updates for your own orders 
        /// </summary>
        /// <param name="onOrderData">Data handler for order updates</param>
        /// <param name="onTradeData">Data handler for trade updates</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToOrderUpdates(Action<KucoinStreamOrderBaseUpdate> onOrderData, Action<KucoinStreamOrderMatchUpdate> onTradeData) => SubscribeToOrderUpdatesAsync(onOrderData, onTradeData).Result;

        /// <summary>
        /// Subscribe to order updates for your own orders 
        /// </summary>
        /// <param name="onOrderData">Data handler for order updates</param>
        /// <param name="onTradeData">Data handler for trade updates</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<KucoinStreamOrderBaseUpdate> onOrderData, Action<KucoinStreamOrderMatchUpdate> onTradeData)
        {
            var innerHandler = new Action<JToken>(tokenData => {
                var type = (string)tokenData["data"]["type"];
                switch (type)
                {
                    case "canceled":
                    case "open":
                    case "filled":
                    case "update":
                        var orderData = GetData<KucoinStreamOrderBaseUpdate>(tokenData);
                        InvokeHandler(orderData, onOrderData!);
                        break;
                    case "match":
                        var tradeData = GetData<KucoinStreamOrderMatchUpdate>(tokenData);
                        InvokeHandler(tradeData, onTradeData!);
                        break;
                }
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", "/spotMarket/tradeOrders", true);
            return await Subscribe(request, null, true, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to balance updates
        /// </summary>
        /// <param name="onBalanceChange">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToBalanceChanges(Action<KucoinBalanceUpdate> onBalanceChange) => SubscribeToBalanceChangesAsync(onBalanceChange).Result;
        
        /// <summary>
        /// Subscribe to balance updates
        /// </summary>
        /// <param name="onBalanceChange">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToBalanceChangesAsync(Action<KucoinBalanceUpdate> onBalanceChange)
        {
            var innerHandler = new Action<JToken>(data =>
            {
                var desResult = Deserialize<KucoinUpdateMessage<KucoinBalanceUpdate>>(data, false);
                if (!desResult)
                {
                    log.Write(LogVerbosity.Warning, "Failed to deserialize balance update: " + desResult.Error);
                    return;
                }
                onBalanceChange(desResult.Data.Data);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", "/account/balance", true);
            return await Subscribe(request, null, true, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to updates for stop orders
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToStopOrderUpdates(Action<KucoinStreamStopOrderUpdate> onData) =>
            SubscribeToStopOrderUpdatesAsync(onData).Result;

        /// <summary>
        /// Subscribe to updates for stop orders
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToStopOrderUpdatesAsync(Action<KucoinStreamStopOrderUpdate> onData)
        {
            var innerHandler = new Action<JToken>(tokenData => {
                InvokeHandler(GetData<KucoinStreamStopOrderUpdate>(tokenData), onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", $"/spotMarket/advancedOrders", true);
            return await Subscribe(request, null, false, innerHandler).ConfigureAwait(false);
        }
        #endregion
        #region private

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> Subscribe<T>(string url, object? request, string? identifier, bool authenticated, Action<T> dataHandler)
        {
            SocketConnection? socket;
            SocketSubscription handler;
            var released = false;
            await semaphoreSlim.WaitAsync().ConfigureAwait(false);
            try
            {
                socket = GetWebsocket(url, authenticated);
                if (socket == null)
                {
                    KucoinToken token;
                    var clientOptions = KucoinClient.DefaultOptions.Copy();
                    KucoinApiCredentials? thisCredentials = (KucoinApiCredentials?)authProvider?.Credentials;
                    if (thisCredentials != null)
                    {
                        clientOptions.ApiCredentials = new KucoinApiCredentials(thisCredentials.Key!.GetString(),
                            thisCredentials.Secret!.GetString(), thisCredentials.PassPhrase.GetString());
                    }

                    using (var restClient = new KucoinClient(clientOptions))
                    {
                        var tokenResult = restClient.GetWebsocketToken(authenticated).Result;
                        if (!tokenResult)
                            return new CallResult<UpdateSubscription>(null, tokenResult.Error);
                        token = tokenResult.Data;
                    }

                    // Create new socket
                    var s = CreateSocket(token.Servers.First().Endpoint + "?token=" + token.Token);
                    socket = new SocketConnection(this, s);
                    foreach (var kvp in genericHandlers)
                        socket.AddHandler(SocketSubscription.CreateForIdentifier(kvp.Key, false, kvp.Value));
                }

                handler = AddHandler(request, identifier, true, socket, dataHandler);
                if (SocketCombineTarget == 1)
                {
                    // Can release early when only a single sub per connection
                    semaphoreSlim.Release();
                    released = true;
                }

                var connectResult = await ConnectIfNeeded(socket, authenticated).ConfigureAwait(false);
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
                var subResult = await SubscribeAndWait(socket, request, handler).ConfigureAwait(false);
                if (!subResult)
                {
                    await socket.Close(handler).ConfigureAwait(false);
                    return new CallResult<UpdateSubscription>(null, subResult.Error);
                }

            }
            else
            {
                handler.Confirmed = true;
            }

            socket.ShouldReconnect = true;
            return new CallResult<UpdateSubscription>(new UpdateSubscription(socket, handler), null);
        }

        /// <inheritdoc />
        protected override SocketConnection GetWebsocket(string address, bool authenticated)
        {
            var socketResult = sockets.Where(s => (s.Value.Authenticated == authenticated || !authenticated) && s.Value.Connected).OrderBy(s => s.Value.HandlerCount).FirstOrDefault();
            var result = socketResult.Equals(default(KeyValuePair<int, SocketConnection>)) ? null : socketResult.Value;
            if (result != null)
            {
                if (result.HandlerCount < SocketCombineTarget || (sockets.Count >= MaxSocketConnections && sockets.All(s => s.Value.HandlerCount >= SocketCombineTarget)))
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
            if ((string)id != kRequest.Id)
                return false;

            var result = Deserialize<KucoinSubscribeResponse>(message, false);
            if(!result)
            {
                callResult = new CallResult<object>(null, result.Error);
                return true;
            }

            if(result.Data.Type != "ack")
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
            if (message["type"] == null || (string)message["type"] != "message")
                return false;

            if (message["topic"] == null)
                return false;

            var kRequest = (KucoinRequest)request;
            var topic = (string)message["topic"];
            if (kRequest.Topic == topic)
                return true;

            if((kRequest.Topic.StartsWith("/market/ticker:") && message["subject"] != null && (string)message["subject"] == "trade.ticker")
            || (kRequest.Topic.StartsWith("/market/level2:") && ((string)message["topic"]).StartsWith("/market/level2"))
            || (kRequest.Topic.StartsWith("/spotMarket/level3:") && ((string)message["topic"]).StartsWith("/spotMarket/level3"))
            || (kRequest.Topic.StartsWith("/spotMarket/level2Depth5:") && ((string)message["topic"]).StartsWith("/spotMarket/level2Depth5"))
            || (kRequest.Topic.StartsWith("/spotMarket/level2Depth20:") && ((string)message["topic"]).StartsWith("/spotMarket/level2Depth20"))
            || (kRequest.Topic.StartsWith("/market/snapshot:") && ((string)message["topic"]).StartsWith("/market/snapshot")))
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

            if (kRequest.Topic == "/account/balance" && message["subject"] != null && (string)message["subject"] == "account.balance")
                return true;
            
            return false;
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(JToken message, string identifier)
        {
            if (message["type"] != null)
            {
                var type = (string)message["type"];
                if (type == "pong" && identifier == "Ping")
                    return true;

                if (type == "welcome" && identifier == "Welcome")
                    return true;
            }

            return false;
        }

        /// <inheritdoc />
        protected override Task<CallResult<bool>> AuthenticateSocket(SocketConnection s)
        {
            return Task.FromResult(new CallResult<bool>(true, null));
        }

        /// <inheritdoc />
        protected override async Task<bool> Unsubscribe(SocketConnection connection, SocketSubscription s)
        {
            var kRequest = (KucoinRequest)s.Request!;
            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "unsubscribe", kRequest.Topic, false);

            var success = false;
            await connection.SendAndWait(request, TimeSpan.FromSeconds(5), message =>
            {
                var id = message["id"];
                if (id == null)
                    return false;

                if ((string)id != request.Id)
                    return false;

                var result = Deserialize<KucoinSubscribeResponse>(message, false);
                if (!result)
                {
                    log.Write(LogVerbosity.Warning, "Failed to unsubscribe: " + result.Error);
                    success = false;
                    return true;
                }

                if (result.Data.Type != "ack")
                {
                    log.Write(LogVerbosity.Warning, "Failed to unsubscribe: " + new ServerError(result.Data.Code, result.Data.Data));
                    success = false;
                    return true;
                }

                success = true;
                return true;
            }).ConfigureAwait(false);

            return success;
        }

        private void InvokeHandler<T>(T data, Action<T> handler)
        {
            if (Equals(data, default(T)!))
                return;

            handler(data!);
        }

        private T GetData<T>(JToken tokenData)
        {
            var desResult = Deserialize<KucoinUpdateMessage<T>>(tokenData, false);
            if (!desResult)
            {
                log.Write(LogVerbosity.Warning, "Failed to deserialize update: " + desResult.Error + ", data: " + tokenData);
                return default!;
            }
            return desResult.Data.Data;
        }
        #endregion
        #endregion
    }
}
