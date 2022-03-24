using CryptoExchange.Net.Interfaces;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Objects;

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

        /// <summary>
        /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
        /// </summary>
        /// <param name="credentials">The credentials to set</param>
        void SetApiCredentials(KucoinApiCredentials credentials);
    }
}
