using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using System;

namespace Kucoin.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Spot API endpoints
    /// </summary>
    public interface IKucoinRestClientSpotApi : IRestApiClient<KucoinCredentials>, IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        /// <see cref="IKucoinRestClientSpotApiAccount"/>
        IKucoinRestClientSpotApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to sub-account settings, info or actions
        /// </summary>
        /// <see cref="IKucoinRestClientSpotApiSubAccount"/>
        IKucoinRestClientSpotApiSubAccount SubAccount { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        /// <see cref="IKucoinRestClientSpotApiExchangeData"/>
        IKucoinRestClientSpotApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        /// <see cref="IKucoinRestClientSpotApiTrading"/>
        IKucoinRestClientSpotApiTrading Trading { get; }

        /// <summary>
        /// Endpoints related to orders and trades using the HighFrequency/ProAccount
        /// </summary>
        /// <see cref="IKucoinRestClientSpotApiHfTrading"/>
        IKucoinRestClientSpotApiHfTrading HfTrading { get; }

        /// <summary>
        /// Endpoints for margin borrowing and lending
        /// </summary>
        /// <see cref="IKucoinRestClientSpotApiMargin"/>
        public IKucoinRestClientSpotApiMargin Margin { get; }

        /// <summary>
        /// Endpoints for Kucoin Earn
        /// </summary>
        /// <see cref="IKucoinRestClientSpotApiEarn"/>
        public IKucoinRestClientSpotApiEarn Earn { get; }

        /// <summary>
        /// Get the shared rest requests client. For new implementations prefer <see cref="SharedApi"/>
        /// </summary>
        IKucoinRestClientSpotApiShared SharedClient { get; }

        /// <summary>
        /// Gets the aggregate Shared API interface. Shared APIs provide a common,
        /// exchange-independent contract for accessing functionality across different
        /// exchange client libraries.
        /// </summary>
        public IKucoinRestClientSpotSharedApi SharedApi { get; }

    }
}
