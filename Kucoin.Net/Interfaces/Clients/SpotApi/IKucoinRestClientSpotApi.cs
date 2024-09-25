using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Interfaces.CommonClients;
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
        IKucoinRestClientSpotApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to sub-account settings, info or actions
        /// </summary>
        IKucoinRestClientSpotApiSubAccount SubAccount { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        IKucoinRestClientSpotApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        IKucoinRestClientSpotApiTrading Trading { get; }

        /// <summary>
        /// Endpoints related to orders and trades using the HighFrequency/ProAccount
        /// </summary>
        IKucoinRestClientSpotApiHfTrading HfTrading { get; }

        /// <summary>
        /// Endpoints for margin borrowing and lending
        /// </summary>
        public IKucoinRestClientSpotApiMargin Margin { get; }

        /// <summary>
        /// DEPRECATED; use <see cref="CryptoExchange.Net.SharedApis.ISharedClient" /> instead for common/shared functionality. See <see href="https://jkorf.github.io/CryptoExchange.Net/docs/index.html#shared" /> for more info.
        /// </summary>
        public ISpotClient CommonSpotClient { get; }

        /// <summary>
        /// Get the shared rest requests client. This interface is shared with other exhanges to allow for a common implementation for different exchanges.
        /// </summary>
        IKucoinRestClientSpotApiShared SharedClient { get; }

    }
}
