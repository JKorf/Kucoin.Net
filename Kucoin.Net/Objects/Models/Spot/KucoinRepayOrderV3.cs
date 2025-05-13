using CryptoExchange.Net.Converters.SystemTextJson;

using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Repayment order info
    /// </summary>
    [SerializationModel]
    public record KucoinRepayOrderV3
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("orderNo")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Isolated margin symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Iniated quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Principal to be paid
        /// </summary>
        [JsonPropertyName("principal")]
        public decimal Principal { get; set; }
        /// <summary>
        /// Interest to be paid
        /// </summary>
        [JsonPropertyName("interest")]
        public decimal Interest { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Time of repayment
        /// </summary>
        [JsonPropertyName("createdTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
