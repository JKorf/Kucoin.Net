using System;


namespace Kucoin.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Tick info
    /// </summary>
    [SerializationModel]
    public record KucoinStreamFuturesTick
    {
        /// <summary>
        /// ["<c>sequence</c>"] Sequence number
        /// </summary>
        [JsonPropertyName("sequence")]
        public long Sequence { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>bestBidSize</c>"] Best bid quantity
        /// </summary>
        [JsonPropertyName("bestBidSize")]
        public decimal BestBidQuantity { get; set; }
        /// <summary>
        /// ["<c>bestBidPrice</c>"] Best bid price
        /// </summary>
        [JsonPropertyName("bestBidPrice")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>bestAskSize</c>"] Best ask quantity
        /// </summary>
        [JsonPropertyName("bestAskSize")]
        public decimal BestAskQuantity { get; set; }
        /// <summary>
        /// ["<c>bestAskPrice</c>"] Best ask price
        /// </summary>
        [JsonPropertyName("bestAskPrice")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>ts</c>"] Filled time
        /// </summary>
        [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
