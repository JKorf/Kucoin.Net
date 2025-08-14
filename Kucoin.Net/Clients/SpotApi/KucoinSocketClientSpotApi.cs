using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Objects;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kucoin.Net.Enums;
using System.Threading;
using Kucoin.Net.Objects.Internal;
using Kucoin.Net.Objects.Models;
using Kucoin.Net.Objects.Models.Futures.Socket;
using Kucoin.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net.Authentication;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using System.Linq;
using Kucoin.Net.Objects.Options;
using CryptoExchange.Net.Objects.Sockets;
using Kucoin.Net.Objects.Sockets.Subscriptions;
using Kucoin.Net.Objects.Sockets.Queries;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.SharedApis;
using Kucoin.Net.Objects.Models.Spot;
using System.Net.WebSockets;
using CryptoExchange.Net.Objects.Errors;

namespace Kucoin.Net.Clients.SpotApi
{
    /// <inheritdoc cref="IKucoinSocketClientSpotApi" />
    internal partial class KucoinSocketClientSpotApi : SocketApiClient, IKucoinSocketClientSpotApi
    {
        private readonly KucoinSocketClient _baseClient;
        private static readonly MessagePath _idPath = MessagePath.Get().Property("id");
        private static readonly MessagePath _typePath = MessagePath.Get().Property("type");
        private static readonly MessagePath _topicPath = MessagePath.Get().Property("topic");
        private static readonly MessagePath _subjectPath = MessagePath.Get().Property("subject");
        private static readonly MessagePath _orderEventTypePath = MessagePath.Get().Property("data").Property("type");

        /// <inheritdoc />
        public new KucoinSocketOptions ClientOptions => (KucoinSocketOptions)base.ClientOptions;

        internal KucoinSocketClientSpotApi(ILogger logger, KucoinSocketClient baseClient, KucoinSocketOptions options)
            : base(logger, options.Environment.SpotAddress, options, options.SpotOptions)
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
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new KucoinAuthenticationProvider(credentials);
        
        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
            => KucoinExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        public IKucoinSocketClientSpotApiShared SharedClient => this;

        /// <inheritdoc />
        public override string GetListenerIdentifier(IMessageAccessor message)
        {
            var type = message.GetValue<string>(_typePath);
            if (string.Equals(type, "welcome", StringComparison.Ordinal))
                return type!;

            var topic = message.GetValue<string>(_topicPath);
            var id = message.GetValue<string>(_idPath);
            if (id != null)
            {
                if (string.Equals(topic, "/account/balance", StringComparison.Ordinal)
                    || topic?.StartsWith("/margin/fundingBook", StringComparison.Ordinal) == true)
                {
                    // This update also contain an id field, but should be identified by the topic regardless
                    return topic!;
                }

                return id;
            }

            if (topic!.StartsWith("/margin/loan", StringComparison.Ordinal)
             || topic!.StartsWith("/margin/position", StringComparison.Ordinal))
                {
                return topic + message.GetValue<string?>(_subjectPath);
            }

            if (topic.Equals("/spotMarket/tradeOrdersV2"))
            {
                return topic + message.GetValue<string?>(_orderEventTypePath);
            }

            return topic;
        }

        /// <inheritdoc />
        protected override Task<Query?> GetAuthenticationRequestAsync(SocketConnection connection) => Task.FromResult<Query?>(null);

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamTick>> onData, CancellationToken ct = default) => SubscribeToTickerUpdatesAsync(new[] { symbol }, onData, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamTick>> onData, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var subscription = new KucoinSubscription<KucoinStreamTick>(_logger, this, "/market/ticker", symbols.ToList(), x => onData(x.WithDataTimestamp(x.Data.Timestamp)), false);
            return await SubscribeAsync("spot", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllTickerUpdatesAsync(Action<DataEvent<KucoinStreamTick>> onData, CancellationToken ct = default)
        {
            var subscription = new KucoinSubscription<KucoinStreamTick>(_logger, this, "/market/ticker:all", null, x => onData(x.WithDataTimestamp(x.Data.Timestamp)), false);
            return await SubscribeAsync("spot", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToSnapshotUpdatesAsync(string symbolOrMarket,
            Action<DataEvent<KucoinStreamSnapshot>> onData, CancellationToken ct = default)
            => SubscribeToSnapshotUpdatesAsync(new[] { symbolOrMarket }, onData, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToSnapshotUpdatesAsync(
            IEnumerable<string> symbolOrMarkets, Action<DataEvent<KucoinStreamSnapshot>> onData, CancellationToken ct = default)
        {
            var subscription = new KucoinSubscription<KucoinStreamSnapshotWrapper>(_logger, this, "/market/snapshot", symbolOrMarkets.ToList(), x => onData(x.As(x.Data.Data).WithDataTimestamp(x.Data.Data.Timestamp)), false);
            return await SubscribeAsync("spot", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamBestOffers>> onData, CancellationToken ct = default) => SubscribeToBookTickerUpdatesAsync(new[] { symbol }, onData, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamBestOffers>> onData, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var subscription = new KucoinSubscription<KucoinStreamBestOffers>(_logger, this, "/spotMarket/level1", symbols.ToList(), x => onData(x.WithDataTimestamp(x.Data.Timestamp)), false);
            return await SubscribeAsync("spot", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToAggregatedOrderBookUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamOrderBook>> onData, CancellationToken ct = default) => SubscribeToAggregatedOrderBookUpdatesAsync(new[] { symbol }, onData, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAggregatedOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamOrderBook>> onData, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var subscription = new KucoinSubscription<KucoinStreamOrderBook>(_logger, this, "/market/level2", symbols.ToList(), x => onData(x.WithDataTimestamp(x.Data.Timestamp)), false);
            return await SubscribeAsync("spot", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamMatch>> onData, CancellationToken ct = default) => SubscribeToTradeUpdatesAsync(new[] { symbol }, onData, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamMatch>> onData, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var subscription = new KucoinSubscription<KucoinStreamMatch>(_logger, this, "/market/match", symbols.ToList(), x => onData(x.WithDataTimestamp(x.Data.Timestamp)), false);
            return await SubscribeAsync("spot", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<KucoinStreamCandle>> onData, CancellationToken ct = default)
            => SubscribeToKlineUpdatesAsync([symbol], interval, onData, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<KucoinStreamCandle>> onData, CancellationToken ct = default)
        {
            var subscription = new KucoinSubscription<KucoinStreamCandle>(_logger, this, $"/market/candles", symbols.Select(x => $"{x}_{EnumConverter.GetString(interval)}").ToList(), x => onData(x.WithDataTimestamp(x.Data.Timestamp)), false);

            return await SubscribeAsync("spot", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int limit,
            Action<DataEvent<KucoinStreamOrderBookChanged>> onData, CancellationToken ct = default) =>
            SubscribeToOrderBookUpdatesAsync(new[] { symbol }, limit, onData, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int limit, Action<DataEvent<KucoinStreamOrderBookChanged>> onData, CancellationToken ct = default)
        {
            limit.ValidateIntValues(nameof(limit), 5, 50);

            var subscription = new KucoinSubscription<KucoinStreamOrderBookChanged>(_logger, this, $"/spotMarket/level2Depth{limit}", symbols.ToList(), x => onData(x.WithDataTimestamp(x.Data.Timestamp)), false);
            return await SubscribeAsync("spot", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamIndicatorPrice>> onData, CancellationToken ct = default) => SubscribeToIndexPriceUpdatesAsync(new string[] { symbol }, onData, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamIndicatorPrice>> onData, CancellationToken ct = default)
        {
            var subscription = new KucoinSubscription<KucoinStreamIndicatorPrice>(_logger, this, $"/indicator/index", symbols.ToList(), x => onData(x.WithDataTimestamp(x.Data.Timestamp)), false);
            return await SubscribeAsync("spot", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamIndicatorPrice>> onData, CancellationToken ct = default) => SubscribeToMarkPriceUpdatesAsync(new string[] { symbol }, onData, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamIndicatorPrice>> onData, CancellationToken ct = default)
        {
            var subscription = new KucoinSubscription<KucoinStreamIndicatorPrice>(_logger, this, $"/indicator/markPrice", symbols.ToList(), x => onData(x.WithDataTimestamp(x.Data.Timestamp)), false);
            return await SubscribeAsync("spot", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToCallAuctionOrderBookUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamOrderBook>> onData, CancellationToken ct = default) => SubscribeToCallAuctionOrderBookUpdatesAsync(new string[] { symbol }, onData, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToCallAuctionOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamOrderBook>> onData, CancellationToken ct = default)
        {
            var subscription = new KucoinSubscription<KucoinStreamOrderBook>(_logger, this, $"/callauction/level2Depth50", symbols.ToList(), onData, false);
            return await SubscribeAsync("spot", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToCallAuctionInfoUpdatesAsync(string symbol, Action<DataEvent<KucoinCallAuctionInfo>> onData, CancellationToken ct = default) => SubscribeToCallAuctionInfoUpdatesAsync(new string[] { symbol }, onData, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToCallAuctionInfoUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinCallAuctionInfo>> onData, CancellationToken ct = default)
        {
            var subscription = new KucoinSubscription<KucoinCallAuctionInfo>(_logger, this, $"/callauction/callauctionData", symbols.ToList(), onData, false);
            return await SubscribeAsync("spot", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(
            Action<DataEvent<KucoinStreamOrderNewUpdate>>? onNewOrder = null,
            Action<DataEvent<KucoinStreamOrderUpdate>>? onOrderData = null,
            Action<DataEvent<KucoinStreamOrderMatchUpdate>>? onTradeData = null,
            CancellationToken ct = default)
        {
            var subscription = new KucoinOrderSubscription(_logger, this, onNewOrder, onOrderData, onTradeData);
            return await SubscribeAsync("spot", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(Action<DataEvent<KucoinBalanceUpdate>> onBalanceChange, CancellationToken ct = default)
        {
            var subscription = new KucoinSubscription<KucoinBalanceUpdate>(_logger, this, "/account/balance", null, x => onBalanceChange(x.WithDataTimestamp(x.Data.Timestamp)), true);
            return await SubscribeAsync("spot", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToStopOrderUpdatesAsync(Action<DataEvent<KucoinStreamStopOrderUpdateBase>> onData, CancellationToken ct = default)
        {
            var subscription = new KucoinSubscription<KucoinStreamStopOrderUpdateBase>(_logger, this, "/spotMarket/advancedOrders", null, x => onData(x.WithDataTimestamp(x.Data.Timestamp)), true);
            return await SubscribeAsync("spot", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIsolatedMarginPositionUpdatesAsync(string symbol, Action<DataEvent<KucoinIsolatedMarginPositionUpdate>> onPositionChange, CancellationToken ct = default)
        {
            var subscription = new KucoinIsolatedMarginPositionSubscription(_logger, this, symbol, onPositionChange);
            return await SubscribeAsync("spot", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarginPositionUpdatesAsync(Action<DataEvent<KucoinMarginDebtRatioUpdate>> onDebtRatioChange, Action<DataEvent<KucoinMarginPositionStatusUpdate>> onPositionStatusChange, CancellationToken ct = default)
        {
            var subscription = new KucoinMarginPositionSubscription(_logger, this, onDebtRatioChange, onPositionStatusChange);
            return await SubscribeAsync("spot", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarginOrderUpdatesAsync(string symbol, Action<DataEvent<KucoinMarginOrderUpdate>>? onOrderPlaced = null, Action<DataEvent<KucoinMarginOrderUpdate>>? onOrderUpdate = null, Action<DataEvent<KucoinMarginOrderDoneUpdate>>? onOrderDone = null, CancellationToken ct = default)
        {
            var subscription = new KucoinMarginOrderSubscription(_logger, this, symbol, onOrderPlaced, onOrderUpdate, onOrderDone);
            return await SubscribeAsync("spot", subscription, ct).ConfigureAwait(false);
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
                    tokenResult = await ((KucoinRestClientSpotApiAccount)restClient.SpotApi.Account).GetWebsocketTokenPrivateAsync().ConfigureAwait(false);
                else
                    tokenResult = await ((KucoinRestClientSpotApiAccount)restClient.SpotApi.Account).GetWebsocketTokenPublicAsync().ConfigureAwait(false);
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
