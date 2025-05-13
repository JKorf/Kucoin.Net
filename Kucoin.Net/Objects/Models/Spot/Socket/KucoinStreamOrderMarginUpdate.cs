using CryptoExchange.Net.Converters.SystemTextJson;
using System;


namespace Kucoin.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Margin update
    /// </summary>
    [SerializationModel]
    public record KucoinStreamOrderMarginUpdate
    {
        /// <summary>
        /// Order margin
        /// </summary>
        [JsonPropertyName("orderMargin")]
        public decimal OrderMargin { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
