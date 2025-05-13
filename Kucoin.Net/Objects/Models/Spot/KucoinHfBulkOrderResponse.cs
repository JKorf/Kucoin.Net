using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using Kucoin.Net.Enums;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// The order model in bulk order creation response
    /// </summary>
    [SerializationModel]
    public record KucoinHfOrder
    {
        /// <summary>
        /// The id of the order
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Order timestamp
        /// </summary>
        [JsonPropertyName("orderTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime OrderTime { get; set; }
        /// <summary>
        /// Trade timestamp
        /// </summary>
        [JsonPropertyName("matchTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? TradeTime { get; set; }
        /// <summary>
        /// The quantity of the order
        /// </summary>
        [JsonPropertyName("originSize")]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// The quote quantity of the order
        /// </summary>
        [JsonPropertyName("originFunds")]
        public decimal? QuoteQuantity { get; set; }
        /// <summary>
        /// The quantity of the order which was filled
        /// </summary>
        [JsonPropertyName("dealSize")]
        public decimal? QuantityFilled { get; set; }
        /// <summary>
        /// The quote quantity of the order which was filled
        /// </summary>
        [JsonPropertyName("dealFunds")]
        public decimal? QuoteQuantityFilled { get; set; }
        /// <summary>
        /// The quantity of the order which is still open
        /// </summary>
        [JsonPropertyName("remainSize")]
        public decimal? QuantityRemaining { get; set; }
        /// <summary>
        /// The quote quantity of the order which is still open
        /// </summary>
        [JsonPropertyName("remainFunds")]
        public decimal? QuoteQuantityRemaining { get; set; }
        /// <summary>
        /// The quantity of the order which was canceled
        /// </summary>
        [JsonPropertyName("canceledSize")]
        public decimal? QuantityCanceled { get; set; }
        /// <summary>
        /// The quote quantity of the order which was canceled
        /// </summary>
        [JsonPropertyName("canceledFunds")]
        public decimal? QuoteQuantityCanceled { get; set; }
        /// <summary>
        /// Status
        /// </summary>

        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }
    }

    /// <summary>
    /// The order model in bulk order creation response
    /// </summary>
    [SerializationModel]
    public record KucoinHfBulkOrderResponse : KucoinHfOrder
    {
        /// <summary>
        /// Successful or not
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        [JsonPropertyName("failMsg")]
        public string? ErrorMessage { get; set; }
    }
}
