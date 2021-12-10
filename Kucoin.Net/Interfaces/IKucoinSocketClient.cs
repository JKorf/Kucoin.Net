using System;

namespace Kucoin.Net.Interfaces
{
    /// <summary>
    /// Kucoin socket client interface
    /// </summary>
    public interface IKucoinSocketClient: IDisposable
    {
        /// <summary>
        /// Spot subscriptions
        /// </summary>
        IKucoinSocketClientSpot Spot { get; }

        /// <summary>
        /// Futures subscriptions
        /// </summary>
        IKucoinSocketClientFutures Futures { get; }
    }
}