using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Lending interest info
    /// </summary>
    public record KucoinLendingInterest
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Market interest rate
        /// </summary>
        [JsonProperty("marketInterestRate")]
        public decimal InterestRate { get; set; }
    }
}
