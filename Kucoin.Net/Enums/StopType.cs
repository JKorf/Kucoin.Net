using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Stop direction
    /// </summary>
    [JsonConverter(typeof(EnumConverter<StopType>))]
    public enum StopType
    {
        /// <summary>
        /// Down, triggers when the price reaches or goes below the stopPrice
        /// </summary>
        [Map("down")]
        Down,
        /// <summary>
        /// Up, triggers when the price reaches or goes above the stopPrice
        /// </summary>
        [Map("up")]
        Up
    }
}
