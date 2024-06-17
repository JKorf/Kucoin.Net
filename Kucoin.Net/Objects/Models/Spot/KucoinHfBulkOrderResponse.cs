using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Converters;
using Kucoin.Net.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// The order model in bulk order creation response
    /// </summary>
    public class KucoinHfBulkOrderResponse
    {
        /// <summary>
        /// The id of the order
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// The quantity of the order
        /// </summary>
        [JsonProperty("originSize")]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// The quantity of the order which was filled
        /// </summary>
        [JsonProperty("dealSize")]
        public decimal? QuantityFilled { get; set; }
        /// <summary>
        /// The quantity of the order which is still open
        /// </summary>
        [JsonProperty("remainSize")]
        public decimal? QuantityRemaining { get; set; }
        /// <summary>
        /// The quantity of the order which was canceled
        /// </summary>
        [JsonProperty("canceledSize")]
        public decimal? QuantityCanceled { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        [JsonProperty("status")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// Successful or not
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }
        /// <summary>
        /// Order timestamp
        /// </summary>
        [JsonProperty("orderTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime OrderTime { get; set; }
    }
}
