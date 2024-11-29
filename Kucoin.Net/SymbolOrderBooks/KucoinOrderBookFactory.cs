using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.SharedApis;
using Kucoin.Net.Interfaces;
using Kucoin.Net.Interfaces.Clients;
using Kucoin.Net.Objects.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Kucoin.Net.SymbolOrderBooks
{
    /// <summary>
    /// Kucoin order book factory
    /// </summary>
    public class KucoinOrderBookFactory : IKucoinOrderBookFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <inheritdoc />
        public IOrderBookFactory<KucoinOrderBookOptions> Spot { get; }

        /// <inheritdoc />
        public IOrderBookFactory<KucoinOrderBookOptions> Futures { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public KucoinOrderBookFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            Spot = new OrderBookFactory<KucoinOrderBookOptions>(CreateSpot, Create);
            Futures = new OrderBookFactory<KucoinOrderBookOptions>(CreateFutures, Create);
        }

        /// <inheritdoc />
        public ISymbolOrderBook Create(SharedSymbol symbol, Action<KucoinOrderBookOptions>? options = null)
        {
            var symbolName = symbol.GetSymbol(KucoinExchange.FormatSymbol);
            if (symbol.TradingMode == TradingMode.Spot)
                return CreateSpot(symbolName, options);

            return CreateFutures(symbolName, options);
        }

        /// <summary>
        /// Create a spot SymbolOrderBook
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="options">Book options</param>
        /// <returns></returns>
        public ISymbolOrderBook CreateSpot(string symbol, Action<KucoinOrderBookOptions>? options = null)
            => new KucoinSpotSymbolOrderBook(symbol,
                                             options,
                                             _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                             _serviceProvider.GetRequiredService<IKucoinRestClient>(),
                                             _serviceProvider.GetRequiredService<IKucoinSocketClient>());

        /// <summary>
        /// Create a futures SymbolOrderBook
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="options">Book options</param>
        /// <returns></returns>
        public ISymbolOrderBook CreateFutures(string symbol, Action<KucoinOrderBookOptions>? options = null)
            => new KucoinFuturesSymbolOrderBook(symbol,
                                                options,
                                                _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                                _serviceProvider.GetRequiredService<IKucoinRestClient>(),
                                                _serviceProvider.GetRequiredService<IKucoinSocketClient>());
    }
}
