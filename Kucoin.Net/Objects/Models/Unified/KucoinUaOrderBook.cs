using Kucoin.Net.Enums;
using Kucoin.Net.Objects.Models.Spot;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Order book
    /// </summary>
    public record KucoinUaOrderBook
    {
        /// <summary>
        /// Product type
        /// </summary>
        [JsonPropertyName("tradeType")]
        public ProductType ProductType { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Sequence
        /// </summary>
        [JsonPropertyName("sequence")]
        public long Sequence { get; set; }

        /// <summary>
        /// Asks
        /// </summary>
        [JsonPropertyName("asks")]
        public KucoinOrderBookEntry[] Asks { get; set; } = [];
        /// <summary>
        /// Bids
        /// </summary>
        [JsonPropertyName("bids")]
        public KucoinOrderBookEntry[] Bids { get; set; } = [];
    }
}
