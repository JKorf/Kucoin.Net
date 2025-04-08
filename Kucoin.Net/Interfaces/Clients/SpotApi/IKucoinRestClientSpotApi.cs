using CryptoExchange.Net.Interfaces;
using System;

namespace Kucoin.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Spot API endpoints
    /// </summary>
    public interface IKucoinRestClientSpotApi : IRestApiClient, IDisposable
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
        /// Get the shared rest requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        IKucoinRestClientSpotApiShared SharedClient { get; }

    }
}
