using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Options;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using Kucoin.Net.Interfaces.Clients.SpotApi;

namespace Kucoin.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Kucoin websocket API. 
    /// </summary>
    public interface IKucoinSocketClient : ISocketClient<KucoinCredentials>
    {
        /// <summary>
        /// Spot socket api
        /// </summary>
        /// <see cref="IKucoinSocketClientSpotApi"/>
        IKucoinSocketClientSpotApi SpotApi { get; }
        /// <summary>
        /// Futures socket api
        /// </summary>
        /// <see cref="IKucoinSocketClientFuturesApi"/>
        IKucoinSocketClientFuturesApi FuturesApi { get; }
        /// <summary>
        /// Unified socket api
        /// </summary>
        /// <see cref="IKucoinSocketClientUnifiedApi"/>
        IKucoinSocketClientUnifiedApi UnifiedApi { get; }
    }
}
