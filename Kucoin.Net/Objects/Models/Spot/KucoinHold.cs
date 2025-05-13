using CryptoExchange.Net.Converters.SystemTextJson;
using System;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Hold info
    /// </summary>
    [SerializationModel]
    public record KucoinHold
    {
        /// <summary>
        /// The asset of the hold
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The quantity of the hold
        /// </summary>
        [JsonPropertyName("holdAmount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The type the hold is for
        /// </summary>
        [JsonPropertyName("bizType")]
        public string BizType { get; set; } = string.Empty;
        /// <summary>
        /// The order id of the hold
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// The time the hold was created
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("createdAt")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// The time the hold was last updated
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("updatedAt")]
        public DateTime UpdateTime { get; set; }
    }
}
