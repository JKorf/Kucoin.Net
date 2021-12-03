using CryptoExchange.Net.Interfaces;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using Kucoin.Net.Interfaces.Clients.SpotApi;

namespace Kucoin.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Kucoin websocket API. 
    /// </summary>
    public interface IKucoinSocketClient : ISocketClient
    {
        /// <summary>
        /// Spot streams
        /// </summary>
        IKucoinSocketClientSpotStreams  SpotStreams { get; }
        /// <summary>
        /// Futures streams
        /// </summary>
        IKucoinSocketClientFuturesStreams  FuturesStreams { get; }
    }
}
