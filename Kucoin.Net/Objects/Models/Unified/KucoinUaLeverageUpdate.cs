using Kucoin.Net.Enums;
using Kucoin.Net.Objects.Models.Spot;
using System;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Leverage update
    /// </summary>
    public record KucoinUaLeverageUpdate
    {
        /// <summary>
        /// ["<c>s</c>"] Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>c</c>"] Asset for cross margin
        /// </summary>
        [JsonPropertyName("c")]
        public string? Asset { get; set; }
        /// <summary>
        /// ["<c>l</c>"] Leverage
        /// </summary>
        [JsonPropertyName("l")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// ["<c>mM</c>"] Margin mode
        /// </summary>
        [JsonPropertyName("mM")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// ["<c>tT</c>"] Trade type
        /// </summary>
        [JsonPropertyName("tT")]
        public ProductType TradeType { get; set; }
    }
}
