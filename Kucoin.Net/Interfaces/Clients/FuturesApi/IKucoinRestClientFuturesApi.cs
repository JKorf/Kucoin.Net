using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Interfaces.CommonClients;
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
        IKucoinRestClientFuturesApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        IKucoinRestClientFuturesApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        IKucoinRestClientFuturesApiTrading Trading { get; }

        /// <summary>
        /// DEPRECATED; use <see cref="CryptoExchange.Net.SharedApis.ISharedClient" /> instead for common/shared functionality. See <see href="https://jkorf.github.io/CryptoExchange.Net/docs/index.html#shared" /> for more info.
        /// </summary>
        public IFuturesClient CommonFuturesClient { get; }

        /// <summary>
        /// Get the shared rest requests client. This interface is shared with other exhanges to allow for a common implementation for different exchanges.
        /// </summary>
        public IKucoinRestClientFuturesApiShared SharedClient { get; }
    }
}
