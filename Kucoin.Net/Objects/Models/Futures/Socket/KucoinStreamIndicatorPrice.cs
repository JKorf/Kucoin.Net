using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Granularity
        /// </summary>
        [JsonPropertyName("granularity")]
        public int Granularity { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Value
        /// </summary>
        [JsonPropertyName("value")]
        public decimal Value { get; set; }
    }
}
