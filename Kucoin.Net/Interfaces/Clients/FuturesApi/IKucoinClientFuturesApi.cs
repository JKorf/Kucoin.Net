using System;

namespace Kucoin.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Client for accessing the Kucoin Futures API. 
    /// </summary>
    public interface IKucoinClientFuturesApi : IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        IKucoinClientFuturesApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        IKucoinClientFuturesApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        IKucoinClientFuturesApiTrading Trading { get; }
    }
}
