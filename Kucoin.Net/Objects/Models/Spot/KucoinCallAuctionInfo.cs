using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Call auction info
    /// </summary>
    [SerializationModel]
    public record KucoinCallAuctionInfo
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol name 
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>estimatedPrice</c>"] Estimated price
        /// </summary>
        [JsonPropertyName("estimatedPrice")]
        public decimal? EstimatedPrice { get; set; }
        /// <summary>
        /// ["<c>estimatedSize</c>"] Estimated quantity
        /// </summary>
        [JsonPropertyName("estimatedSize")]
        public decimal? EstimatedQuantity { get; set; }
        /// <summary>
        /// ["<c>sellOrderRangeLowPrice</c>"] Sell order range low price
        /// </summary>
        [JsonPropertyName("sellOrderRangeLowPrice")]
        public decimal? SellOrderRangeLowPrice { get; set; }
        /// <summary>
        /// ["<c>sellOrderRangeHighPrice</c>"] Sell order range high price
        /// </summary>
        [JsonPropertyName("sellOrderRangeHighPrice")]
        public decimal? SellOrderRangeHighPrice { get; set; }
        /// <summary>
        /// ["<c>buyOrderRangeLowPrice</c>"] Buy order range low price
        /// </summary>
        [JsonPropertyName("buyOrderRangeLowPrice")]
        public decimal? BuyOrderRangeLowPrice { get; set; }
        /// <summary>
        /// ["<c>buyOrderRangeHighPrice</c>"] Buy order range high price
        /// </summary>
        [JsonPropertyName("buyOrderRangeHighPrice")]
        public decimal? BuyOrderRangeHighPrice { get; set; }
        /// <summary>
        /// ["<c>time</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime? Timestamp { get; set; }
    }


}
