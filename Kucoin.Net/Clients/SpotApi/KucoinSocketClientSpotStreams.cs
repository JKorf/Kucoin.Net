using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Objects;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Kucoin.Net.Converters;
using Kucoin.Net.Enums;
using System.Threading;
using Kucoin.Net.Objects.Internal;
using Kucoin.Net.Objects.Models;
using Kucoin.Net.Objects.Models.Futures.Socket;
using Kucoin.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Authentication;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.CommonObjects;

namespace Kucoin.Net.Clients.SpotApi
{
    /// <inheritdoc cref="IKucoinSocketClientSpotStreams" />
    public class KucoinSocketClientSpotStreams : SocketApiClient, IKucoinSocketClientSpotStreams
    {
        private readonly KucoinSocketClient _baseClient;
        private readonly Log _log;

        internal KucoinSocketClientSpotStreams(Log log, KucoinSocketClient baseClient, KucoinSocketClientOptions options)
            : base(options, options.SpotStreamsOptions)
        {
            _baseClient = baseClient;
            _log = log;
        }

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new KucoinAuthenticationProvider((KucoinApiCredentials)credentials);

        #region public
        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamTick>> onData, CancellationToken ct = default) => SubscribeToTickerUpdatesAsync(new[] { symbol }, onData, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamTick>> onData, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));
            foreach (var symbol in symbols)
                symbol.ValidateKucoinSymbol();

            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                var data = _baseClient.GetData<KucoinStreamTick>(tokenData);
                if (data == null)
                    return;

                data.Symbol = _baseClient.TryGetSymbolFromTopic(tokenData)!;
                KucoinSocketClient.InvokeHandler(tokenData.As(data, data.Symbol), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", "/market/ticker:" + string.Join(",", symbols), false);
            return await _baseClient.SubscribeInternalAsync(this, "spot", request, null, false, innerHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllTickerUpdatesAsync(Action<DataEvent<KucoinStreamTick>> onData, CancellationToken ct = default)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                var data = _baseClient.GetData<KucoinStreamTick>(tokenData);
                if (data == null)
                    return;

                data.Symbol = (string)tokenData.Data["subject"]!;
                KucoinSocketClient.InvokeHandler(tokenData.As(data, data.Symbol), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", "/market/ticker:all", false);
            return await _baseClient.SubscribeInternalAsync(this, "spot", request, null, false, innerHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToSnapshotUpdatesAsync(string symbolOrMarket,
            Action<DataEvent<KucoinStreamSnapshot>> onData, CancellationToken ct = default)
            => SubscribeToSnapshotUpdatesAsync(new[] { symbolOrMarket }, onData, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToSnapshotUpdatesAsync(
            IEnumerable<string> symbolOrMarkets, Action<DataEvent<KucoinStreamSnapshot>> onData, CancellationToken ct = default)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                var data = _baseClient.GetData<KucoinStreamSnapshotWrapper>(tokenData)?.Data;
                if (data == null)
                {
                    _log.Write(LogLevel.Warning, "Failed to process snapshot update");
                    return;
                }

                KucoinSocketClient.InvokeHandler(tokenData.As(data, data?.Symbol), onData!);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", "/market/snapshot:" + string.Join(",", symbolOrMarkets), false);
            return await _baseClient.SubscribeInternalAsync(this, "spot", request, null, false, innerHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToAggregatedOrderBookUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamOrderBook>> onData, CancellationToken ct = default) => SubscribeToAggregatedOrderBookUpdatesAsync(new[] { symbol }, onData, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAggregatedOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamOrderBook>> onData, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));
            foreach (var symbol in symbols)
                symbol.ValidateKucoinSymbol();

            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                var data = _baseClient.GetData<KucoinStreamOrderBook>(tokenData);
                KucoinSocketClient.InvokeHandler(tokenData.As(data, data.Symbol), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", "/market/level2:" + string.Join(",", symbols), false);
            return await _baseClient.SubscribeInternalAsync(this, "spot", request, null, false, innerHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamMatch>> onData, CancellationToken ct = default)
        {
            symbol.ValidateKucoinSymbol();

            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                KucoinSocketClient.InvokeHandler(tokenData.As(_baseClient.GetData<KucoinStreamMatch>(tokenData), symbol), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", "/market/match:" + symbol, false);
            return await _baseClient.SubscribeInternalAsync(this, "spot", request, null, false, innerHandler, ct).ConfigureAwait(false);
        }


        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<KucoinStreamCandle>> onData, CancellationToken ct = default)
        {
            symbol.ValidateKucoinSymbol();

            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                KucoinSocketClient.InvokeHandler(tokenData.As(_baseClient.GetData<KucoinStreamCandle>(tokenData), symbol), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", $"/market/candles:{symbol}_{JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false))}", false);
            return await _baseClient.SubscribeInternalAsync(this, "spot", request, null, false, innerHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int limit,
            Action<DataEvent<KucoinStreamOrderBookChanged>> onData, CancellationToken ct = default) =>
            SubscribeToOrderBookUpdatesAsync(new[] { symbol }, limit, onData, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int limit, int symbolPrecision, Action<DataEvent<KucoinStreamOrderBookChanged>> onData, CancellationToken ct = default)
        {
            symbol.ValidateKucoinSymbol();
            limit.ValidateIntValues(nameof(limit), 5, 50);

            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                var book = _baseClient.GetData<KucoinStreamOrderBookChanged>(tokenData);
                KucoinSocketClient.InvokeHandler(tokenData.As(book, _baseClient.TryGetSymbolFromTopic(tokenData)), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", $"/spotMarket/level2Depth{limit}:" + symbol + "_" + symbolPrecision, false);
            return await _baseClient.SubscribeInternalAsync(this, "spot", request, null, false, innerHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int limit, Action<DataEvent<KucoinStreamOrderBookChanged>> onData, CancellationToken ct = default)
        {
            foreach (var symbol in symbols)
                symbol.ValidateKucoinSymbol();
            limit.ValidateIntValues(nameof(limit), 5, 50);

            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                var book = _baseClient.GetData<KucoinStreamOrderBookChanged>(tokenData);
                KucoinSocketClient.InvokeHandler(tokenData.As(book, _baseClient.TryGetSymbolFromTopic(tokenData)), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", $"/spotMarket/level2Depth{limit}:" + string.Join(",", symbols), false);
            return await _baseClient.SubscribeInternalAsync(this, "spot", request, null, false, innerHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamIndicatorPrice>> onData, CancellationToken ct = default) => SubscribeToIndexPriceUpdatesAsync(new string[] { symbol }, onData, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamIndicatorPrice>> onData, CancellationToken ct = default)
        {
            foreach (var symbol in symbols)
                symbol.ValidateKucoinSymbol();

            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                var data = _baseClient.GetData<KucoinStreamIndicatorPrice>(tokenData);
                KucoinSocketClient.InvokeHandler(tokenData.As(data, data.Symbol), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", "/indicator/index:" + string.Join(",", symbols), false);
            return await _baseClient.SubscribeInternalAsync(this, "spot", request, null, false, innerHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamIndicatorPrice>> onData, CancellationToken ct = default) => SubscribeToMarkPriceUpdatesAsync(new string[] { symbol }, onData, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamIndicatorPrice>> onData, CancellationToken ct = default)
        {
            foreach (var symbol in symbols)
                symbol.ValidateKucoinSymbol();

            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                var data = _baseClient.GetData<KucoinStreamIndicatorPrice>(tokenData);
                KucoinSocketClient.InvokeHandler(tokenData.As(data, data.Symbol), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", "/indicator/markPrice:" + string.Join(",", symbols), false);
            return await _baseClient.SubscribeInternalAsync(this, "spot", request, null, false, innerHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToFundingBookUpdatesAsync(string asset, Action<DataEvent<KucoinStreamFundingBookUpdate>> onData, CancellationToken ct = default) => SubscribeToFundingBookUpdatesAsync(new string[] { asset }, onData, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToFundingBookUpdatesAsync(IEnumerable<string> assets, Action<DataEvent<KucoinStreamFundingBookUpdate>> onData, CancellationToken ct = default)
        {
            foreach (var asset in assets)
                asset.ValidateNotNull(asset);

            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                var data = _baseClient.GetData<KucoinStreamFundingBookUpdate>(tokenData);
                KucoinSocketClient.InvokeHandler(tokenData.As(data, data.Asset), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", "/margin/fundingBook:" + string.Join(",", assets), false);
            return await _baseClient.SubscribeInternalAsync(this, "spot", request, null, false, innerHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToMatchEngineUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamMatchEngineUpdate>> onData, CancellationToken ct = default) => SubscribeToMatchEngineUpdatesAsync(new[] { symbol }, onData, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMatchEngineUpdatesAsync(IEnumerable<string> symbols,
            Action<DataEvent<KucoinStreamMatchEngineUpdate>> onData, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));
            foreach (var symbol in symbols)
                symbol.ValidateKucoinSymbol();

            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                KucoinStreamMatchEngineUpdate? data;
                var subject = tokenData.Data["subject"]?.ToString();
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
                    default:
                        _log.Write(LogLevel.Warning, "Unknown match engine update type: " + subject);
                        return;
                }

                KucoinSocketClient.InvokeHandler(tokenData.As(data, data.Symbol), onData!);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", "/spotMarket/level3:" + string.Join(",", symbols), false);
            return await _baseClient.SubscribeInternalAsync(this, "spot", request, null, false, innerHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<DataEvent<KucoinStreamOrderBaseUpdate>> onOrderData,
            Action<DataEvent<KucoinStreamOrderMatchUpdate>> onTradeData, CancellationToken ct = default)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {

                if (tokenData.Data["data"] == null || tokenData.Data["data"]!["type"] == null)
                {
                    _log.Write(LogLevel.Warning, "Order update without type value");
                    return;
                }


                var type = tokenData.Data["data"]!["type"]!.ToString();
                switch (type)
                {
                    case "canceled":
                    case "open":
                    case "filled":
                    case "update":
                        var orderData = _baseClient.GetData<KucoinStreamOrderBaseUpdate>(tokenData);
                        KucoinSocketClient.InvokeHandler(tokenData.As(orderData, orderData.Symbol), onOrderData!);
                        break;
                    case "match":
                        var tradeData = _baseClient.GetData<KucoinStreamOrderMatchUpdate>(tokenData);
                        KucoinSocketClient.InvokeHandler(tokenData.As(tradeData, tradeData.Symbol), onTradeData!);
                        break;
                }
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", "/spotMarket/tradeOrders", true);
            return await _baseClient.SubscribeInternalAsync(this, "spot", request, null, true, innerHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(Action<DataEvent<KucoinBalanceUpdate>> onBalanceChange, CancellationToken ct = default)
        {
            var innerHandler = new Action<DataEvent<JToken>>(data =>
            {
                var desResult = _baseClient.DeserializeInternal<KucoinUpdateMessage<KucoinBalanceUpdate>>(data.Data);
                if (!desResult)
                {
                    _log.Write(LogLevel.Warning, "Failed to DeserializeInternal balance update: " + desResult.Error);
                    return;
                }
                onBalanceChange(data.As(desResult.Data.Data, desResult.Data.Data.Asset));
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", "/account/balance", true);
            return await _baseClient.SubscribeInternalAsync(this, "spot", request, null, true, innerHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToStopOrderUpdatesAsync(Action<DataEvent<KucoinStreamStopOrderUpdateBase>> onData, CancellationToken ct = default)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                var data = _baseClient.GetData<KucoinStreamStopOrderUpdate>(tokenData);
                KucoinSocketClient.InvokeHandler(tokenData.As((KucoinStreamStopOrderUpdateBase)data, data.Symbol), onData);
            });

            var request = new KucoinRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), "subscribe", "/spotMarket/advancedOrders", true);
            return await _baseClient.SubscribeInternalAsync(this, "spot", request, null, true, innerHandler, ct).ConfigureAwait(false);
        }
        #endregion

    }
}
