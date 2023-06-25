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