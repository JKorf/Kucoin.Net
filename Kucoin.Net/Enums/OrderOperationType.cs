using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Order operation type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderOperationType>))]
    public enum OrderOperationType
    {
        /// <summary>
        /// ["<c>DEAL</c>"] Matched
        /// </summary>
        [Map("DEAL")]
        Deal,
        /// <summary>
        /// ["<c>CANCEL</c>"] Canceled
        /// </summary>
        [Map("CANCEL")]
        Cancel
    }
}
