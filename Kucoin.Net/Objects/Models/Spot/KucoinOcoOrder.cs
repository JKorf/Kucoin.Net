using CryptoExchange.Net.Converters.SystemTextJson;
using Kucoin.Net.Enums;

using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// One Cancels Other order
    /// </summary>
    [SerializationModel]
    public record KucoinOcoOrder
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("clientOid")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Order time
        /// </summary>
        [JsonPropertyName("orderTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime OrderTime { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonPropertyName("status")]

        public OcoOrderStatus Status { get; set; }
    }

    /// <summary>
    /// Oco order details
    /// </summary>
    [SerializationModel]
    public record KucoinOcoOrderDetails : KucoinOcoOrder
    {
        /// <summary>
        /// Orders
        /// </summary>
        [JsonPropertyName("orders")]
        public KucoinOcoOrderInfo[] Orders { get; set; } = Array.Empty<KucoinOcoOrderInfo>();
    }

    /// <summary>
    /// Oco stop order info
    /// </summary>
    [SerializationModel]
    public record KucoinOcoOrderInfo
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Stop price
        /// </summary>
        [JsonPropertyName("stopPrice")]
        public decimal StopPrice { get; set; }
        /// <summary>
        /// Order quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
    }
}
