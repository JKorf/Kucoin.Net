using CryptoExchange.Net;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Interfaces;
using Kucoin.Net.Objects;
using Kucoin.Net.Objects.Futures;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Kucoin.Net.Objects.Futures.Socket;
using Kucoin.Net.Objects.Socket;
using Kucoin.Net.Objects.Spot.Socket;

namespace Kucoin.Net.SocketSubClients
{
    /// <summary>
    /// Futures subscriptions
    /// </summary>
    public class KucoinSocketClientFutures: IKucoinSocketClientFutures
    {
        private KucoinSocketClient _baseClient;
        private Log _log;

        internal KucoinSocketClientFutures(Log log, KucoinSocketClient baseClient)
        {
            _log = log;
            _baseClient = baseClient;
        }

        /// <summary>
        /// Subscribe to trade updates
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamFuturesMatch>> onData)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                _baseClient.InvokeHandler(tokenData.As(_baseClient.GetData<KucoinStreamFuturesMatch>(tokenData), symbol), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", "/contractMarket/execution:" + symbol, false);
            return await _baseClient.SubscribeInternalAsync("futures", request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to ticker updates
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamFuturesTick>> onData)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                _baseClient.InvokeHandler(tokenData.As(_baseClient.GetData<KucoinStreamFuturesTick>(tokenData), symbol), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", "/contractMarket/tickerV2:" + symbol, false);
            return await _baseClient.SubscribeInternalAsync("futures", request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to full order book updates
        /// </summary>
        /// <param name="symbol">The symbol to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<KucoinFuturesOrderBookChange>> onData)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                var data = _baseClient.GetData<JToken>(tokenData);
                var change = data["change"]?.ToString();
                if (string.IsNullOrEmpty(change))
                    return;

                var items = change!.Split(',');
                var result = new KucoinFuturesOrderBookChange
                {
                    Price = decimal.Parse(items[0]),
                    Side = items[1] == "sell" ? KucoinOrderSide.Sell: KucoinOrderSide.Buy,
                    Quantity = decimal.Parse(items[2])
                };

                _baseClient.InvokeHandler(tokenData.As(result), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contractMarket/level2:" + symbol, false);
            return await _baseClient.SubscribeInternalAsync("futures", request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to partial order book updates
        /// </summary>
        /// <param name="symbol">The symbol to subscribe</param>
        /// <param name="limit">The amount of levels to receive, either 5 or 50</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int limit, Action<DataEvent<KucoinStreamOrderBookChanged>> onData)
        {
            limit.ValidateIntValues(nameof(limit), 5, 50);

            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                var book = _baseClient.GetData<KucoinStreamOrderBookChanged>(tokenData);
                _baseClient.InvokeHandler(tokenData.As(book), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contractMarket/level2Depth{limit}:" + symbol, false);
            return await _baseClient.SubscribeInternalAsync("futures", request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to market data updates
        /// </summary>
        /// <param name="symbol">The symbol to subscribe</param>
        /// <param name="onMarkIndexPriceUpdate">Mark/Index price update handler</param>
        /// <param name="onFundingRateUpdate">Funding price update handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarketUpdatesAsync(string symbol, 
            Action<DataEvent<KucoinStreamFuturesMarkIndexPrice>> onMarkIndexPriceUpdate,
            Action<DataEvent<KucoinStreamFuturesFundingRate>> onFundingRateUpdate)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                if (tokenData.Data["subject"]?.ToString() == "mark.index.price")
                {
                    var data = _baseClient.GetData<KucoinStreamFuturesMarkIndexPrice>(tokenData);
                    _baseClient.InvokeHandler(tokenData.As(data), onMarkIndexPriceUpdate);
                }
                else
                {
                    var data = _baseClient.GetData<KucoinStreamFuturesFundingRate>(tokenData);
                    _baseClient.InvokeHandler(tokenData.As(data), onFundingRateUpdate);
                }
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contract/instrument:" + symbol, false);
            return await _baseClient.SubscribeInternalAsync("futures", request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe system announcement
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToSystemAnnouncementsAsync(Action<DataEvent<KucoinContractAnnouncement>> onData)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                var data = _baseClient.GetData<KucoinContractAnnouncement>(tokenData);
                data.Event = tokenData.Data["subject"]?.ToString() ?? "";
                _baseClient.InvokeHandler(tokenData.As(data), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contract/announcement", false);
            return await _baseClient.SubscribeInternalAsync("futures", request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to order updates
        /// </summary>
        /// <param name="symbol">[Optional] Symbol</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string? symbol, 
            Action<DataEvent<KucoinStreamFuturesOrderUpdate>> onData)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                var data = _baseClient.GetData<KucoinStreamFuturesOrderUpdate>(tokenData);
                _baseClient.InvokeHandler(tokenData.As(data, data.Symbol), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contractMarket/tradeOrders" + (symbol == null ? "": ":" +symbol), true);
            return await _baseClient.SubscribeInternalAsync("futures", request, null, true, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to stop order updates
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToStopOrderUpdatesAsync(Action<DataEvent<KucoinStreamStopOrderUpdateBase>> onData)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                var data = _baseClient.GetData<KucoinStreamFuturesStopOrderUpdate>(tokenData);
                _baseClient.InvokeHandler(tokenData.As((KucoinStreamStopOrderUpdateBase)data, data.Symbol), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contractMarket/advancedOrders", false);
            return await _baseClient.SubscribeInternalAsync("futures", request, null, true, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to snapshot updates
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeTo24HourSnapshotUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamTransactionStatisticsUpdate>> onData)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                var data = _baseClient.GetData<KucoinStreamTransactionStatisticsUpdate>(tokenData);
                _baseClient.InvokeHandler(tokenData.As(data), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contractMarket/snapshot:"+symbol, false);
            return await _baseClient.SubscribeInternalAsync("futures", request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to wallet updates
        /// </summary>
        /// <param name="onOrderMarginUpdate">Data handler for order margin updates</param>
        /// <param name="onBalanceUpdate">Data handler for balance updates</param>
        /// <param name="onWithdrawableUpdate">Data handler for withdrawable funds updates</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync( 
            Action<DataEvent<KucoinStreamOrderMarginUpdate>> onOrderMarginUpdate,
            Action<DataEvent<KucoinStreamFuturesBalanceUpdate>> onBalanceUpdate,
            Action<DataEvent<KucoinStreamFuturesWithdrawableUpdate>> onWithdrawableUpdate)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                var subject = tokenData.Data["subject"]?.ToString();
                if (subject == "orderMargin.change")
                {
                    var data = _baseClient.GetData<KucoinStreamOrderMarginUpdate>(tokenData);
                    _baseClient.InvokeHandler(tokenData.As(data), onOrderMarginUpdate);
                }
                else if (subject == "availableBalance.change")
                {
                    var data = _baseClient.GetData<KucoinStreamFuturesBalanceUpdate>(tokenData);
                    _baseClient.InvokeHandler(tokenData.As(data), onBalanceUpdate);
                }
                else if (subject == "withdrawHold.change")
                {
                    var data = _baseClient.GetData<KucoinStreamFuturesWithdrawableUpdate>(tokenData);
                    _baseClient.InvokeHandler(tokenData.As(data), onWithdrawableUpdate);
                }
                else
                    _log.Write(LogLevel.Warning, "Unknown update: " + subject);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contractAccount/wallet", false);
            return await _baseClient.SubscribeInternalAsync("futures", request, null, true, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to position updates
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(string symbol, Action<DataEvent<KucoinPosition>> onData)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                var data = _baseClient.GetData<KucoinPosition>(tokenData);
                _baseClient.InvokeHandler(tokenData.As(data), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contract/position:" + symbol, false);
            return await _baseClient.SubscribeInternalAsync("futures", request, null, true, innerHandler).ConfigureAwait(false);
        }
    }
}
