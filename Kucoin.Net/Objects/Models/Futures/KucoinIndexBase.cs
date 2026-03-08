using System;


namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Base record for index data
    /// </summary>
    [SerializationModel]
    public record KucoinIndexBase
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>granularity</c>"] Granularity in milliseconds
        /// </summary>
        [JsonPropertyName("granularity")]
        public int? Granularity { get; set; }
        /// <summary>
        /// ["<c>timePoint</c>"] Time point
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("timePoint")]
        public DateTime TimePoint { get; set; }
        /// <summary>
        /// ["<c>value</c>"] Value
        /// </summary>
        [JsonPropertyName("value")]
        public decimal Value { get; set; }
    }
}
