using Kucoin.Net.Enums;
using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Position status update
    /// </summary>
    public record KucoinMarginPositionStatusUpdate
    {
        /// <summary>
        /// Event type
        /// </summary>
        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter))]
        public MarginEventType TotalDebt { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
