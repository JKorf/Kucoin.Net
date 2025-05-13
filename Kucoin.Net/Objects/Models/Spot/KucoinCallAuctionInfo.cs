using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Symbol name 
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Estimated price
        /// </summary>
        [JsonPropertyName("estimatedPrice")]
        public decimal? EstimatedPrice { get; set; }
        /// <summary>
        /// Estimated quantity
        /// </summary>
        [JsonPropertyName("estimatedSize")]
        public decimal? EstimatedQuantity { get; set; }
        /// <summary>
        /// Sell order range low price
        /// </summary>
        [JsonPropertyName("sellOrderRangeLowPrice")]
        public decimal? SellOrderRangeLowPrice { get; set; }
        /// <summary>
        /// Sell order range high price
        /// </summary>
        [JsonPropertyName("sellOrderRangeHighPrice")]
        public decimal? SellOrderRangeHighPrice { get; set; }
        /// <summary>
        /// Buy order range low price
        /// </summary>
        [JsonPropertyName("buyOrderRangeLowPrice")]
        public decimal? BuyOrderRangeLowPrice { get; set; }
        /// <summary>
        /// Buy order range high price
        /// </summary>
        [JsonPropertyName("buyOrderRangeHighPrice")]
        public decimal? BuyOrderRangeHighPrice { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime? Timestamp { get; set; }
    }


}
