﻿
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
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Market interest rate
        /// </summary>
        [JsonPropertyName("marketInterestRate")]
        public decimal InterestRate { get; set; }
    }
}
