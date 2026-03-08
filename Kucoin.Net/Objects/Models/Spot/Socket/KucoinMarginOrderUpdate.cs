using System;

namespace Kucoin.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Margin order update
    /// </summary>
    [SerializationModel]
    public record KucoinMarginOrderUpdate
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
        /// ["<c>dailyIntRate</c>"] Daily interest rate
        /// </summary>
        [JsonPropertyName("dailyIntRate")]
        public decimal DailyInterestRate { get; set; }
        /// <summary>
        /// ["<c>term</c>"] Term in days
        /// </summary>
        [JsonPropertyName("term")]
        public int Term { get; set; }
        /// <summary>
        /// ["<c>size</c>"] Quantity
        /// </summary>
        [JsonPropertyName("size")]
        public int Quantity { get; set; }
        /// <summary>
        /// ["<c>lentSize</c>"] Quantity
        /// </summary>
        [JsonPropertyName("lentSize")]
        public decimal? LentQuantity { get; set; }
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
