using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Order info
    /// </summary>
    [SerializationModel]
    public record KucoinHfOrderDetails : KucoinOrder
    {
        /// <summary>
        /// ["<c>active</c>"] Whether the order is active
        /// </summary>
        [JsonPropertyName("active")]
        public override bool? IsActive { get; set; }

        /// <summary>
        /// ["<c>inOrderBook</c>"] Is the order in the order book
        /// </summary>
        [JsonPropertyName("inOrderBook")]
        public bool InOrderBook { get; set; }
        /// <summary>
        /// ["<c>lastUpdatedAt</c>"] Last update time
        /// </summary>
        [JsonPropertyName("lastUpdatedAt")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// ["<c>cancelledSize</c>"] Quantity canceled
        /// </summary>
        [JsonPropertyName("cancelledSize")]
        public decimal? QuantityCanceled { get; set; }
        /// <summary>
        /// ["<c>cancelledFunds</c>"] Quote quantity canceled
        /// </summary>
        [JsonPropertyName("cancelledFunds")]
        public decimal? QuoteQuantityCanceled { get; set; }
        /// <summary>
        /// ["<c>remainSize</c>"] Remaining quantity
        /// </summary>
        [JsonPropertyName("remainSize")]
        public decimal? QuantityRemaining { get; set; }
        /// <summary>
        /// ["<c>remainFunds</c>"] Remaining quote quantity
        /// </summary>
        [JsonPropertyName("remainFunds")]
        public decimal? QuoteQuantityRemaining { get; set; }
    }
}
