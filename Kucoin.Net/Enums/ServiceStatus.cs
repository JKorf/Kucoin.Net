using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Service status
    /// </summary>
    public enum ServiceStatus
    {
        /// <summary>
        /// Open
        /// </summary>
        [Map("open")]
        Open,
        /// <summary>
        /// Closed
        /// </summary>
        [Map("close")]
        Close,
        /// <summary>
        /// Only cancelation available
        /// </summary>
        [Map("cancelOnly")]
        CancelOnly
    }
}
