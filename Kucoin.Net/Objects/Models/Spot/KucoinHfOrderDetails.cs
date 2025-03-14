using CryptoExchange.Net.Converters.SystemTextJson;

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
        /// Whether the order is active
        /// </summary>
        [JsonPropertyName("active")]
        public override bool? IsActive { get; set; }

        /// <summary>
        /// Is the order in the order book
        /// </summary>
        [JsonPropertyName("inOrderBook")]
        public bool InOrderBook { get; set; }
        /// <summary>
        /// Last update time
        /// </summary>
        [JsonPropertyName("lastUpdatedAt")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// Quantity canceled
        /// </summary>
        [JsonPropertyName("cancelledSize")]
        public decimal? QuantityCanceled { get; set; }
        /// <summary>
        /// Quote quantity canceled
        /// </summary>
        [JsonPropertyName("cancelledFunds")]
        public decimal? QuoteQuantityCanceled { get; set; }
        /// <summary>
        /// Remaining quantity
        /// </summary>
        [JsonPropertyName("remainSize")]
        public decimal? QuantityRemaining { get; set; }
        /// <summary>
        /// Remaining quote quantity
        /// </summary>
        [JsonPropertyName("remainFunds")]
        public decimal? QuoteQuantityRemaining { get; set; }
    }
}
