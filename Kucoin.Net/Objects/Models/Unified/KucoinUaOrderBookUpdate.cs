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
        /// Start sequence number
        /// </summary>
        [JsonPropertyName("O")]
        public long StartSequence { get; set; }
        /// <summary>
        /// End sequence number
        /// </summary>
        [JsonPropertyName("C")]
        public long EndSequence { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("M")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// Symbol name
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Bids
        /// </summary>
        [JsonPropertyName("b")]
        public KucoinOrderBookEntry[] Bids { get; set; } = [];
        /// <summary>
        /// Asks
        /// </summary>
        [JsonPropertyName("a")]
        public KucoinOrderBookEntry[] Asks { get; set; } = [];
    }
}
