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
        /// ["<c>l</c>"] Leverage
        /// </summary>
        [JsonPropertyName("l")]
        public decimal Leverage { get; set; }
    }
}
