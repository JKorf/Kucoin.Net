using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Interest info
    /// </summary>
    public record KucoinMarginInterest
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Daily interest rate
        /// </summary>
        [JsonProperty("dayRatio")]
        public decimal DailyInterestRate { get; set; }
        /// <summary>
        /// Interest quantity
        /// </summary>
        [JsonProperty("interestAmount")]
        public decimal InterestQuantity { get; set; }
        /// <summary>
        /// Timesatmp
        /// </summary>
        [JsonProperty("createdTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
