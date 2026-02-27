using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<UnifiedOrderStatus>))]
    public enum UnifiedOrderStatus
    {
        /// <summary>
        /// Not triggered
        /// </summary>
        [Map("0")]
        NotTriggered,
        /// <summary>
        /// Triggered
        /// </summary>
        [Map("1")]
        Triggered,
        /// <summary>
        /// Live
        /// </summary>
        [Map("2")]
        Live,
        /// <summary>
        /// Filled
        /// </summary>
        [Map("3")]
        Filled,
        /// <summary>
        /// Partially filled
        /// </summary>
        [Map("4")]
        PartiallyFilled,
        /// <summary>
        /// Canceled
        /// </summary>
        [Map("5")]
        Canceled,
        /// <summary>
        /// Partially canceled
        /// </summary>
        [Map("6")]
        PartiallyCanceled
    }
}
