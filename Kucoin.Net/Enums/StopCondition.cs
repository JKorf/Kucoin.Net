using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;


namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Stop condition
    /// </summary>
    [JsonConverter(typeof(EnumConverter))]
    [JsonConverter(typeof(EnumConverter<StopCondition>))]
    public enum StopCondition
    {
        /// <summary>
        /// No stop condition
        /// </summary>
        [Map("")]
        None,
        /// <summary>
        /// Loss condition, triggers when the last trade price changes to a value at or below the stopPrice.
        /// </summary>
        [Map("loss", "down")]
        Loss,
        /// <summary>
        /// Entry condition, triggers when the last trade price changes to a value at or above the stopPrice.
        /// </summary>
        [Map("entry", "up")]
        Entry
    }
}
