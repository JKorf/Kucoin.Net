using Kucoin.Net.Enums;
using Kucoin.Net.Objects.Models.Spot;
using System;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Liquidation warning update
    /// </summary>
    public record KucoinUaLiquidationWarningUpdate
    {
        /// <summary>
        /// ["<c>eT</c>"] Event type
        /// </summary>
        [JsonPropertyName("eT")]
        public LiquidationWarningType EventType { get; set; }

        /// <summary>
        /// ["<c>r</c>"] Risk ratio
        /// </summary>
        [JsonPropertyName("r")]
        public decimal RiskRatio { get; set; }

        /// <summary>
        /// ["<c>a</c>"] Adjusted equity
        /// </summary>
        [JsonPropertyName("a")]
        public decimal AdjustedEquity { get; set; }

        /// <summary>
        /// ["<c>iM</c>"] Initial margin
        /// </summary>
        [JsonPropertyName("iM")]
        public decimal InitialMargin { get; set; }

        /// <summary>
        /// ["<c>mM</c>"] Maintenance margin
        /// </summary>
        [JsonPropertyName("mM")]
        public decimal MaintenanceMargin { get; set; }

        /// <summary>
        /// ["<c>aM</c>"] Available margin
        /// </summary>
        [JsonPropertyName("aM")]
        public decimal AvailableMargin { get; set; }

        /// <summary>
        /// ["<c>e</c>"] Equity
        /// </summary>
        [JsonPropertyName("e")]
        public decimal Equity { get; set; }

        /// <summary>
        /// ["<c>l</c>"] Liability
        /// </summary>
        [JsonPropertyName("l")]
        public decimal Liability { get; set; }

        /// <summary>
        /// ["<c>U</c>"] Update time
        /// </summary>
        [JsonPropertyName("U")]
        public DateTime UpdateTime { get; set; }
    }
}
