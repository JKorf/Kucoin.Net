using CryptoExchange.Net.Interfaces;
using System;

namespace Kucoin.Net.Interfaces.Clients.Rest.Futures
{
    /// <summary>
    /// Client for accessing the Kucoin Futures API. 
    /// </summary>
    public interface IKucoinClientFuturesMarket : IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        IKucoinClientFuturesAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        IKucoinClientFuturesExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        IKucoinClientFuturesTrading Trading { get; }
    }
}
