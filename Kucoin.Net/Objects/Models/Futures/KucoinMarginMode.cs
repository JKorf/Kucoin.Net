using CryptoExchange.Net.Converters.SystemTextJson;
using Kucoin.Net.Enums;

namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Margin mode info
    /// </summary>
    [SerializationModel]
    public record KucoinMarginMode
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonPropertyName("marginMode")]
        public FuturesMarginMode MarginMode { get; set; }
    }


}
