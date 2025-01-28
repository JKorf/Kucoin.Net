using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Bulk order creation status
    /// </summary>
    public enum BulkOrderCreationStatus
    {
        /// <summary>
        /// Success
        /// </summary>
        [Map("success")]
        Success,
        /// <summary>
        /// Fail
        /// </summary>
        [Map("fail")]
        Fail
    }
}
