using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Mode of Margin
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarginMode>))]
    public enum MarginMode
    {
        /// <summary>
        /// Cross Mode
        /// </summary>
        [Map("CROSS", "cross")]
        CrossMode,
        /// <summary>
        /// Isolated Mode, This mode is not supported by platform yet.
        /// </summary>
        [Map("ISOLATED", "isolated")]
        IsolatedMode,
    }
}
