using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Stop order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<StopOrderStatus>))]
    public enum StopOrderStatus
    {
        /// <summary>
        /// ["<c>NEW</c>"] New
        /// </summary>
        [Map("NEW")]
        New,
        /// <summary>
        /// ["<c>TRIGGERED</c>"] Triggered
        /// </summary>
        [Map("TRIGGERED")]
        Triggered
    }
}
