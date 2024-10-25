using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Klines;
using CryptoExchange.Net.Trackers.Trades;
using Kucoin.Net.Interfaces;
using Kucoin.Net.Interfaces.Clients;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Kucoin.Net
{
    /// <inheritdoc />
    public class KucoinTrackerFactory : IKucoinTrackerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public KucoinTrackerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public IKlineTracker CreateKlineTracker(SharedSymbol symbol, SharedKlineInterval interval, int? limit = null, TimeSpan? period = null)
        {
            IKlineRestClient restClient;
            IKlineSocketClient socketClient;
            if (symbol.TradingMode == TradingMode.Spot)
            {
                restClient = _serviceProvider.GetRequiredService<IKucoinRestClient>().SpotApi.SharedClient;
                socketClient = _serviceProvider.GetRequiredService<IKucoinSocketClient>().SpotApi.SharedClient;
            }
            else
            {
                restClient = _serviceProvider.GetRequiredService<IKucoinRestClient>().FuturesApi.SharedClient;
                socketClient = _serviceProvider.GetRequiredService<IKucoinSocketClient>().FuturesApi.SharedClient;
            }

            return new KlineTracker(
                _serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
                restClient,
                socketClient,
                symbol,
                interval,
                limit,
                period
                );
        }
        /// <inheritdoc />
        public ITradeTracker CreateTradeTracker(SharedSymbol symbol, int? limit = null, TimeSpan? period = null)
        {
            IRecentTradeRestClient? restClient = null;
            ITradeSocketClient socketClient;
            if (symbol.TradingMode == TradingMode.Spot)
            {
                restClient = _serviceProvider.GetRequiredService<IKucoinRestClient>().SpotApi.SharedClient;
                socketClient = _serviceProvider.GetRequiredService<IKucoinSocketClient>().SpotApi.SharedClient;
            }
            else
            {
                restClient = _serviceProvider.GetRequiredService<IKucoinRestClient>().FuturesApi.SharedClient;
                socketClient = _serviceProvider.GetRequiredService<IKucoinSocketClient>().FuturesApi.SharedClient;
            }

            return new TradeTracker(
                _serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
                restClient,
                socketClient,
                symbol,
                limit,
                period
                );
        }
    }
}
