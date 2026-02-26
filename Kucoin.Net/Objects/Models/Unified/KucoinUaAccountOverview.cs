using Kucoin.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Account overview
    /// </summary>
    public record KucoinUaAccountOverview
    {
        /// <summary>
        /// Account type
        /// </summary>
        [JsonPropertyName("accountType")]
        public AccountType AccountType { get; set; }
        /// <summary>
        /// Risk ratio
        /// </summary>
        [JsonPropertyName("riskRatio")]
        public decimal RiskRatio { get; set; }
        /// <summary>
        /// Equity
        /// </summary>
        [JsonPropertyName("equity")]
        public decimal Equity { get; set; }
        /// <summary>
        /// Liability
        /// </summary>
        [JsonPropertyName("liability")]
        public decimal Liability { get; set; }
        /// <summary>
        /// Available margin
        /// </summary>
        [JsonPropertyName("availableMargin")]
        public decimal AvailableMargin { get; set; }
        /// <summary>
        /// Adjusted equity
        /// </summary>
        [JsonPropertyName("adjustedEquity")]
        public decimal AdjustedEquity { get; set; }
        /// <summary>
        /// Initial margin
        /// </summary>
        [JsonPropertyName("im")]
        public decimal InitialMargin { get; set; }
        /// <summary>
        /// Maintenance margin
        /// </summary>
        [JsonPropertyName("mm")]
        public decimal MaintenanceMargin { get; set; }
    }
}
