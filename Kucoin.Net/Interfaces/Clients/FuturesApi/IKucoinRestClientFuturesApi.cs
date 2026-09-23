using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;
using System;

namespace Kucoin.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Client for accessing the Kucoin Futures API. 
    /// </summary>
    public interface IKucoinRestClientFuturesApi : IRestApiClient<KucoinCredentials>, IDisposable
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
        /// [V1] Get the shared rest requests client. For new implementations prefer <see cref="SharedApi"/>
        /// </summary>
        public IKucoinRestClientFuturesApiShared SharedClient { get; }

        /// <summary>
        /// [V2] Gets the aggregate Shared API interface. Shared APIs provide a common,
        /// exchange-independent contract for accessing functionality across different
        /// exchange client libraries.
        /// </summary>
        public IKucoinRestClientFuturesSharedApi SharedApi { get; }
    }
}
