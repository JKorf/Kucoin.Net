using CryptoExchange.Net.Interfaces;

namespace Kucoin.Net.Interfaces
{
    /// <summary>
    /// Kucoin socket client interface
    /// </summary>
    public interface IKucoinSocketClient: ISocketClient
    {
        /// <summary>
        /// Spot subscriptions
        /// </summary>
        IKucoinSocketClientSpot Spot { get; }
    }
}