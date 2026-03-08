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
        /// ["<c>accountType</c>"] Account type
        /// </summary>
        [JsonPropertyName("accountType")]
        public AccountType AccountType { get; set; }
        /// <summary>
        /// ["<c>riskRatio</c>"] Risk ratio
        /// </summary>
        [JsonPropertyName("riskRatio")]
        public decimal RiskRatio { get; set; }
        /// <summary>
        /// ["<c>equity</c>"] Equity
        /// </summary>
        [JsonPropertyName("equity")]
        public decimal Equity { get; set; }
        /// <summary>
        /// ["<c>liability</c>"] Liability
        /// </summary>
        [JsonPropertyName("liability")]
        public decimal Liability { get; set; }
        /// <summary>
        /// ["<c>availableMargin</c>"] Available margin
        /// </summary>
        [JsonPropertyName("availableMargin")]
        public decimal AvailableMargin { get; set; }
        /// <summary>
        /// ["<c>adjustedEquity</c>"] Adjusted equity
        /// </summary>
        [JsonPropertyName("adjustedEquity")]
        public decimal AdjustedEquity { get; set; }
        /// <summary>
        /// ["<c>im</c>"] Initial margin
        /// </summary>
        [JsonPropertyName("im")]
        public decimal InitialMargin { get; set; }
        /// <summary>
        /// ["<c>mm</c>"] Maintenance margin
        /// </summary>
        [JsonPropertyName("mm")]
        public decimal MaintenanceMargin { get; set; }
    }
}
