using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Both (One way position mode)
        /// </summary>
        [Map("BOTH")]
        Both,
        /// <summary>
        /// Long
        /// </summary>
        [Map("LONG")]
        Long,
        /// <summary>
        /// Short
        /// </summary>
        [Map("SHORT")]
        Short
    }
}
