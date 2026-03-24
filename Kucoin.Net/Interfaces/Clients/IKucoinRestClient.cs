using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Options;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using Kucoin.Net.Interfaces.Clients.SpotApi;

namespace Kucoin.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Kucoin Spot API. 
    /// </summary>
    public interface IKucoinRestClient : IRestClient<KucoinCredentials>
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
    }
}
