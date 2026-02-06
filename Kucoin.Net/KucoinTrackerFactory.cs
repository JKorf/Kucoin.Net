using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Klines;
using CryptoExchange.Net.Trackers.Trades;
using CryptoExchange.Net.Trackers.UserData.Interfaces;
using CryptoExchange.Net.Trackers.UserData.Objects;
using Kucoin.Net.Clients;
using Kucoin.Net.Interfaces;
using Kucoin.Net.Interfaces.Clients;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;

namespace Kucoin.Net
{
    /// <inheritdoc />
    public class KucoinTrackerFactory : IKucoinTrackerFactory
    {
        private readonly IServiceProvider? _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        public KucoinTrackerFactory()
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public KucoinTrackerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public bool CanCreateKlineTracker(SharedSymbol symbol, SharedKlineInterval interval)
        {
            var client = (_serviceProvider?.GetRequiredService<IKucoinSocketClient>() ?? new KucoinSocketClient());
            SubscribeKlineOptions klineOptions = symbol.TradingMode == TradingMode.Spot ? client.SpotApi.SharedClient.SubscribeKlineOptions : client.FuturesApi.SharedClient.SubscribeKlineOptions;
            return klineOptions.IsSupported(interval);
        }

        /// <inheritdoc />
        public bool CanCreateTradeTracker(SharedSymbol symbol) => true;

        /// <inheritdoc />
        public IKlineTracker CreateKlineTracker(SharedSymbol symbol, SharedKlineInterval interval, int? limit = null, TimeSpan? period = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<IKucoinRestClient>() ?? new KucoinRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IKucoinSocketClient>() ?? new KucoinSocketClient();

            IKlineRestClient sharedRestClient;
            IKlineSocketClient sharedSocketClient;
            if (symbol.TradingMode == TradingMode.Spot)
            {
                sharedRestClient = restClient.SpotApi.SharedClient;
                sharedSocketClient = socketClient.SpotApi.SharedClient;
            }
            else
            {
                sharedRestClient = restClient.FuturesApi.SharedClient;
                sharedSocketClient = socketClient.FuturesApi.SharedClient;
            }

            return new KlineTracker(
                _serviceProvider?.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
                sharedRestClient,
                sharedSocketClient,
                symbol,
                interval,
                limit,
                period
                );
        }
        /// <inheritdoc />
        public ITradeTracker CreateTradeTracker(SharedSymbol symbol, int? limit = null, TimeSpan? period = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<IKucoinRestClient>() ?? new KucoinRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IKucoinSocketClient>() ?? new KucoinSocketClient();

            IRecentTradeRestClient? sharedRestClient;
            ITradeSocketClient sharedSocketClient;
            if (symbol.TradingMode == TradingMode.Spot)
            {
                sharedRestClient = restClient.SpotApi.SharedClient;
                sharedSocketClient = socketClient.SpotApi.SharedClient;
            }
            else
            {
                sharedRestClient = restClient.FuturesApi.SharedClient;
                sharedSocketClient = socketClient.FuturesApi.SharedClient;
            }

            return new TradeTracker(
                _serviceProvider?.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
                sharedRestClient,
                null,
                sharedSocketClient,
                symbol,
                limit,
                period
                );
        }

        /// <inheritdoc />
        public IUserSpotDataTracker CreateUserSpotDataTracker(SpotUserDataTrackerConfig? config = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<IKucoinRestClient>() ?? new KucoinRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IKucoinSocketClient>() ?? new KucoinSocketClient();
            return new KucoinUserSpotDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<KucoinUserSpotDataTracker>>() ?? new NullLogger<KucoinUserSpotDataTracker>(),
                restClient,
                socketClient,
                null,
                config
                );
        }

        /// <inheritdoc />
        public IUserSpotDataTracker CreateUserSpotDataTracker(string userIdentifier, ApiCredentials credentials, SpotUserDataTrackerConfig? config = null, KucoinEnvironment? environment = null)
        {
            var clientProvider = _serviceProvider?.GetRequiredService<IKucoinUserClientProvider>() ?? new KucoinUserClientProvider();
            var restClient = clientProvider.GetRestClient(userIdentifier, credentials, environment);
            var socketClient = clientProvider.GetSocketClient(userIdentifier, credentials, environment);
            return new KucoinUserSpotDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<KucoinUserSpotDataTracker>>() ?? new NullLogger<KucoinUserSpotDataTracker>(),
                restClient,
                socketClient,
                userIdentifier,
                config
                );
        }

        /// <inheritdoc />
        public IUserFuturesDataTracker CreateUserFuturesDataTracker(FuturesUserDataTrackerConfig? config = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<IKucoinRestClient>() ?? new KucoinRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IKucoinSocketClient>() ?? new KucoinSocketClient();
            return new KucoinUserFuturesDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<KucoinUserFuturesDataTracker>>() ?? new NullLogger<KucoinUserFuturesDataTracker>(),
                restClient,
                socketClient,
                null,
                config
                );
        }

        /// <inheritdoc />
        public IUserFuturesDataTracker CreateUserFuturesDataTracker(string userIdentifier, ApiCredentials credentials, FuturesUserDataTrackerConfig? config = null, KucoinEnvironment? environment = null)
        {
            var clientProvider = _serviceProvider?.GetRequiredService<IKucoinUserClientProvider>() ?? new KucoinUserClientProvider();
            var restClient = clientProvider.GetRestClient(userIdentifier, credentials, environment);
            var socketClient = clientProvider.GetSocketClient(userIdentifier, credentials, environment);
            return new KucoinUserFuturesDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<KucoinUserFuturesDataTracker>>() ?? new NullLogger<KucoinUserFuturesDataTracker>(),
                restClient,
                socketClient,
                userIdentifier,
                config
                );
        }
    }
}
