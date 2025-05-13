using CryptoExchange.Net.Converters.SystemTextJson;

using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Borrow order info
    /// </summary>
    [SerializationModel]
    public record KucoinBorrowOrderV3
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
        /// Actual quantity
        /// </summary>
        [JsonPropertyName("actualSize")]
        public decimal ActualQuantity { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Time of borrowing
        /// </summary>
        [JsonPropertyName("createdTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
