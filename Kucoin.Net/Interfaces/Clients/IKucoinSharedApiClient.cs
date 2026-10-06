using CryptoExchange.Net.SharedApis;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Interfaces.Clients.UnifiedApi;

namespace Kucoin.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for the shared REST and WebSocket API implementations of Kucoin
    /// </summary>
    public interface IKucoinSharedApiClient : ISharedApiClientBase
    {
        /// <summary>
        /// Spot REST shared API implementations
        /// </summary>
        IKucoinRestClientSpotSharedApi SpotRest { get; }

        /// <summary>
        /// Futures REST shared API implementations
        /// </summary>
        IKucoinRestClientFuturesSharedApi FuturesRest { get; }

        /// <summary>
        /// Spot WebSocket shared API implementations
        /// </summary>
        IKucoinSocketClientSpotSharedApi SpotSocket { get; }

        /// <summary>
        /// Futures WebSocket shared API implementations
        /// </summary>
        IKucoinSocketClientFuturesSharedApi FuturesSocket { get; }


        /// <summary>
        /// Unified REST Shared API implementations
        /// </summary>
        public IKucoinRestClientUnifiedSharedApi UnifiedRest { get; }
        /// <summary>
        /// Unified Socket Shared API implementations
        /// </summary>
        public IKucoinSocketClientUnifiedSharedApi UnifiedSocket { get; }
    }
}
