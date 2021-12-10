using System;

namespace Kucoin.Net.Interfaces
{
    /// <summary>
    /// Interface for the Kucoin client
    /// </summary>
    public interface IKucoinClient : IDisposable
    {
        /// <summary>
        /// Spot API endpoints
        /// </summary>
        IKucoinClientSpot Spot { get; }

        /// <summary>
        /// Margin API endpoints
        /// </summary>
        IKucoinClientMargin Margin { get; }

        /// <summary>
        /// Futures API endpoints
        /// </summary>
        IKucoinClientFutures Futures { get; }
    }
}