using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Lend order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<LendStatus>))]
    public enum LendStatus
    {
        /// <summary>
        /// ["<c>FILLED</c>"] Filled
        /// </summary>
        [Map("FILLED")]
        Filled,
        /// <summary>
        /// ["<c>CANCELED</c>"] Canceled
        /// </summary>
        [Map("CANCELED")]
        Canceled
    }
}
