using Kucoin.Net.Enums;
using System;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Position info
    /// </summary>
    public record KucoinUaPosition
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>marginMode</c>"] Margin mode
        /// </summary>
        [JsonPropertyName("marginMode")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// ["<c>size</c>"] Quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>entryPrice</c>"] Entry price
        /// </summary>
        [JsonPropertyName("entryPrice")]
        public decimal EntryPrice { get; set; }
        /// <summary>
        /// ["<c>positionValue</c>"] Position value
        /// </summary>
        [JsonPropertyName("positionValue")]
        public decimal PositionValue { get; set; }
        /// <summary>
        /// ["<c>markPrice</c>"] Mark price
        /// </summary>
        [JsonPropertyName("markPrice")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// ["<c>unrealizedPnL</c>"] Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealizedPnL")]
        public decimal UnrealizedPnL { get; set; }
        /// <summary>
        /// ["<c>realizedPnL</c>"] Realized profit and loss
        /// </summary>
        [JsonPropertyName("realizedPnL")]
        public decimal RealizedPnL { get; set; }
        /// <summary>
        /// ["<c>initialMargin</c>"] Initial margin
        /// </summary>
        [JsonPropertyName("initialMargin")]
        public decimal InitialMargin { get; set; }
        /// <summary>
        /// ["<c>mmr</c>"] Maintenance margin ratio
        /// </summary>
        [JsonPropertyName("mmr")]
        public decimal MaintenanceMarginRatio { get; set; }
        /// <summary>
        /// ["<c>maintenanceMargin</c>"] Maintenance margin
        /// </summary>
        [JsonPropertyName("maintenanceMargin")]
        public decimal MaintenanceMargin { get; set; }
        /// <summary>
        /// ["<c>creationTime</c>"] Create time
        /// </summary>
        [JsonPropertyName("creationTime")]
        public DateTime CreateTime { get; set; }
    }


}
