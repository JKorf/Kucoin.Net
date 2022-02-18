using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Objects;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Kucoin.Net.Enums;
using System.Threading;
using Kucoin.Net.Objects.Internal;
using Kucoin.Net.Objects.Models;
using Kucoin.Net.Objects.Models.Futures;
using Kucoin.Net.Objects.Models.Futures.Socket;
using Kucoin.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Authentication;
using Kucoin.Net.Interfaces.Clients.FuturesApi;

namespace Kucoin.Net.Clients.FuturesApi
{
    /// <inheritdoc cref="IKucoinSocketClientFuturesStreams" />
    public class KucoinSocketClientFuturesStreams : SocketApiClient, IKucoinSocketClientFuturesStreams
    {
        private readonly KucoinSocketClient _baseClient;
        private readonly Log _log;

        internal KucoinSocketClientFuturesStreams(Log log, KucoinSocketClient baseClient, KucoinSocketClientOptions options)
            : base(options, options.FuturesStreamsOptions)
        {
            _baseClient = baseClient;
            _log = log;
        }

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new KucoinAuthenticationProvider((KucoinApiCredentials)credentials);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamFuturesMatch>> onData, CancellationToken ct = default)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                KucoinSocketClient.InvokeHandler(tokenData.As(_baseClient.GetData<KucoinStreamFuturesMatch>(tokenData), symbol), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", "/contractMarket/execution:" + symbol, false);
            return await _baseClient.SubscribeInternalAsync(this, "futures", request, null, false, innerHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamFuturesTick>> onData, CancellationToken ct = default)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                KucoinSocketClient.InvokeHandler(tokenData.As(_baseClient.GetData<KucoinStreamFuturesTick>(tokenData), symbol), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", "/contractMarket/tickerV2:" + symbol, false);
            return await _baseClient.SubscribeInternalAsync(this, "futures", request, null, false, innerHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<KucoinFuturesOrderBookChange>> onData, CancellationToken ct = default)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                var data = _baseClient.GetData<JToken>(tokenData);
                var change = data["change"]?.ToString();
                var sequence = data["sequence"]?.ToString();
                if (string.IsNullOrEmpty(change) || string.IsNullOrEmpty(sequence))
                    return;

                var items = change!.Split(',');
                var result = new KucoinFuturesOrderBookChange
                {
                    Sequence = long.Parse(sequence),
                    Price = decimal.Parse(items[0], CultureInfo.InvariantCulture),
                    Side = items[1] == "sell" ? OrderSide.Sell : OrderSide.Buy,
                    Quantity = decimal.Parse(items[2], CultureInfo.InvariantCulture)
                };

                KucoinSocketClient.InvokeHandler(tokenData.As(result, symbol), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contractMarket/level2:" + symbol, false);
            return await _baseClient.SubscribeInternalAsync(this, "futures", request, null, false, innerHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int limit, Action<DataEvent<KucoinStreamOrderBookChanged>> onData, CancellationToken ct = default)
        {
            limit.ValidateIntValues(nameof(limit), 5, 50);

            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                var book = _baseClient.GetData<KucoinStreamOrderBookChanged>(tokenData);
                KucoinSocketClient.InvokeHandler(tokenData.As(book, symbol), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contractMarket/level2Depth{limit}:" + symbol, false);
            return await _baseClient.SubscribeInternalAsync(this, "futures", request, null, false, innerHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarketUpdatesAsync(string symbol,
            Action<DataEvent<KucoinStreamFuturesMarkIndexPrice>> onMarkIndexPriceUpdate,
            Action<DataEvent<KucoinStreamFuturesFundingRate>> onFundingRateUpdate,
            CancellationToken ct = default)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                if (tokenData.Data["subject"]?.ToString() == "mark.index.price")
                {
                    var data = _baseClient.GetData<KucoinStreamFuturesMarkIndexPrice>(tokenData);
                    KucoinSocketClient.InvokeHandler(tokenData.As(data, symbol), onMarkIndexPriceUpdate);
                }
                else
                {
                    var data = _baseClient.GetData<KucoinStreamFuturesFundingRate>(tokenData);
                    KucoinSocketClient.InvokeHandler(tokenData.As(data, symbol), onFundingRateUpdate);
                }
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contract/instrument:" + symbol, false);
            return await _baseClient.SubscribeInternalAsync(this, "futures", request, null, false, innerHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToSystemAnnouncementsAsync(Action<DataEvent<KucoinContractAnnouncement>> onData, CancellationToken ct = default)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                var data = _baseClient.GetData<KucoinContractAnnouncement>(tokenData);
                data.Event = tokenData.Data["subject"]?.ToString() ?? "";
                KucoinSocketClient.InvokeHandler(tokenData.As(data), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contract/announcement", false);
            return await _baseClient.SubscribeInternalAsync(this, "futures", request, null, false, innerHandler, ct).ConfigureAwait(false);
        }


        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeTo24HourSnapshotUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamTransactionStatisticsUpdate>> onData, CancellationToken ct = default)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                var data = _baseClient.GetData<KucoinStreamTransactionStatisticsUpdate>(tokenData);
                KucoinSocketClient.InvokeHandler(tokenData.As(data, symbol), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contractMarket/snapshot:" + symbol, false);
            return await _baseClient.SubscribeInternalAsync(this, "futures", request, null, false, innerHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string? symbol,
            Action<DataEvent<KucoinStreamFuturesOrderUpdate>> onData,
            CancellationToken ct = default)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                var data = _baseClient.GetData<KucoinStreamFuturesOrderUpdate>(tokenData);
                KucoinSocketClient.InvokeHandler(tokenData.As(data, data.Symbol), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contractMarket/tradeOrders" + (symbol == null ? "" : ":" + symbol), true);
            return await _baseClient.SubscribeInternalAsync(this, "futures", request, null, true, innerHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToStopOrderUpdatesAsync(Action<DataEvent<KucoinStreamStopOrderUpdateBase>> onData, CancellationToken ct = default)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                var data = _baseClient.GetData<KucoinStreamFuturesStopOrderUpdate>(tokenData);
                KucoinSocketClient.InvokeHandler(tokenData.As((KucoinStreamStopOrderUpdateBase)data, data.Symbol), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contractMarket/advancedOrders", true);
            return await _baseClient.SubscribeInternalAsync(this, "futures", request, null, true, innerHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(
            Action<DataEvent<KucoinStreamOrderMarginUpdate>> onOrderMarginUpdate,
            Action<DataEvent<KucoinStreamFuturesBalanceUpdate>> onBalanceUpdate,
            Action<DataEvent<KucoinStreamFuturesWithdrawableUpdate>> onWithdrawableUpdate,
            CancellationToken ct = default)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                var subject = tokenData.Data["subject"]?.ToString();
                if (subject == "orderMargin.change")
                {
                    var data = _baseClient.GetData<KucoinStreamOrderMarginUpdate>(tokenData);
                    KucoinSocketClient.InvokeHandler(tokenData.As(data, data.Asset), onOrderMarginUpdate);
                }
                else if (subject == "availableBalance.change")
                {
                    var data = _baseClient.GetData<KucoinStreamFuturesBalanceUpdate>(tokenData);
                    KucoinSocketClient.InvokeHandler(tokenData.As(data, data.Asset), onBalanceUpdate);
                }
                else if (subject == "withdrawHold.change")
                {
                    var data = _baseClient.GetData<KucoinStreamFuturesWithdrawableUpdate>(tokenData);
                    KucoinSocketClient.InvokeHandler(tokenData.As(data, data.Asset), onWithdrawableUpdate);
                }
                else
                    _log.Write(LogLevel.Warning, "Unknown update: " + subject);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contractAccount/wallet", true);
            return await _baseClient.SubscribeInternalAsync(this, "futures", request, null, true, innerHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(
            string symbol,
            Action<DataEvent<KucoinPositionUpdate>> onPositionUpdate,
            Action<DataEvent<KucoinPositionMarkPriceUpdate>> onMarkPriceUpdate,
            Action<DataEvent<KucoinPositionFundingSettlementUpdate>> onFundingSettlementUpdate,
            Action<DataEvent<KucoinPositionRiskAdjustResultUpdate>> onRiskAdjustUpdate,
            CancellationToken ct = default)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                var subject = tokenData.Data["subject"]?.ToString();
                if (subject == "position.change")
                {
                    var changeReason = tokenData.Data["data"]?["changeReason"]?.ToString();
                    if (changeReason == null || changeReason == "markPriceChange")
                    {
                        var data = _baseClient.GetData<KucoinPositionMarkPriceUpdate>(tokenData);
                        KucoinSocketClient.InvokeHandler(tokenData.As(data, symbol), onMarkPriceUpdate);
                    }
                    else
                    {
                        var data = _baseClient.GetData<KucoinPositionUpdate>(tokenData);
                        KucoinSocketClient.InvokeHandler(tokenData.As(data, symbol), onPositionUpdate);
                    }
                }
                else if (subject == "position.settlement")
                {
                    var data = _baseClient.GetData<KucoinPositionFundingSettlementUpdate>(tokenData);
                    KucoinSocketClient.InvokeHandler(tokenData.As(data, symbol), onFundingSettlementUpdate);
                }
                else if (subject == "position.adjustRiskLimit")
                {
                    var data = _baseClient.GetData<KucoinPositionRiskAdjustResultUpdate>(tokenData);
                    KucoinSocketClient.InvokeHandler(tokenData.As(data, symbol), onRiskAdjustUpdate);
                }
                else
                    _log.Write(LogLevel.Warning, $"Unknown update: {subject}, data: {tokenData}");
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contract/position:" + symbol, true);
            return await _baseClient.SubscribeInternalAsync(this, "futures", request, null, true, innerHandler, ct).ConfigureAwait(false);
        }
    }
}
