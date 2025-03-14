using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;


namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Stop order event
    /// </summary>
    [JsonConverter(typeof(EnumConverter<StopOrderEvent>))]
    public enum StopOrderEvent
    {
        /// <summary>
        /// Stop order opened
        /// </summary>
        [Map("open")]
        Open,
        /// <summary>
        /// Stop order triggered by price
        /// </summary>
        [Map("triggered")]
        Triggered,
        /// <summary>
        /// Stop order canceled
        /// </summary>
        [Map("cancel")]
        Canceled
    }
}
