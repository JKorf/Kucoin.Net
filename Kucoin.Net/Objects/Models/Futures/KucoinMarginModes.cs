using Kucoin.Net.Enums;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Margin mode info
    /// </summary>
    [SerializationModel]
    public record KucoinMarginModes
    {
        /// <summary>
        /// ["<c>errors</c>"] Errors
        /// </summary>
        [JsonPropertyName("errors")]
        public KucoinMarginModeError[] Errors { get; set; } = [];
        /// <summary>
        /// ["<c>marginMode</c>"] Margin modes for each symbol
        /// </summary>
        [JsonPropertyName("marginMode")]
        public Dictionary<string, FuturesMarginMode> MarginModes { get; set; } = new();
    }

    /// <summary>
    /// Error info
    /// </summary>
    public record KucoinMarginModeError
    {
        /// <summary>
        /// ["<c>code</c>"] Error code
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }
        /// <summary>
        /// ["<c>msg</c>"] Error message
        /// </summary>
        [JsonPropertyName("msg")]
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
    }
}
