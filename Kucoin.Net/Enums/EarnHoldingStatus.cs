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
        /// ["<c>LOCKED</c>"] Locked
        /// </summary>
        [Map("LOCKED")]
        Locked,

        /// <summary>
        /// ["<c>REDEEMING</c>"] Redeeming
        /// </summary>
        [Map("REDEEMING")]
        Redeeming,
    }
}
