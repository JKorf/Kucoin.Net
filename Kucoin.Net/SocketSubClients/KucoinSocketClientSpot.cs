using CryptoExchange.Net;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Converts;
using Kucoin.Net.Interfaces;
using Kucoin.Net.Objects;
using Kucoin.Net.Objects.Sockets;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.SocketSubClients
{
    /// <summary>
    /// Spot subscriptions
    /// </summary>
    public class KucoinSocketClientSpot: IKucoinSocketClientSpot
    {
        private KucoinSocketClient _baseClient;
        private Log _log;

        internal KucoinSocketClientSpot(Log log, KucoinSocketClient baseClient)
        {
            _log = log;
            _baseClient = baseClient;
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
                var data = _baseClient.GetData<KucoinStreamTick>(tokenData);
                if (data == null)
                    return;

                data.Symbol = ((string)tokenData.Data["topic"]).Split(':').Last();
                _baseClient.InvokeHandler(tokenData.As(data, data.Symbol), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", "/market/ticker:" + string.Join(",", symbols), false);
            return await _baseClient.SubscribeInternalAsync(request, null, false, innerHandler).ConfigureAwait(false);
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
                var data = _baseClient.GetData<KucoinStreamTick>(tokenData);
                if (data == null)
                    return;

                data.Symbol = (string)tokenData.Data["subject"];
                _baseClient.InvokeHandler(tokenData.As(data, data.Symbol), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", "/market/ticker:all", false);
            return await _baseClient.SubscribeInternalAsync(request, null, false, innerHandler).ConfigureAwait(false);
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
                var data = _baseClient.GetData<KucoinStreamSnapshotWrapper>(tokenData)?.Data;
                _baseClient.InvokeHandler(tokenData.As(data, data?.Symbol), onData!);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", "/market/snapshot:" + string.Join(",", symbolOrMarkets), false);
            return await _baseClient.SubscribeInternalAsync(request, null, false, innerHandler).ConfigureAwait(false);
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
                _baseClient.InvokeHandler(tokenData.As(_baseClient.GetData<KucoinStreamOrderBook>(tokenData)), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", "/market/level2:" + string.Join(",", symbols), false);
            return await _baseClient.SubscribeInternalAsync(request, null, false, innerHandler).ConfigureAwait(false);
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
                _baseClient.InvokeHandler(tokenData.As(_baseClient.GetData<KucoinStreamMatch>(tokenData), symbol), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", "/market/match:" + symbol, false);
            return await _baseClient.SubscribeInternalAsync(request, null, false, innerHandler).ConfigureAwait(false);
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
                _baseClient.InvokeHandler(tokenData.As(_baseClient.GetData<KucoinStreamCandle>(tokenData), symbol), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", $"/market/candles:{symbol}_{JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false))}", false);
            return await _baseClient.SubscribeInternalAsync(request, null, false, innerHandler).ConfigureAwait(false);
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
                var book = _baseClient.GetData<KucoinStreamOrderBookChanged>(tokenData);
                _baseClient.InvokeHandler(tokenData.As(book), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", $"/spotMarket/level2Depth{limit}:" + string.Join(",", symbols), false);
            return await _baseClient.SubscribeInternalAsync(request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to index price updates
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToIndexPriceUpdates(string symbol, Action<KucoinStreamIndicatorPrice> onData) => SubscribeToIndexPriceUpdates(new string[] { symbol }, onData);

        /// <summary>
        /// Subscribe to index price updates
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(string symbol, Action<KucoinStreamIndicatorPrice> onData) => SubscribeToIndexPriceUpdatesAsync(new string[] { symbol }, onData);

        /// <summary>
        /// Subscribe to index price updates
        /// </summary>
        /// <param name="symbols">Symbols to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToIndexPriceUpdates(IEnumerable<string> symbols, Action<KucoinStreamIndicatorPrice> onData) => SubscribeToIndexPriceUpdatesAsync(symbols, onData).Result;

        /// <summary>
        /// Subscribe to index price updates
        /// </summary>
        /// <param name="symbols">Symbols to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(IEnumerable<string> symbols, Action<KucoinStreamIndicatorPrice> onData)
        {
            foreach (var symbol in symbols)
                symbol.ValidateKucoinSymbol();

            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                _baseClient.InvokeHandler(_baseClient.GetData<KucoinStreamIndicatorPrice>(tokenData), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", $"/indicator/index:" + string.Join(",", symbols), false);
            return await _baseClient.SubscribeInternalAsync(request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to mark price updates
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToMarkPriceUpdates(string symbol, Action<KucoinStreamIndicatorPrice> onData) => SubscribeToMarkPriceUpdates(new string[] { symbol }, onData);

        /// <summary>
        /// Subscribe to mark price updates
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, Action<KucoinStreamIndicatorPrice> onData) => SubscribeToMarkPriceUpdatesAsync(new string[] { symbol }, onData);

        /// <summary>
        /// Subscribe to mark price updates
        /// </summary>
        /// <param name="symbols">Symbols to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToMarkPriceUpdates(IEnumerable<string> symbols, Action<KucoinStreamIndicatorPrice> onData) => SubscribeToMarkPriceUpdatesAsync(symbols, onData).Result;

        /// <summary>
        /// Subscribe to mark price updates
        /// </summary>
        /// <param name="symbols">Currency to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, Action<KucoinStreamIndicatorPrice> onData)
        {
            foreach (var symbol in symbols)
                symbol.ValidateKucoinSymbol();

            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                _baseClient.InvokeHandler(_baseClient.GetData<KucoinStreamIndicatorPrice>(tokenData), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", $"/indicator/markPrice:" + string.Join(",", symbols), false);
            return await _baseClient.SubscribeInternalAsync(request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to funding book updates
        /// </summary>
        /// <param name="currency">Currency to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToFundingBookUpdates(string currency, Action<KucoinStreamFundingBookUpdate> onData) => SubscribeToFundingBookUpdates(new string[] { currency }, onData);

        /// <summary>
        /// Subscribe to funding book updates
        /// </summary>
        /// <param name="currency">Currencies to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public Task<CallResult<UpdateSubscription>> SubscribeToFundingBookUpdatesAsync(string currency, Action<KucoinStreamFundingBookUpdate> onData) => SubscribeToFundingBookUpdatesAsync(new string[] { currency }, onData);

        /// <summary>
        /// Subscribe to funding book updates
        /// </summary>
        /// <param name="currencies">Symbols to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToFundingBookUpdates(IEnumerable<string> currencies, Action<KucoinStreamFundingBookUpdate> onData) => SubscribeToFundingBookUpdatesAsync(currencies, onData).Result;

        /// <summary>
        /// Subscribe to funding book updates
        /// </summary>
        /// <param name="currencies">Currencies to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToFundingBookUpdatesAsync(IEnumerable<string> currencies, Action<KucoinStreamFundingBookUpdate> onData)
        {
            foreach (var currency in currencies)
                currency.ValidateNotNull(currency);

            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                _baseClient.InvokeHandler(_baseClient.GetData<KucoinStreamFundingBookUpdate>(tokenData), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", $"/margin/fundingBook:" + string.Join(",", currencies), false);
            return await _baseClient.SubscribeInternalAsync(request, null, false, innerHandler).ConfigureAwait(false);
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
                var subject = (string)tokenData.Data["subject"];
                switch (subject)
                {
                    case "received":
                        data = _baseClient.GetData<KucoinStreamMatchEngineUpdate>(tokenData);
                        break;
                    case "open":
                        data = _baseClient.GetData<KucoinStreamMatchEngineOpenUpdate>(tokenData);
                        break;
                    case "done":
                        data = _baseClient.GetData<KucoinStreamMatchEngineDoneUpdate>(tokenData);
                        break;
                    case "match":
                        data = _baseClient.GetData<KucoinStreamMatchEngineMatchUpdate>(tokenData);
                        break;
                    case "update":
                        data = _baseClient.GetData<KucoinStreamMatchEngineChangeUpdate>(tokenData);
                        break;
                }

                _baseClient.InvokeHandler(tokenData.As<KucoinStreamMatchEngineUpdate>(data, data.Symbol), onData!);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", "/spotMarket/level3:" + string.Join(",", symbols), false);
            return await _baseClient.SubscribeInternalAsync(request, null, false, innerHandler).ConfigureAwait(false);
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
                var type = (string)tokenData.Data["data"]["type"];
                switch (type)
                {
                    case "canceled":
                    case "open":
                    case "filled":
                    case "update":
                        var orderData = _baseClient.GetData<KucoinStreamOrderBaseUpdate>(tokenData);
                        _baseClient.InvokeHandler(tokenData.As(orderData, orderData.Symbol), onOrderData!);
                        break;
                    case "match":
                        var tradeData = _baseClient.GetData<KucoinStreamOrderMatchUpdate>(tokenData);
                        _baseClient.InvokeHandler(tokenData.As(tradeData, tradeData.Symbol), onTradeData!);
                        break;
                }
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", "/spotMarket/tradeOrders", true);
            return await _baseClient.SubscribeInternalAsync(request, null, true, innerHandler).ConfigureAwait(false);
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
                var desResult = _baseClient.DeserializeInternal<KucoinUpdateMessage<KucoinBalanceUpdate>>(data.Data, false);
                if (!desResult)
                {
                    _log.Write(LogLevel.Warning, "Failed to deserialize balance update: " + desResult.Error);
                    return;
                }
                onBalanceChange(data.As(desResult.Data.Data, desResult.Data.Data.Currency));
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", "/account/balance", true);
            return await _baseClient.SubscribeInternalAsync(request, null, true, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to updates for stop orders
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToStopOrderUpdatesAsync(Action<DataEvent<KucoinStreamStopOrderUpdate>> onData)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                var data = _baseClient.GetData<KucoinStreamStopOrderUpdate>(tokenData);
                _baseClient.InvokeHandler(tokenData.As(data, data.Symbol), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", $"/spotMarket/advancedOrders", true);
            return await _baseClient.SubscribeInternalAsync(request, null, true, innerHandler).ConfigureAwait(false);
        }
        #endregion
    }
}
