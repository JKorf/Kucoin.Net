using Kucoin.Net.Objects.Models.Spot;
using System;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Book update
    /// </summary>
    public record KucoinUaOrderBookUpdate
    {
        /// <summary>
        /// ["<c>O</c>"] Start sequence number
        /// </summary>
        [JsonPropertyName("O")]
        public long StartSequence { get; set; }
        /// <summary>
        /// ["<c>C</c>"] End sequence number
        /// </summary>
        [JsonPropertyName("C")]
        public long EndSequence { get; set; }
        /// <summary>
        /// ["<c>M</c>"] Update time
        /// </summary>
        [JsonPropertyName("M")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// ["<c>s</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>b</c>"] Bids
        /// </summary>
        [JsonPropertyName("b")]
        public KucoinOrderBookEntry[] Bids { get; set; } = [];
        /// <summary>
        /// ["<c>a</c>"] Asks
        /// </summary>
        [JsonPropertyName("a")]
        public KucoinOrderBookEntry[] Asks { get; set; } = [];
    }
}
