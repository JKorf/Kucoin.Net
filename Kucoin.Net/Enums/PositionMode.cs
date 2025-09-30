using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Position mode
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PositionMode>))]
    public enum PositionMode
    {
        /// <summary>
        /// One way position mode
        /// </summary>
        [Map("0")]
        OneWay,
        /// <summary>
        /// Hedge mode
        /// </summary>
        [Map("1")]
        HedgeMode
    }
}
