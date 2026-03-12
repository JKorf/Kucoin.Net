using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Position side
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PositionSide>))]
    public enum PositionSide
    {
        /// <summary>
        /// ["<c>BOTH</c>"] Both (One way position mode)
        /// </summary>
        [Map("BOTH")]
        Both,
        /// <summary>
        /// ["<c>LONG</c>"] Long
        /// </summary>
        [Map("LONG")]
        Long,
        /// <summary>
        /// ["<c>SHORT</c>"] Short
        /// </summary>
        [Map("SHORT")]
        Short
    }
}
