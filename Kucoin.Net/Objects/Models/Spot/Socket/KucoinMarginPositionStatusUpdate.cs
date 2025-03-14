using CryptoExchange.Net.Converters.SystemTextJson;
using Kucoin.Net.Enums;

using System;

namespace Kucoin.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Position status update
    /// </summary>
    [SerializationModel]
    public record KucoinMarginPositionStatusUpdate
    {
        /// <summary>
        /// Event type
        /// </summary>
        [JsonPropertyName("type")]

        public MarginEventType TotalDebt { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
