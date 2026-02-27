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
        /// Event type
        /// </summary>
        [JsonPropertyName("eT")]
        public LiquidationWarningType EventType { get; set; }

        /// <summary>
        /// Risk ratio
        /// </summary>
        [JsonPropertyName("r")]
        public decimal RiskRatio { get; set; }

        /// <summary>
        /// Adjusted equity
        /// </summary>
        [JsonPropertyName("a")]
        public decimal AdjustedEquity { get; set; }

        /// <summary>
        /// Initial margin
        /// </summary>
        [JsonPropertyName("iM")]
        public decimal InitialMargin { get; set; }

        /// <summary>
        /// Maintenance margin
        /// </summary>
        [JsonPropertyName("mM")]
        public decimal MaintenanceMargin { get; set; }

        /// <summary>
        /// Available margin
        /// </summary>
        [JsonPropertyName("aM")]
        public decimal AvailableMargin { get; set; }

        /// <summary>
        /// Equity
        /// </summary>
        [JsonPropertyName("e")]
        public decimal Equity { get; set; }

        /// <summary>
        /// Liability
        /// </summary>
        [JsonPropertyName("l")]
        public decimal Liability { get; set; }

        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("U")]
        public DateTime UpdateTime { get; set; }
    }
}
