using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Status of an earn holding
    /// </summary>
    [JsonConverter(typeof(EnumConverter<EarnHoldingStatus>))]
    public enum EarnHoldingStatus
    {
        /// <summary>
        /// Locked
        /// </summary>
        [Map("LOCKED")]
        Locked,

        /// <summary>
        /// Redeeming
        /// </summary>
        [Map("REDEEMING")]
        Redeeming,
    }
}