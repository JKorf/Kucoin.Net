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
        /// Filled
        /// </summary>
        [Map("FILLED")]
        Filled,
        /// <summary>
        /// Canceled
        /// </summary>
        [Map("CANCELED")]
        Canceled
    }
}
