using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Interest rates
    /// </summary>
    public record KucoinBorrowInterestRates
    {
        /// <summary>
        /// VIP level
        /// </summary>
        [JsonPropertyName("vipLevel")]
        public int VipLevel { get; set; }
        /// <summary>
        /// Asset rates
        /// </summary>
        [JsonPropertyName("items")]
        public KucoinBorrowInterestRate[] Rates { get; set; } = [];
    }

    /// <summary>
    /// Interest rate
    /// </summary>
    public record KucoinBorrowInterestRate
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Hourly borrow rate
        /// </summary>
        [JsonPropertyName("hourlyBorrowRate")]
        public decimal HourlyBorrowRate { get; set; }
        /// <summary>
        /// Annualized borrow rate
        /// </summary>
        [JsonPropertyName("annualizedBorrowRate")]
        public decimal AnnualizedBorrowRate { get; set; }
    }
}
