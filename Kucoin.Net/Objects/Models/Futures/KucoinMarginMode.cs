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
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>marginMode</c>"] Margin mode
        /// </summary>
        [JsonPropertyName("marginMode")]
        public FuturesMarginMode MarginMode { get; set; }
    }


}
