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
        /// ["<c>type</c>"] Event type
        /// </summary>
        [JsonPropertyName("type")]

        public MarginEventType TotalDebt { get; set; }
        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
