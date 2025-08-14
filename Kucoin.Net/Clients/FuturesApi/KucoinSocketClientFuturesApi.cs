using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Enums;
using Kucoin.Net.Objects;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Threading;
using Kucoin.Net.Objects.Internal;
using Kucoin.Net.Objects.Models.Futures;
using Kucoin.Net.Objects.Models.Futures.Socket;
using Kucoin.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net.Authentication;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using System.Linq;
using Kucoin.Net.Objects.Options;
using Kucoin.Net.Objects.Sockets.Queries;
using Kucoin.Net.Objects.Sockets.Subscriptions;
using CryptoExchange.Net.Objects.Sockets;
using System.Collections.Generic;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.SharedApis;
using System.Net.WebSockets;
using CryptoExchange.Net.Objects.Errors;

namespace Kucoin.Net.Clients.FuturesApi
{
    /// <inheritdoc cref="IKucoinSocketClientFuturesApi" />
    internal partial class KucoinSocketClientFuturesApi : SocketApiClient, IKucoinSocketClientFuturesApi
    {
        private static readonly MessagePath _idPath = MessagePath.Get().Property("id");
        private static readonly MessagePath _typePath = MessagePath.Get().Property("type");
        private static readonly MessagePath _topicPath = MessagePath.Get().Property("topic");
        private static readonly MessagePath _subjectPath = MessagePath.Get().Property("subject");
        private static readonly MessagePath _changeReasonPath = MessagePath.Get().Property("data").Property("changeReason");

        private readonly KucoinSocketClient _baseClient;

        /// <inheritdoc />
        public new KucoinSocketOptions ClientOptions => (KucoinSocketOptions)base.ClientOptions;

        internal KucoinSocketClientFuturesApi(ILogger logger, KucoinSocketClient baseClient, KucoinSocketOptions options)
            : base(logger, options.Environment.FuturesAddress, options, options.FuturesOptions)
        {
            _baseClient = baseClient;

            AddSystemSubscription(new KucoinWelcomeSubscription(_logger));
            RegisterPeriodicQuery(
                "Ping", 
                TimeSpan.FromSeconds(30), 
                x => new KucoinPingQuery(DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow).ToString()!),
                (connection, result) =>
                {
                    if (result.Error?.ErrorType == ErrorType.Timeout)
                    {
                        // Ping timeout, reconnect
                        _logger.LogWarning("[Sckt {SocketId}] Ping response timeout, reconnecting", connection.SocketId);
                        _ = connection.TriggerReconnectAsync();
                    }
            });
        }

        protected override IByteMessageAccessor CreateAccessor(WebSocketMessageType type) => new SystemTextJsonByteMessageAccessor(SerializerOptions.WithConverters(KucoinExchange.SerializerContext));
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(KucoinExchange.SerializerContext));

        /// <inheritdoc />
        public override string GetListenerIdentifier(IMessageAccessor message)
        {
            var type = message.GetValue<string>(_typePath);
            if (string.Equals(type, "welcome", StringComparison.Ordinal))
                return type!;

            var id = message.GetValue<string>(_idPath);
            if (!string.Equals(type, "message", StringComparison.Ordinal) && id != null)
                return id;

            var topic = message.GetValue<string>(_topicPath)!;
            if (topic.Equals("/contractAccount/wallet", StringComparison.Ordinal)
                || topic.StartsWith("/margin/position", StringComparison.Ordinal)
                || topic.StartsWith("/contract/instrument", StringComparison.Ordinal))
            {
                return topic + message.GetValue<string?>(_subjectPath);
            }

            if (topic.StartsWith("/contract/position", StringComparison.Ordinal))
            {
                var subject = message.GetValue<string?>(_subjectPath);
                if (subject?.Equals("position.change", StringComparison.Ordinal) == true)
                {
                    var changeReason = message.GetValue<string?>(_changeReasonPath);
                    if (changeReason?.Equals("markPriceChange") == true)
                        return topic + subject + changeReason;

                    return topic + subject;
                }

                return topic + subject;
            }

            return topic;
        }

        public IKucoinSocketClientFuturesApiShared SharedClient => this;

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new KucoinAuthenticationProvider(credentials);


        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
            => KucoinExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        /// <inheritdoc />
        protected override Task<Query?> GetAuthenticationRequestAsync(SocketConnection connection) => Task.FromResult<Query?>(null);

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamFuturesMatch>> onData, CancellationToken ct = default)
            => SubscribeToTradeUpdatesAsync(new[] { symbol }, onData, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamFuturesMatch>> onData, CancellationToken ct = default)
        {
            var subscription = new KucoinSubscription<KucoinStreamFuturesMatch>(_logger, this, "/contractMarket/execution", symbols.ToList(), onData, false);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<KucoinStreamFuturesKline>> onData, CancellationToken ct = default)
            => SubscribeToKlineUpdatesAsync(new[] { symbol }, interval, onData, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<KucoinStreamFuturesKline>> onData, CancellationToken ct = default)
        {
            var symbolTopics = symbols.Select(x => x + "_" + EnumConverter.GetString(interval)).ToList();
            var subscription = new KucoinSubscription<KucoinStreamFuturesKlineUpdate>(_logger, this, "/contractMarket/limitCandle", symbolTopics, x => onData(x.As(x.Data.Klines).WithSymbol(x.Data.Symbol)), false);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamFuturesTick>> onData, CancellationToken ct = default)
            => SubscribeToBookTickerUpdatesAsync(new[] { symbol }, onData, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamFuturesTick>> onData, CancellationToken ct = default)
        {
            var subscription = new KucoinSubscription<KucoinStreamFuturesTick>(_logger, this, "/contractMarket/tickerV2", symbols.ToList(), onData, false);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<KucoinFuturesOrderBookChange>> onData, CancellationToken ct = default)
            => SubscribeToOrderBookUpdatesAsync(new[] { symbol }, onData, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinFuturesOrderBookChange>> onData, CancellationToken ct = default)
        {
            var subscription = new KucoinSubscription<KucoinFuturesOrderBookChange>(_logger, this, "/contractMarket/level2", symbols.ToList(), onData, false);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int limit, Action<DataEvent<KucoinStreamOrderBookChanged>> onData, CancellationToken ct = default)
            => SubscribeToPartialOrderBookUpdatesAsync(new[] { symbol }, limit, onData, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(IEnumerable<string> symbols, int limit, Action<DataEvent<KucoinStreamOrderBookChanged>> onData, CancellationToken ct = default)
        {
            limit.ValidateIntValues(nameof(limit), 5, 50);

            var subscription = new KucoinSubscription<KucoinStreamOrderBookChanged>(_logger, this, $"/contractMarket/level2Depth{limit}", symbols.ToList(), onData, false);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToSymbolUpdatesAsync(string symbol,
            Action<DataEvent<KucoinStreamFuturesMarkIndexPrice>> onMarkIndexPriceUpdate,
            Action<DataEvent<KucoinStreamFuturesFundingRate>> onFundingRateUpdate, CancellationToken ct = default)
            => SubscribeToSymbolUpdatesAsync(new[] { symbol }, onMarkIndexPriceUpdate, onFundingRateUpdate, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToSymbolUpdatesAsync(IEnumerable<string> symbols,
            Action<DataEvent<KucoinStreamFuturesMarkIndexPrice>> onMarkIndexPriceUpdate,
            Action<DataEvent<KucoinStreamFuturesFundingRate>> onFundingRateUpdate,
            CancellationToken ct = default)
        {
            var subscription = new KucoinInstrumentSubscription(_logger, this, symbols.ToList(), onMarkIndexPriceUpdate, onFundingRateUpdate);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToFundingFeeSettlementUpdatesAsync(Action<DataEvent<KucoinContractAnnouncement>> onData, CancellationToken ct = default)
        {
            var subscription = new KucoinFundingFeeSettlementSubscription(_logger, this, onData);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeTo24HTickerUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamTransactionStatisticsUpdate>> onData, CancellationToken ct = default)
            => SubscribeTo24HourSnapshotUpdatesAsync(new[] { symbol }, onData, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeTo24HourSnapshotUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamTransactionStatisticsUpdate>> onData, CancellationToken ct = default)
        {
            var subscription = new KucoinSubscription<KucoinStreamTransactionStatisticsUpdate>(_logger, this, $"/contractMarket/snapshot", symbols.ToList(), onData, false);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string? symbol,
            Action<DataEvent<KucoinStreamFuturesOrderUpdate>> onData,
            CancellationToken ct = default)
        {
            var subscription = new KucoinSubscription<KucoinStreamFuturesOrderUpdate>(_logger, this, $"/contractMarket/tradeOrders", symbol != null ? new List<string> { symbol }: null, onData, true);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToStopOrderUpdatesAsync(Action<DataEvent<KucoinStreamFuturesStopOrderUpdate>> onData, CancellationToken ct = default)
        {
            var subscription = new KucoinSubscription<KucoinStreamFuturesStopOrderUpdate>(_logger, this, $"/contractMarket/advancedOrders", null, onData, true);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(
            Action<DataEvent<KucoinStreamOrderMarginUpdate>>? onOrderMarginUpdate = null,
            Action<DataEvent<KucoinStreamFuturesBalanceUpdate>>? onBalanceUpdate = null,
            Action<DataEvent<KucoinStreamFuturesWithdrawableUpdate>>? onWithdrawableUpdate = null,
            Action<DataEvent<KucoinStreamFuturesWalletUpdate>>? onWalletUpdate = null,
            CancellationToken ct = default)
        {
            var subscription = new KucoinBalanceSubscription(_logger, this, onOrderMarginUpdate, onBalanceUpdate, onWithdrawableUpdate, onWalletUpdate);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(
            string symbol,
            Action<DataEvent<KucoinPositionUpdate>>? onPositionUpdate = null,
            Action<DataEvent<KucoinPositionMarkPriceUpdate>>? onMarkPriceUpdate = null,
            Action<DataEvent<KucoinPositionFundingSettlementUpdate>>? onFundingSettlementUpdate = null,
            Action<DataEvent<KucoinPositionRiskAdjustResultUpdate>>? onRiskAdjustUpdate = null,
            CancellationToken ct = default)
        {
            var subscription = new KucoinPositionSubscription(_logger, this, symbol, onPositionUpdate, onMarkPriceUpdate, onFundingSettlementUpdate, onRiskAdjustUpdate);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(
            Action<DataEvent<KucoinPositionUpdate>>? onPositionUpdate = null,
            Action<DataEvent<KucoinPositionMarkPriceUpdate>>? onMarkPriceUpdate = null,
            Action<DataEvent<KucoinPositionFundingSettlementUpdate>>? onFundingSettlementUpdate = null,
            Action<DataEvent<KucoinPositionRiskAdjustResultUpdate>>? onRiskAdjustUpdate = null,
            CancellationToken ct = default)
        {
            var subscription = new KucoinPositionSubscription(_logger, this, null, onPositionUpdate, onMarkPriceUpdate, onFundingSettlementUpdate, onRiskAdjustUpdate);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarginModeUpdatesAsync(Action<DataEvent<Dictionary<string, FuturesMarginMode>>> onData, CancellationToken ct = default)
        {
            var subscription = new KucoinSubscription<Dictionary<string, FuturesMarginMode>>(_logger, this, $"/contract/marginMode", null, onData, true);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToCrossMarginLeverageUpdatesAsync(Action<DataEvent<Dictionary<string, KucoinLeverageUpdate>>> onData, CancellationToken ct = default)
        {
            var subscription = new KucoinSubscription<Dictionary<string, KucoinLeverageUpdate>>(_logger, this, $"/contract/crossLeverage", null, onData, true);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        protected override async Task<CallResult<string?>> GetConnectionUrlAsync(string address, bool authenticated)
        {
            if (ClientOptions.Environment.Name == "UnitTesting")
                return new CallResult<string?>("wss://ws-api-spot.kucoin.com");

            using (var restClient = new KucoinRestClient((options) =>
            {
                options.ApiCredentials = ApiCredentials;
                options.Environment = ClientOptions.Environment;
            }))
            {
                WebCallResult<KucoinToken> tokenResult;
                if (authenticated)
                    tokenResult = await ((KucoinRestClientFuturesApiAccount)restClient.FuturesApi.Account).GetWebsocketTokenPrivateAsync().ConfigureAwait(false);
                else
                    tokenResult = await ((KucoinRestClientFuturesApiAccount)restClient.FuturesApi.Account).GetWebsocketTokenPublicAsync().ConfigureAwait(false);
                if (!tokenResult)
                    return tokenResult.As<string?>(null);

                return new CallResult<string?>(tokenResult.Data.Servers.First().Endpoint + "?token=" + tokenResult.Data.Token);
            }
        }

        /// <inheritdoc />
        protected override async Task<Uri?> GetReconnectUriAsync(SocketConnection connection)
        {
            var result = await GetConnectionUrlAsync(connection.ConnectionUri.ToString(), connection.Subscriptions.Any(s => s.Authenticated)).ConfigureAwait(false);
            if (!result)
                return null;

            return new Uri(result.Data!);
        }
    }
}
