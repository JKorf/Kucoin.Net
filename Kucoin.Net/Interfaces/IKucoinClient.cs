using CryptoExchange.Net.Interfaces;

namespace Kucoin.Net.Interfaces
{
    /// <summary>
    /// Interface for the Kucoin client
    /// </summary>
    public interface IKucoinClient : IRestClient
    {
        /// <summary>
        /// Spot API endpoints
        /// </summary>
        IKucoinClientSpot Spot { get; }

        /// <summary>
        /// Futures API endpoints
        /// </summary>
        IKucoinClientFutures Futures { get; }
    }
}