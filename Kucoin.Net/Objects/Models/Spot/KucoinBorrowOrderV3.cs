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
        /// ["<c>actualSize</c>"] Actual quantity
        /// </summary>
        [JsonPropertyName("actualSize")]
        public decimal ActualQuantity { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>createdTime</c>"] Time of borrowing
        /// </summary>
        [JsonPropertyName("createdTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
