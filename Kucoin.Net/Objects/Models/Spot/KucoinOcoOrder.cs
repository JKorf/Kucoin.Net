using Kucoin.Net.Converters;
using Kucoin.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// One Cancels Other order
    /// </summary>
    public record KucoinOcoOrder
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonProperty("clientOid")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Order time
        /// </summary>
        [JsonProperty("orderTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime OrderTime { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonProperty("status")]
        [JsonConverter(typeof(EnumConverter))]
        public OcoOrderStatus Status { get; set; }
    }

    /// <summary>
    /// Oco order details
    /// </summary>
    public record KucoinOcoOrderDetails : KucoinOcoOrder
    {
        /// <summary>
        /// Orders
        /// </summary>
        [JsonProperty("orders")]
        public IEnumerable<KucoinOcoOrderInfo> Orders { get; set; } = Array.Empty<KucoinOcoOrderInfo>();
    }

    /// <summary>
    /// Oco stop order info
    /// </summary>
    public record KucoinOcoOrderInfo
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Side
        /// </summary>
        [JsonProperty("side")]
        [JsonConverter(typeof(OrderSideConverter))]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Stop price
        /// </summary>
        [JsonProperty("stopPrice")]
        public decimal StopPrice { get; set; }
        /// <summary>
        /// Order quantity
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; } = string.Empty;
    }
}
