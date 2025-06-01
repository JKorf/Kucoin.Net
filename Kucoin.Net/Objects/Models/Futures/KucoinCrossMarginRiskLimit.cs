using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Cross margin risk limit
    /// </summary>
    public record KucoinCrossMarginRiskLimit
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Max open quantity
        /// </summary>
        [JsonPropertyName("maxOpenSize")]
        public decimal MaxOpenQuantity { get; set; }
        /// <summary>
        /// Max open value
        /// </summary>
        [JsonPropertyName("maxOpenValue")]
        public decimal MaxOpenValue { get; set; }
        /// <summary>
        /// Total margin
        /// </summary>
        [JsonPropertyName("totalMargin")]
        public decimal TotalMargin { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// Maintenance margin rate
        /// </summary>
        [JsonPropertyName("mmr")]
        public decimal MaintenanceMarginRate { get; set; }
        /// <summary>
        /// Initial margin rate
        /// </summary>
        [JsonPropertyName("imr")]
        public decimal InitialMarginRate { get; set; }
        /// <summary>
        /// Margin asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
    }
}
