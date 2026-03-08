using Kucoin.Net.Enums;

using System;

namespace Kucoin.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Margin order done event
    /// </summary>
    [SerializationModel]
    public record KucoinMarginOrderDoneUpdate
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset name
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>reason</c>"] Order done reason
        /// </summary>
        [JsonPropertyName("reason")]

        public MarginOrderDoneReason Reason { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Lend
        /// </summary>
        [JsonPropertyName("side")]
        public string Side { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>ts</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
