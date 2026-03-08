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
        /// ["<c>pi</c>"] Position id
        /// </summary>
        [JsonPropertyName("pi")]
        public string PositionId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>s</c>"] Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>mM</c>"] Margin mode
        /// </summary>
        [JsonPropertyName("mM")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// ["<c>q</c>"] Order quantity
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>eP</c>"] Average entry price
        /// </summary>
        [JsonPropertyName("eP")]
        public decimal EntryPrice { get; set; }
        /// <summary>
        /// ["<c>pV</c>"] Position value
        /// </summary>
        [JsonPropertyName("pV")]
        public decimal PositionValue { get; set; }
        /// <summary>
        /// ["<c>mP</c>"] Mark price
        /// </summary>
        [JsonPropertyName("mP")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// ["<c>lP</c>"] Liquidation price
        /// </summary>
        [JsonPropertyName("lP")]
        public decimal LiquidationPrice { get; set; }
        /// <summary>
        /// ["<c>bP</c>"] Bankrupt price
        /// </summary>
        [JsonPropertyName("bP")]
        public decimal BankruptPrice { get; set; }
        /// <summary>
        /// ["<c>l</c>"] Leverage
        /// </summary>
        [JsonPropertyName("l")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// ["<c>uPL</c>"] Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("uPL")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>rPL</c>"] Realized profit and loss
        /// </summary>
        [JsonPropertyName("rPL")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// ["<c>iM</c>"] Initial margin
        /// </summary>
        [JsonPropertyName("iM")]
        public decimal InitialMargin { get; set; }
        /// <summary>
        /// ["<c>mmr</c>"] Maintenance margin rate
        /// </summary>
        [JsonPropertyName("mmr")]
        public decimal MaintenanceMarginRate { get; set; }
        /// <summary>
        /// ["<c>mtM</c>"] Maintenance margin
        /// </summary>
        [JsonPropertyName("mtM")]
        public decimal MaintenanceMargin { get; set; }
        /// <summary>
        /// ["<c>U</c>"] Update time
        /// </summary>
        [JsonPropertyName("U")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// ["<c>O</c>"] Create time
        /// </summary>
        [JsonPropertyName("O")]
        public DateTime CreateTime { get; set; }
    }
}
