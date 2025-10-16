using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects.Options;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Objects;

namespace Kucoin.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Kucoin Spot API. 
    /// </summary>
    public interface IKucoinRestClient : IRestClient
    {
        /// <summary>
        /// Spot API endpoints
        /// </summary>
        /// <see cref="IKucoinRestClientSpotApi"/>
        IKucoinRestClientSpotApi SpotApi { get; }
        /// <summary>
        /// Futures API endpoints
        /// </summary>
        /// <see cref="IKucoinRestClientFuturesApi"/>
        IKucoinRestClientFuturesApi FuturesApi { get; }
        /// <summary>
        /// Unified API endpoints
        /// </summary>
        /// <see cref="IKucoinRestClientUnifiedApi"/>
        IKucoinRestClientUnifiedApi UnifiedApi { get; }

        /// <summary>
        /// Update specific options
        /// </summary>
        /// <param name="options">Options to update. Only specific options are changeable after the client has been created</param>
        void SetOptions(UpdateOptions options);

        /// <summary>
        /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
        /// </summary>
        /// <param name="credentials">The credentials to set</param>
        void SetApiCredentials(ApiCredentials credentials);
    }
}
