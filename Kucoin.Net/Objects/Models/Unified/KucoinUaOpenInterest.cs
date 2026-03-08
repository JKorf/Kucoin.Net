using System;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Open interest
    /// </summary>
    public record KucoinUaOpenInterest
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>openInterest</c>"] Open interest in number of contracts
        /// </summary>
        [JsonPropertyName("openInterest")]
        public decimal OpenInterest { get; set; }
        /// <summary>
        /// ["<c>ts</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
    }
}
