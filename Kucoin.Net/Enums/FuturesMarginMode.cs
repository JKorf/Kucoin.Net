using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FuturesMarginMode>))]
    public enum FuturesMarginMode
    {
        /// <summary>
        /// Cross margin
        /// </summary>
        [Map("CROSS")]
        Cross,
        /// <summary>
        /// Isolated margin
        /// </summary>
        [Map("ISOLATED")]
        Isolated,
    }
}
