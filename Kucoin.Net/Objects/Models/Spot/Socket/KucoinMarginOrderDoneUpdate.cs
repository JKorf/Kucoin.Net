using Kucoin.Net.Enums;

using System;

namespace Kucoin.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Margin order done event
    /// </summary>
    public record KucoinMarginOrderDoneUpdate
    {
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Order done reason
        /// </summary>
        [JsonPropertyName("reason")]
        [JsonConverter(typeof(EnumConverter))]
        public MarginOrderDoneReason Reason { get; set; }
        /// <summary>
        /// Lend
        /// </summary>
        [JsonPropertyName("side")]
        public string Side { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
