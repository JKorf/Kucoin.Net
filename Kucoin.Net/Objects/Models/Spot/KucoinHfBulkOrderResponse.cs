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
        /// ["<c>orderId</c>"] The id of the order
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>orderTime</c>"] Order timestamp
        /// </summary>
        [JsonPropertyName("orderTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime OrderTime { get; set; }
        /// <summary>
        /// ["<c>matchTime</c>"] Trade timestamp
        /// </summary>
        [JsonPropertyName("matchTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? TradeTime { get; set; }
        /// <summary>
        /// ["<c>originSize</c>"] The quantity of the order
        /// </summary>
        [JsonPropertyName("originSize")]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// ["<c>originFunds</c>"] The quote quantity of the order
        /// </summary>
        [JsonPropertyName("originFunds")]
        public decimal? QuoteQuantity { get; set; }
        /// <summary>
        /// ["<c>dealSize</c>"] The quantity of the order which was filled
        /// </summary>
        [JsonPropertyName("dealSize")]
        public decimal? QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>dealFunds</c>"] The quote quantity of the order which was filled
        /// </summary>
        [JsonPropertyName("dealFunds")]
        public decimal? QuoteQuantityFilled { get; set; }
        /// <summary>
        /// ["<c>remainSize</c>"] The quantity of the order which is still open
        /// </summary>
        [JsonPropertyName("remainSize")]
        public decimal? QuantityRemaining { get; set; }
        /// <summary>
        /// ["<c>remainFunds</c>"] The quote quantity of the order which is still open
        /// </summary>
        [JsonPropertyName("remainFunds")]
        public decimal? QuoteQuantityRemaining { get; set; }
        /// <summary>
        /// ["<c>canceledSize</c>"] The quantity of the order which was canceled
        /// </summary>
        [JsonPropertyName("canceledSize")]
        public decimal? QuantityCanceled { get; set; }
        /// <summary>
        /// ["<c>canceledFunds</c>"] The quote quantity of the order which was canceled
        /// </summary>
        [JsonPropertyName("canceledFunds")]
        public decimal? QuoteQuantityCanceled { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
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
        /// ["<c>success</c>"] Successful or not
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        /// <summary>
        /// ["<c>failMsg</c>"] Error message
        /// </summary>
        [JsonPropertyName("failMsg")]
        public string? ErrorMessage { get; set; }
    }
}
