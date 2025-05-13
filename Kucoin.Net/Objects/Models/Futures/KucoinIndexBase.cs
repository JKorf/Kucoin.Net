using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Granularity in milliseconds
        /// </summary>
        [JsonPropertyName("granularity")]
        public int? Granularity { get; set; }
        /// <summary>
        /// Time point
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("timePoint")]
        public DateTime TimePoint { get; set; }
        /// <summary>
        /// Value
        /// </summary>
        [JsonPropertyName("value")]
        public decimal Value { get; set; }
    }
}
