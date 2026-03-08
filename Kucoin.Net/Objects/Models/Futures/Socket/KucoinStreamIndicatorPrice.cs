using System;


namespace Kucoin.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Index price update
    /// </summary>
    [SerializationModel]
    public record KucoinStreamIndicatorPrice
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>granularity</c>"] Granularity
        /// </summary>
        [JsonPropertyName("granularity")]
        public int Granularity { get; set; }
        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>value</c>"] Value
        /// </summary>
        [JsonPropertyName("value")]
        public decimal Value { get; set; }
    }
}
