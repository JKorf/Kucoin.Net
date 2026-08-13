using System;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Call auction info update
    /// </summary>
    public record KucoinUaCallAuctionInfoUpdate
    {
        /// <summary>
        /// ["<c>s</c>"] Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>es</c>"] Estimated price
        /// </summary>
        [JsonPropertyName("es")]
        public decimal? EstimatedPrice { get; set; }
        /// <summary>
        /// ["<c>eq</c>"] Estimated size
        /// </summary>
        [JsonPropertyName("eq")]
        public decimal? EstimatedSize { get; set; }
        /// <summary>
        /// ["<c>slp</c>"] Sell order range low price
        /// </summary>
        [JsonPropertyName("slp")]
        public decimal? SellOrderRangeLowPrice { get; set; }
        /// <summary>
        /// ["<c>shp</c>"] Sell order range high price
        /// </summary>
        [JsonPropertyName("shp")]
        public decimal? SellOrderRangeHighPrice { get; set; }
        /// <summary>
        /// ["<c>blp</c>"] Buy order range low price
        /// </summary>
        [JsonPropertyName("blp")]
        public decimal? BuyOrderRangeLowPrice { get; set; }
        /// <summary>
        /// ["<c>bhp</c>"] Buy order range high price
        /// </summary>
        [JsonPropertyName("bhp")]
        public decimal? BuyOrderRangeHighPrice { get; set; }
        /// <summary>
        /// ["<c>time</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
    }
}
