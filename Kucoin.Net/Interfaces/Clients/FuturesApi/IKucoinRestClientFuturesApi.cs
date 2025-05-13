using CryptoExchange.Net.Interfaces;
using System;

namespace Kucoin.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Client for accessing the Kucoin Futures API. 
    /// </summary>
    public interface IKucoinRestClientFuturesApi : IRestApiClient, IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        /// <see cref="IKucoinRestClientFuturesApiAccount"/>
        IKucoinRestClientFuturesApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        /// <see cref="IKucoinRestClientFuturesApiExchangeData"/>
        IKucoinRestClientFuturesApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        /// <see cref="IKucoinRestClientFuturesApiTrading"/>
        IKucoinRestClientFuturesApiTrading Trading { get; }

        /// <summary>
        /// Get the shared rest requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        public IKucoinRestClientFuturesApiShared SharedClient { get; }
    }
}
