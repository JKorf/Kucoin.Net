using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Margin requirements
    /// </summary>
    public record KucoinCrossMarginRequirement
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Initial margin requirements
        /// </summary>
        [JsonPropertyName("imr")]
        public decimal InitialMarginRequirement { get; set; }
        /// <summary>
        /// Maintenance margin requirement
        /// </summary>
        [JsonPropertyName("mmr")]
        public decimal MaintenanceMarginRequirement { get; set; }
        /// <summary>
        /// Position size in contracts
        /// </summary>
        [JsonPropertyName("size")]
        public int PositionSize { get; set; }
        /// <summary>
        /// Position value
        /// </summary>
        [JsonPropertyName("positionValue")]
        public decimal PositionValue { get; set; }
        /// <summary>
        /// Trade price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }
}
