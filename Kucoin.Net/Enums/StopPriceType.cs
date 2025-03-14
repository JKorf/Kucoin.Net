using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Stop price trigger value
    /// </summary>
    [JsonConverter(typeof(EnumConverter<StopPriceType>))]
    public enum StopPriceType
    {
        /// <summary>
        /// Trigger on the last trade price
        /// </summary>
        [Map("TP")]
        TradePrice,
        /// <summary>
        /// Trigger on mark price
        /// </summary>
        [Map("MP")]
        MarkPrice,
        /// <summary>
        /// Trigger on index price
        /// </summary>
        [Map("IP")]
        IndexPrice
    }
}
