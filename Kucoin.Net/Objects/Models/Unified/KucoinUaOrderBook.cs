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
        /// ["<c>tradeType</c>"] Product type
        /// </summary>
        [JsonPropertyName("tradeType")]
        public ProductType ProductType { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>sequence</c>"] Sequence
        /// </summary>
        [JsonPropertyName("sequence")]
        public long Sequence { get; set; }

        /// <summary>
        /// ["<c>asks</c>"] Asks
        /// </summary>
        [JsonPropertyName("asks")]
        public KucoinOrderBookEntry[] Asks { get; set; } = [];
        /// <summary>
        /// ["<c>bids</c>"] Bids
        /// </summary>
        [JsonPropertyName("bids")]
        public KucoinOrderBookEntry[] Bids { get; set; } = [];
    }
}
