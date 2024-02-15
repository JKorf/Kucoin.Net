﻿using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Objects;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Threading;
using Kucoin.Net.Objects.Internal;
using Kucoin.Net.Objects.Models;
using Kucoin.Net.Objects.Models.Futures;
using Kucoin.Net.Objects.Models.Futures.Socket;
using Kucoin.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net.Authentication;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using System.Linq;
using Kucoin.Net.Objects.Options;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Sockets.MessageParsing;
using CryptoExchange.Net.Sockets.MessageParsing.Interfaces;
using Kucoin.Net.Objects.Sockets.Queries;
using Kucoin.Net.Objects.Sockets.Subscriptions;
using CryptoExchange.Net.Objects.Sockets;
using System.Collections.Generic;

namespace Kucoin.Net.Clients.FuturesApi
{
    /// <inheritdoc cref="IKucoinSocketClientFuturesApi" />
    public class KucoinSocketClientFuturesApi : SocketApiClient, IKucoinSocketClientFuturesApi
    {
        private static readonly MessagePath _idPath = MessagePath.Get().Property("id");
        private static readonly MessagePath _typePath = MessagePath.Get().Property("type");
        private static readonly MessagePath _topicPath = MessagePath.Get().Property("topic");

        private readonly KucoinSocketClient _baseClient;

        internal KucoinSocketClientFuturesApi(ILogger logger, KucoinSocketClient baseClient, KucoinSocketOptions options)
            : base(logger, options.Environment.FuturesAddress, options, options.FuturesOptions)
        {
            _baseClient = baseClient;

            AddSystemSubscription(new KucoinWelcomeSubscription(_logger));
            RegisterPeriodicQuery("Ping", TimeSpan.FromSeconds(30), x => new KucoinPingQuery(DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow).ToString()), null);
        }

        /// <inheritdoc />
        public override string GetListenerIdentifier(IMessageAccessor message)
        {
            var type = message.GetValue<string>(_typePath);
            if (type == "welcome")
                return type;

            var id = message.GetValue<string>(_idPath);
            if (type != "message" && id != null)
                return id;

            return message.GetValue<string>(_topicPath)!;
        }

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new KucoinAuthenticationProvider((KucoinApiCredentials)credentials);

        /// <inheritdoc />
        protected override Query? GetAuthenticationRequest() => null;

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamFuturesMatch>> onData, CancellationToken ct = default)
            => SubscribeToTradeUpdatesAsync(new[] { symbol }, onData, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamFuturesMatch>> onData, CancellationToken ct = default)
        {
            var subscription = new KucoinSubscription<KucoinStreamFuturesMatch>(_logger, "/contractMarket/execution", symbols.ToList(), onData, false);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamFuturesTick>> onData, CancellationToken ct = default)
            => SubscribeToTickerUpdatesAsync(new[] { symbol }, onData, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamFuturesTick>> onData, CancellationToken ct = default)
        {
            var subscription = new KucoinSubscription<KucoinStreamFuturesTick>(_logger, "/contractMarket/tickerV2", symbols.ToList(), onData, false);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<KucoinFuturesOrderBookChange>> onData, CancellationToken ct = default)
            => SubscribeToOrderBookUpdatesAsync(new[] { symbol }, onData, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinFuturesOrderBookChange>> onData, CancellationToken ct = default)
        {
            var subscription = new KucoinSubscription<KucoinFuturesOrderBookChange>(_logger, "/contractMarket/level2", symbols.ToList(), onData, false);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int limit, Action<DataEvent<KucoinStreamOrderBookChanged>> onData, CancellationToken ct = default)
            => SubscribeToPartialOrderBookUpdatesAsync(new[] { symbol }, limit, onData, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(IEnumerable<string> symbols, int limit, Action<DataEvent<KucoinStreamOrderBookChanged>> onData, CancellationToken ct = default)
        {
            limit.ValidateIntValues(nameof(limit), 5, 50);

            var subscription = new KucoinSubscription<KucoinStreamOrderBookChanged>(_logger, $"/contractMarket/level2Depth{limit}", symbols.ToList(), onData, false);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToMarketUpdatesAsync(string symbol,
            Action<DataEvent<KucoinStreamFuturesMarkIndexPrice>> onMarkIndexPriceUpdate,
            Action<DataEvent<KucoinStreamFuturesFundingRate>> onFundingRateUpdate, CancellationToken ct = default)
            => SubscribeToMarketUpdatesAsync(new[] { symbol }, onMarkIndexPriceUpdate, onFundingRateUpdate, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarketUpdatesAsync(IEnumerable<string> symbols,
            Action<DataEvent<KucoinStreamFuturesMarkIndexPrice>> onMarkIndexPriceUpdate,
            Action<DataEvent<KucoinStreamFuturesFundingRate>> onFundingRateUpdate,
            CancellationToken ct = default)
        {
            var subscription = new KucoinInstrumentSubscription(_logger, symbols.ToList(), onMarkIndexPriceUpdate, onFundingRateUpdate);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToSystemAnnouncementsAsync(Action<DataEvent<KucoinContractAnnouncement>> onData, CancellationToken ct = default)
        {
            var subscription = new KucoinFundingFeeSettlementSubscription(_logger, onData);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeTo24HourSnapshotUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamTransactionStatisticsUpdate>> onData, CancellationToken ct = default)
            => SubscribeTo24HourSnapshotUpdatesAsync(new[] { symbol }, onData, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeTo24HourSnapshotUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamTransactionStatisticsUpdate>> onData, CancellationToken ct = default)
        {
            var subscription = new KucoinSubscription<KucoinStreamTransactionStatisticsUpdate>(_logger, $"/contractMarket/snapshot", symbols.ToList(), onData, false);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string? symbol,
            Action<DataEvent<KucoinStreamFuturesOrderUpdate>> onData,
            CancellationToken ct = default)
        {
            var subscription = new KucoinSubscription<KucoinStreamFuturesOrderUpdate>(_logger, $"/contractMarket/tradeOrders", symbol != null ? new List<string> { symbol }: null, onData, true);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToStopOrderUpdatesAsync(Action<DataEvent<KucoinStreamStopOrderUpdateBase>> onData, CancellationToken ct = default)
        {
            var subscription = new KucoinSubscription<KucoinStreamStopOrderUpdateBase>(_logger, $"/contractMarket/advancedOrders", null, onData, true);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(
            Action<DataEvent<KucoinStreamOrderMarginUpdate>>? onOrderMarginUpdate = null,
            Action<DataEvent<KucoinStreamFuturesBalanceUpdate>>? onBalanceUpdate = null,
            Action<DataEvent<KucoinStreamFuturesWithdrawableUpdate>>? onWithdrawableUpdate = null,
            CancellationToken ct = default)
        {
            var subscription = new KucoinBalanceSubscription(_logger, onOrderMarginUpdate, onBalanceUpdate, onWithdrawableUpdate);
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
            var subscription = new KucoinPositionSubscription(_logger, symbol, onPositionUpdate, onMarkPriceUpdate, onFundingSettlementUpdate, onRiskAdjustUpdate);
            return await SubscribeAsync("futures", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        protected override async Task<CallResult<string?>> GetConnectionUrlAsync(string address, bool authenticated)
        {
            var apiCredentials = (KucoinApiCredentials?)(ApiOptions.ApiCredentials ?? _baseClient.ClientOptions.ApiCredentials ?? KucoinSocketOptions.Default.ApiCredentials ?? KucoinRestOptions.Default.ApiCredentials);
            using (var restClient = new KucoinRestClient((options) =>
            {
                options.ApiCredentials = apiCredentials;
            }))
            {
                WebCallResult<KucoinToken> tokenResult = await ((KucoinRestClientFuturesApiAccount)restClient.FuturesApi.Account).GetWebsocketToken(authenticated).ConfigureAwait(false);
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

            return new Uri(result.Data);
        }
    }
}
