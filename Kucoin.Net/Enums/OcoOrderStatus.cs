using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// OCO order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OcoOrderStatus>))]
    public enum OcoOrderStatus
    {
        /// <summary>
        /// ["<c>NEW</c>"] New
        /// </summary>
        [Map("NEW")]
        New,
        /// <summary>
        /// ["<c>DONE</c>"] Done
        /// </summary>
        [Map("DONE")]
        Done,
        /// <summary>
        /// ["<c>TRIGGERED</c>"] Triggered
        /// </summary>
        [Map("TRIGGERED")]
        Triggered,
        /// <summary>
        /// ["<c>CANCELLED</c>"] Cancelled
        /// </summary>
        [Map("CANCELLED")]
        Canceled
    }
}
