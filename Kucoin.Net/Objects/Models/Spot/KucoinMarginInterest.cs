using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Interest info
    /// </summary>
    [SerializationModel]
    public record KucoinMarginInterest
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>dayRatio</c>"] Daily interest rate
        /// </summary>
        [JsonPropertyName("dayRatio")]
        public decimal DailyInterestRate { get; set; }
        /// <summary>
        /// ["<c>interestAmount</c>"] Interest quantity
        /// </summary>
        [JsonPropertyName("interestAmount")]
        public decimal InterestQuantity { get; set; }
        /// <summary>
        /// ["<c>createdTime</c>"] Timesatmp
        /// </summary>
        [JsonPropertyName("createdTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
