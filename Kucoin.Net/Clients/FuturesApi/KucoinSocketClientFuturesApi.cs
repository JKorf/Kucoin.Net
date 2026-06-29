using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Enums;
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
using Kucoin.Net.Objects.Sockets;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using Kucoin.Net.Clients.MessageHandlers;
using CryptoExchange.Net.Sockets.Interfaces;
using CryptoExchange.Net.Sockets.Default;

namespace Kucoin.Net.Clients.FuturesApi
{
    /// <inheritdoc cref="IKucoinSocketClientFuturesApi" />
    internal partial class KucoinSocketClientFuturesApi : SocketApiClient<KucoinEnvironment, KucoinAuthenticationProvider, KucoinCredentials>, IKucoinSocketClientFuturesApi
    {
        private readonly KucoinSocketClient _baseClient;

        /// <inheritdoc />
        public new KucoinSocketOptions ClientOptions => (KucoinSocketOptions)base.ClientOptions;

        internal KucoinSocketClientFuturesApi(ILoggerFactory? loggerFactory, KucoinSocketClient baseClient, KucoinSocketOptions options)
            : base(loggerFactory, KucoinExchange.Metadata.Id, options.Environment.FuturesAddress, options, options.FuturesOptions)
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

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(KucoinExchange._serializerContext));
        public override ISocketMessageHandler CreateMessageConverter(WebSocketMessageType messageType) => new KucoinSocketFuturesMessageHandler();

        public IKucoinSocketClientFuturesApiShared SharedClient => this;

        /// <inheritdoc />
        protected override KucoinAuthenticationProvider CreateAuthenticationProvider(KucoinCredentials credentials)
            => new KucoinAuthenticationProvider(credentials);


        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
            => KucoinExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamFuturesMatch>> onData, CancellationToken ct = default)
            => SubscribeToTradeUpdatesAsync(new[] { symbol }, onData, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamFuturesMatch>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, KucoinSocketUpdate<KucoinStreamFuturesMatch>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Data.Timestamp);

                onData.Invoke(
                    new DataEvent<KucoinStreamFuturesMatch>(KucoinExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Topic)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.Timestamp, GetTimeOffset())
                    );
            });

            var subscription = new KucoinSubscription<KucoinStreamFuturesMatch>(_logger, this, "/contractMarket/execution", symbols.ToList(), internalHandler, false);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<KucoinStreamFuturesKline>> onData, CancellationToken ct = default)
            => SubscribeToKlineUpdatesAsync(new[] { symbol }, interval, onData, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<KucoinStreamFuturesKline>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, KucoinSocketUpdate<KucoinStreamFuturesKlineUpdate>>((receiveTime, originalData, data) =>
            {
                onData.Invoke(
                    new DataEvent<KucoinStreamFuturesKline>(KucoinExchange.ExchangeName, data.Data.Klines, receiveTime, originalData)
                        .WithStreamId(data.Topic)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithSymbol(data.Data.Symbol)
                    );
            });

            var symbolTopics = symbols.Select(x => x + "_" + EnumConverter.GetString(interval)).ToList();
            var subscription = new KucoinSubscription<KucoinStreamFuturesKlineUpdate>(_logger, this, "/contractMarket/limitCandle", symbolTopics, internalHandler, false);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamFuturesTick>> onData, CancellationToken ct = default)
            => SubscribeToBookTickerUpdatesAsync(new[] { symbol }, onData, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamFuturesTick>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, KucoinSocketUpdate<KucoinStreamFuturesTick>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Data.Timestamp);

                onData.Invoke(
                    new DataEvent<KucoinStreamFuturesTick>(KucoinExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Topic)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.Timestamp, GetTimeOffset())
                    );
            });

            var subscription = new KucoinSubscription<KucoinStreamFuturesTick>(_logger, this, "/contractMarket/tickerV2", symbols.ToList(), internalHandler, false);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<KucoinFuturesOrderBookChange>> onData, CancellationToken ct = default)
            => SubscribeToOrderBookUpdatesAsync(new[] { symbol }, onData, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinFuturesOrderBookChange>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, KucoinSocketUpdate<KucoinFuturesOrderBookChange>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Data.Timestamp);

                onData.Invoke(
                    new DataEvent<KucoinFuturesOrderBookChange>(KucoinExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Topic)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithSymbol(data.Symbol)
                        .WithDataTimestamp(data.Data.Timestamp, GetTimeOffset())
                        .WithSequenceNumber(data.Data.Sequence)
                    );
            });

            var subscription = new KucoinSubscription<KucoinFuturesOrderBookChange>(_logger, this, "/contractMarket/level2", symbols.ToList(), internalHandler, false);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int limit, Action<DataEvent<KucoinStreamOrderBookChanged>> onData, CancellationToken ct = default)
            => SubscribeToPartialOrderBookUpdatesAsync(new[] { symbol }, limit, onData, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(IEnumerable<string> symbols, int limit, Action<DataEvent<KucoinStreamOrderBookChanged>> onData, CancellationToken ct = default)
        {
            limit.ValidateIntValues(nameof(limit), 5, 50);

            var internalHandler = new Action<DateTime, string?, KucoinSocketUpdate<KucoinStreamOrderBookChanged>>((receiveTime, originalData, data) =>
            {
                if (data.Data.Timestamp != null)
                    UpdateTimeOffset(data.Data.Timestamp.Value);

                onData.Invoke(
                    new DataEvent<KucoinStreamOrderBookChanged>(KucoinExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Topic)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithSymbol(data.Symbol)
                        .WithDataTimestamp(data.Data.Timestamp, GetTimeOffset())
                    );
            });
            var subscription = new KucoinSubscription<KucoinStreamOrderBookChanged>(_logger, this, $"/contractMarket/level2Depth{limit}", symbols.ToList(), internalHandler, false);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToSymbolUpdatesAsync(string symbol,
            Action<DataEvent<KucoinStreamFuturesMarkIndexPrice>> onMarkIndexPriceUpdate,
            Action<DataEvent<KucoinStreamFuturesFundingRate>> onFundingRateUpdate, CancellationToken ct = default)
            => SubscribeToSymbolUpdatesAsync(new[] { symbol }, onMarkIndexPriceUpdate, onFundingRateUpdate, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToSymbolUpdatesAsync(IEnumerable<string> symbols,
            Action<DataEvent<KucoinStreamFuturesMarkIndexPrice>> onMarkIndexPriceUpdate,
            Action<DataEvent<KucoinStreamFuturesFundingRate>> onFundingRateUpdate,
            CancellationToken ct = default)
        {
            var subscription = new KucoinInstrumentSubscription(_logger, this, symbols.ToList(), onMarkIndexPriceUpdate, onFundingRateUpdate);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToFundingFeeSettlementUpdatesAsync(Action<DataEvent<KucoinContractAnnouncement>> onData, CancellationToken ct = default)
        {
            var subscription = new KucoinFundingFeeSettlementSubscription(_logger, this, onData);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeTo24HTickerUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamTransactionStatisticsUpdate>> onData, CancellationToken ct = default)
            => SubscribeTo24HourSnapshotUpdatesAsync(new[] { symbol }, onData, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeTo24HourSnapshotUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamTransactionStatisticsUpdate>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, KucoinSocketUpdate<KucoinStreamTransactionStatisticsUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Data.Timestamp);

                onData.Invoke(
                    new DataEvent<KucoinStreamTransactionStatisticsUpdate>(KucoinExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Topic)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithSymbol(data.Symbol)
                        .WithDataTimestamp(data.Data.Timestamp, GetTimeOffset())
                    );
            });

            var subscription = new KucoinSubscription<KucoinStreamTransactionStatisticsUpdate>(_logger, this, $"/contractMarket/snapshot", symbols.ToList(), internalHandler, false);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string? symbol,
            Action<DataEvent<KucoinStreamFuturesOrderUpdate>> onData,
            CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, KucoinSocketUpdate<KucoinStreamFuturesOrderUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Data.Timestamp);

                onData.Invoke(
                    new DataEvent<KucoinStreamFuturesOrderUpdate>(KucoinExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Topic)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.Timestamp, GetTimeOffset())
                    );
            });

            var subscription = new KucoinSubscription<KucoinStreamFuturesOrderUpdate>(_logger, this, $"/contractMarket/tradeOrders", symbol != null ? new List<string> { symbol }: null, internalHandler, true);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToStopOrderUpdatesAsync(Action<DataEvent<KucoinStreamFuturesStopOrderUpdate>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, KucoinSocketUpdate<KucoinStreamFuturesStopOrderUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Data.Timestamp);

                onData.Invoke(
                    new DataEvent<KucoinStreamFuturesStopOrderUpdate>(KucoinExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Topic)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.Timestamp, GetTimeOffset())
                    );
            });

            var subscription = new KucoinSubscription<KucoinStreamFuturesStopOrderUpdate>(_logger, this, $"/contractMarket/advancedOrders", null, internalHandler, true);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(
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
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(
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
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(
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
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToMarginModeUpdatesAsync(Action<DataEvent<Dictionary<string, FuturesMarginMode>>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, KucoinSocketUpdate<Dictionary<string, FuturesMarginMode>>>((receiveTime, originalData, data) =>
            {
                onData.Invoke(
                    new DataEvent<Dictionary<string, FuturesMarginMode>>(KucoinExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Topic)
                        .WithUpdateType(SocketUpdateType.Update)
                    );
            });

            var subscription = new KucoinSubscription<Dictionary<string, FuturesMarginMode>>(_logger, this, $"/contract/marginMode", null, internalHandler, true);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToCrossMarginLeverageUpdatesAsync(Action<DataEvent<Dictionary<string, KucoinLeverageUpdate>>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, KucoinSocketUpdate<Dictionary<string, KucoinLeverageUpdate>>>((receiveTime, originalData, data) =>
            {
                onData.Invoke(
                    new DataEvent<Dictionary<string, KucoinLeverageUpdate>>(KucoinExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Topic)
                        .WithUpdateType(SocketUpdateType.Update)
                    );
            });

            var subscription = new KucoinSubscription<Dictionary<string, KucoinLeverageUpdate>>(_logger, this, $"/contract/crossLeverage", null, internalHandler, true);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        protected override async Task<CallResult<string?>> GetConnectionUrlAsync(string address, bool authenticated)
        {
            if (ClientOptions.Environment.Name == "UnitTesting")
                return CallResult<string?>.Ok("wss://ws-api-futures.kucoin.com");

            using (var restClient = new KucoinRestClient((options) =>
            {
                options.ApiCredentials = ApiCredentials;
                options.Environment = ClientOptions.Environment;
            }))
            {
                HttpResult<KucoinToken> tokenResult;
                if (authenticated)
                    tokenResult = await ((KucoinRestClientFuturesApiAccount)restClient.FuturesApi.Account).GetWebsocketTokenPrivateAsync().ConfigureAwait(false);
                else
                    tokenResult = await ((KucoinRestClientFuturesApiAccount)restClient.FuturesApi.Account).GetWebsocketTokenPublicAsync().ConfigureAwait(false);
                if (!tokenResult.Success)
                    return CallResult.Fail<string?>(tokenResult.Error);

                return CallResult.Ok<string?>(tokenResult.Data.Servers.First().Endpoint + "?token=" + tokenResult.Data.Token);
            }
        }

        /// <inheritdoc />
        protected override async Task<Uri?> GetReconnectUriAsync(ISocketConnection connection)
        {
            var result = await GetConnectionUrlAsync(connection.ConnectionUri.ToString(), connection.HasAuthenticatedSubscription).ConfigureAwait(false);
            if (!result.Success)
                return null;

            return new Uri(result.Data!);
        }

        protected override async Task<CallResult<SocketConnection>> GetSocketConnection(
           string address,
           bool authenticated,
           bool dedicatedRequestConnection,
           CancellationToken ct,
           string? topic = null,
           int individualSubscriptionCount = 1)
        {
            // address is either spot or futures
            var connection = _socketConnections.Values.Where(x => x.Tag == address)
                    .OrderBy(s => s.UserSubscriptionCount)
                    .FirstOrDefault();

            bool maxConnectionsReached = _socketConnections.Count >= (ApiOptions.MaxSocketConnections ?? ClientOptions.MaxSocketConnections);
            if (connection != null)
            {
                bool lessThanBatchSubCombineTarget = connection.UserSubscriptionCount < ClientOptions.SocketSubscriptionsCombineTarget;
                bool lessThanIndividualSubCombineTarget = connection.Subscriptions.Sum(x => x.IndividualSubscriptionCount) < ClientOptions.SocketIndividualSubscriptionCombineTarget;

                if ((lessThanBatchSubCombineTarget && lessThanIndividualSubCombineTarget)
                    || maxConnectionsReached)
                {
                    // Use existing socket if it has less than target connections OR it has the least connections and we can't make new
                    // If there is a max subscriptions per connection limit also only use existing if the new subscription doesn't go over the limit
                    if (MaxIndividualSubscriptionsPerConnection == null)
                        return CallResult.Ok(connection);

                    var currentCount = connection.Subscriptions.Sum(x => x.IndividualSubscriptionCount);
                    if (currentCount + individualSubscriptionCount <= MaxIndividualSubscriptionsPerConnection)
                        return CallResult.Ok(connection);
                }
            }

            var result = await base.GetSocketConnection(address, authenticated, dedicatedRequestConnection, ct, topic, individualSubscriptionCount).ConfigureAwait(false);
            if (result.Success)
                result.Data.Tag = address;
            return result;
        }
    }
}
