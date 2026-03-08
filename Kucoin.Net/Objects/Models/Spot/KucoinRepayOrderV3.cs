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
        /// ["<c>orderNo</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderNo")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbol</c>"] Isolated margin symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>size</c>"] Iniated quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>principal</c>"] Principal to be paid
        /// </summary>
        [JsonPropertyName("principal")]
        public decimal Principal { get; set; }
        /// <summary>
        /// ["<c>interest</c>"] Interest to be paid
        /// </summary>
        [JsonPropertyName("interest")]
        public decimal Interest { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>createdTime</c>"] Time of repayment
        /// </summary>
        [JsonPropertyName("createdTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
