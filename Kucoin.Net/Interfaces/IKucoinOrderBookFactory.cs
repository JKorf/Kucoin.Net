using CryptoExchange.Net.Interfaces;
using Kucoin.Net.Objects.Options;
using System;

namespace Kucoin.Net.Interfaces
{
    /// <summary>
    /// Factory for creating Kucoin symbol orderbook instance
    /// </summary>
    public interface IKucoinOrderBookFactory
    {
        /// <summary>
        /// Spot order book factory methods
        /// </summary>
        public IOrderBookFactory<KucoinOrderBookOptions> Spot { get; }

        /// <summary>
        /// Futures order book factory methods
        /// </summary>
        public IOrderBookFactory<KucoinOrderBookOptions> Futures { get; }

        /// <summary>
        /// Create a futures ISymbolOrderBook instance for the symbol
        /// </summary>
        /// <param name="symbol">The symbol of the order book</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        /// <returns></returns>
        ISymbolOrderBook CreateFutures(string symbol, Action<KucoinOrderBookOptions>? optionsDelegate = null);

        /// <summary>
        /// Create a spot ISymbolOrderBook instance for the symbol
        /// </summary>
        /// <param name="symbol">The symbol of the order book</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        /// <returns></returns>
        ISymbolOrderBook CreateSpot(string symbol, Action<KucoinOrderBookOptions>? optionsDelegate = null);
    }
}