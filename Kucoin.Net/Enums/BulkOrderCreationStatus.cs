using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Bulk order creation status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<BulkOrderCreationStatus>))]
    public enum BulkOrderCreationStatus
    {
        /// <summary>
        /// ["<c>success</c>"] Success
        /// </summary>
        [Map("success")]
        Success,
        /// <summary>
        /// ["<c>fail</c>"] Fail
        /// </summary>
        [Map("fail")]
        Fail
    }
}
