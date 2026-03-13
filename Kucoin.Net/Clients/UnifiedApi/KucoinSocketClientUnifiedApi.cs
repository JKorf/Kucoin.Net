using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Interfaces;
using Kucoin.Net.Clients.MessageHandlers;
using Kucoin.Net.Clients.UnifiedApi;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Objects.Internal;
using Kucoin.Net.Objects.Models;
using Kucoin.Net.Objects.Models.Futures.Socket;
using Kucoin.Net.Objects.Models.Spot;
using Kucoin.Net.Objects.Models.Spot.Socket;
using Kucoin.Net.Objects.Models.Unified;
using Kucoin.Net.Objects.Options;
using Kucoin.Net.Objects.Sockets;
using Kucoin.Net.Objects.Sockets.Queries;
using Kucoin.Net.Objects.Sockets.Subscriptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Clients.SpotApi
{
    /// <inheritdoc cref="IKucoinSocketClientSpotApi" />
    internal partial class KucoinSocketClientUnifiedApi : SocketApiClient<KucoinEnvironment, KucoinAuthenticationProvider, KucoinCredentials>, IKucoinSocketClientUnifiedApi
    {
        private readonly KucoinSocketClient _baseClient;

        /// <inheritdoc />
        public new KucoinSocketOptions ClientOptions => (KucoinSocketOptions)base.ClientOptions;

        internal KucoinSocketClientUnifiedApi(ILogger logger, KucoinSocketClient baseClient, KucoinSocketOptions options)
            : base(logger, options.Environment.SpotAddress, options, options.SpotOptions)
        {
            _baseClient = baseClient;

            RateLimiter = KucoinExchange.RateLimiter.UnifiedSocket;

            AddSystemSubscription(new KucoinUnifiedWelcomeSubscription(this, _logger));
        }

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(KucoinExchange.SerializerContext));
        public override ISocketMessageHandler CreateMessageConverter(WebSocketMessageType messageType) => new KucoinSocketUnifiedMessageHandler();

        /// <inheritdoc />
        protected override KucoinAuthenticationProvider CreateAuthenticationProvider(KucoinCredentials credentials)
            => new KucoinAuthenticationProvider(credentials);
        
        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
            => KucoinExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(
            UnifiedAccountType tradeType,
            string symbol,
            Action<DataEvent<KucoinUaTickerUpdate>> onData,
            CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, KucoinUnifiedSocketUpdate<KucoinUaTickerUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.PushTime);

                onData.Invoke(
                    new DataEvent<KucoinUaTickerUpdate>(KucoinExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Type)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.UpdateTime, GetTimeOffset())
                    );
            });
            var subscription = new KucoinUnifiedSubscription<KucoinUaTickerUpdate>(_logger, this, "ticker", tradeType, symbol, internalHandler, false);
            return await SubscribeAsync(GetConnectionUrl(tradeType), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(
            UnifiedAccountType tradeType, string symbol, 
            KlineInterval interval,
            Action<DataEvent<KucoinUaKlineUpdate>> onData, 
            CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, KucoinUnifiedSocketUpdate<KucoinUaKlineUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.PushTime);

                onData.Invoke(
                    new DataEvent<KucoinUaKlineUpdate>(KucoinExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Type)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.PushTime, GetTimeOffset())
                    );
            });
            var subscription = new KucoinUnifiedSubscription<KucoinUaKlineUpdate>(_logger, this, "kline", tradeType, symbol, internalHandler, false, interval);
            return await SubscribeAsync(GetConnectionUrl(tradeType), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(
            UnifiedAccountType tradeType,
            string symbol,
            OrderBookDepth depth,
            Action<DataEvent<KucoinUaOrderBookUpdate>> onData,
            CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, KucoinUnifiedSocketUpdate<KucoinUaOrderBookUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.PushTime);

                onData.Invoke(
                    new DataEvent<KucoinUaOrderBookUpdate>(KucoinExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Type)
                        .WithUpdateType(data.PushType == "snapshot" ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.UpdateTime, GetTimeOffset())
                    );
            });
            var subscription = new KucoinUnifiedSubscription<KucoinUaOrderBookUpdate>(_logger, this, "obu", tradeType, symbol, internalHandler, false, depth: depth);
            return await SubscribeAsync(GetConnectionUrl(tradeType), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(
            UnifiedAccountType tradeType,
            string symbol,
            Action<DataEvent<KucoinUaTradeUpdate>> onData,
            CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, KucoinUnifiedSocketUpdate<KucoinUaTradeUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.PushTime);

                onData.Invoke(
                    new DataEvent<KucoinUaTradeUpdate>(KucoinExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Type)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.UpdateTime, GetTimeOffset())
                    );
            });
            var subscription = new KucoinUnifiedSubscription<KucoinUaTradeUpdate>(_logger, this, "trade", tradeType, symbol, internalHandler, false);
            return await SubscribeAsync(GetConnectionUrl(tradeType), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(
            UnifiedAccountType tradeType,
            Action<DataEvent<KucoinUaBalanceUpdate>> onData,
            CancellationToken ct = default)
        {
            var subscription = new KucoinUnifiedBalanceSubscription(_logger, this, tradeType, onData);
            return await SubscribeAsync(GetConnectionUrl(tradeType), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(
            UnifiedAccountType tradeType,
            Action<DataEvent<KucoinUaOrderUpdate>> onData,
            CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, KucoinUnifiedSocketUpdate<KucoinUaOrderUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.PushTime);

                onData.Invoke(
                    new DataEvent<KucoinUaOrderUpdate>(KucoinExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Type)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Data.UpdateTime, GetTimeOffset())
                    );
            });
            var subscription = new KucoinUnifiedSubscription<KucoinUaOrderUpdate>(_logger, this, "orderAll", tradeType, null, internalHandler, true);
            return await SubscribeAsync(GetConnectionUrl(tradeType), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(
            UnifiedAccountType tradeType,
            Action<DataEvent<KucoinUaUserTradeUpdate>> onData,
            CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, KucoinUnifiedSocketUpdate<KucoinUaUserTradeUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.PushTime);

                onData.Invoke(
                    new DataEvent<KucoinUaUserTradeUpdate>(KucoinExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Type)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Data.TradeTime, GetTimeOffset())
                    );
            });
            var subscription = new KucoinUnifiedSubscription<KucoinUaUserTradeUpdate>(_logger, this, "execution", tradeType, null, internalHandler, true);
            return await SubscribeAsync(GetConnectionUrl(tradeType), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(
            UnifiedAccountType tradeType,
            Action<DataEvent<KucoinUaPositionUpdate>> onData,
            CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, KucoinUnifiedSocketUpdate<KucoinUaPositionUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.PushTime);

                onData.Invoke(
                    new DataEvent<KucoinUaPositionUpdate>(KucoinExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Type)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Data.UpdateTime, GetTimeOffset())
                    );
            });
            var subscription = new KucoinUnifiedSubscription<KucoinUaPositionUpdate>(_logger, this, "positionAll", tradeType, null, internalHandler, true);
            return await SubscribeAsync(GetConnectionUrl(tradeType), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToLeverageUpdatesAsync(
            UnifiedAccountType tradeType,
            Action<DataEvent<KucoinUaLeverageUpdate>> onData,
            CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, KucoinUnifiedSocketUpdate<KucoinUaLeverageUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.PushTime);

                onData.Invoke(
                    new DataEvent<KucoinUaLeverageUpdate>(KucoinExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Type)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.PushTime, GetTimeOffset())
                    );
            });
            var subscription = new KucoinUnifiedSubscription<KucoinUaLeverageUpdate>(_logger, this, "leverage", tradeType, null, internalHandler, true);
            return await SubscribeAsync(GetConnectionUrl(tradeType), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToLiquidationWarningUpdatesAsync(
            UnifiedAccountType tradeType,
            Action<DataEvent<KucoinUaLiquidationWarningUpdate>> onData,
            CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, KucoinUnifiedSocketUpdate<KucoinUaLiquidationWarningUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.PushTime);

                onData.Invoke(
                    new DataEvent<KucoinUaLiquidationWarningUpdate>(KucoinExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Type)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Data.UpdateTime, GetTimeOffset())
                    );
            });
            var subscription = new KucoinUnifiedSubscription<KucoinUaLiquidationWarningUpdate>(_logger, this, "lw", tradeType, null, internalHandler, true);
            return await SubscribeAsync(GetConnectionUrl(tradeType), subscription, ct).ConfigureAwait(false);
        }

        private string GetConnectionUrl(UnifiedAccountType type) => 
            type == UnifiedAccountType.Futures 
                ? ClientOptions.Environment.UnifiedSocketFuturesAddress 
                : ClientOptions.Environment.UnifiedSocketSpotAddress;

        /// <inheritdoc />
        protected override async Task<CallResult<string?>> GetConnectionUrlAsync(string address, bool authenticated)
        {
            if (ClientOptions.Environment.Name == "UnitTesting")
                return new CallResult<string?>(address);

            if (!authenticated)
                return new CallResult<string?>(address);

            using (var restClient = new KucoinRestClient((options) =>
            {
                options.ApiCredentials = ApiCredentials;
                options.Environment = ClientOptions.Environment;
            }))
            {
                var tokenResult = await ((KucoinRestClientUnifiedApiAccount)restClient.UnifiedApi.Account).GetWebsocketTokenPrivateAsync().ConfigureAwait(false);
                if (!tokenResult)
                    return tokenResult.As<string?>(null);

                return new CallResult<string?>(ClientOptions.Environment.UnifiedSocketPrivateAddress + "?token=" + tokenResult.Data.Token);
            }
        }

        /// <inheritdoc />
        protected override async Task<Uri?> GetReconnectUriAsync(ISocketConnection connection)
        {
            var result = await GetConnectionUrlAsync(connection.ConnectionUri.ToString(), connection.HasAuthenticatedSubscription).ConfigureAwait(false);
            if (!result)
                return null;

            return new Uri(result.Data!);
        }
    }
}
