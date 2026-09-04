using Kucoin.Net.Interfaces.Clients.FuturesApi;
using Kucoin.Net.Interfaces.Clients.SpotApi;

namespace Kucoin.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for the shared REST and WebSocket API implementations of Kucoin
    /// </summary>
    public interface IKucoinSharedApiClient
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
    }
}
