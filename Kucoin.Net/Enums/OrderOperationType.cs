using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Order operation type
    /// </summary>
    public enum OrderOperationType
    {
        /// <summary>
        /// Matched
        /// </summary>
        [Map("DEAL")]
        Deal,
        /// <summary>
        /// Canceled
        /// </summary>
        [Map("CANCEL")]
        Cancel
    }
}
