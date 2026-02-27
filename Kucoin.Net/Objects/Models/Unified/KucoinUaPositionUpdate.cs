using Kucoin.Net.Enums;
using Kucoin.Net.Objects.Models.Spot;
using System;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// User position update
    /// </summary>
    public record KucoinUaPositionUpdate
    {
        /// <summary>
        /// Position id
        /// </summary>
        [JsonPropertyName("pi")]
        public string PositionId { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonPropertyName("mM")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// Order quantity
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Average entry price
        /// </summary>
        [JsonPropertyName("eP")]
        public decimal EntryPrice { get; set; }
        /// <summary>
        /// Position value
        /// </summary>
        [JsonPropertyName("pV")]
        public decimal PositionValue { get; set; }
        /// <summary>
        /// Mark price
        /// </summary>
        [JsonPropertyName("mP")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// Liquidation price
        /// </summary>
        [JsonPropertyName("lP")]
        public decimal LiquidationPrice { get; set; }
        /// <summary>
        /// Bankrupt price
        /// </summary>
        [JsonPropertyName("bP")]
        public decimal BankruptPrice { get; set; }
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("l")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("uPL")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// Realized profit and loss
        /// </summary>
        [JsonPropertyName("rPL")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// Initial margin
        /// </summary>
        [JsonPropertyName("iM")]
        public decimal InitialMargin { get; set; }
        /// <summary>
        /// Maintenance margin rate
        /// </summary>
        [JsonPropertyName("mmr")]
        public decimal MaintenanceMarginRate { get; set; }
        /// <summary>
        /// Maintenance margin
        /// </summary>
        [JsonPropertyName("mtM")]
        public decimal MaintenanceMargin { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("U")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        [JsonPropertyName("O")]
        public DateTime CreateTime { get; set; }
    }
}
