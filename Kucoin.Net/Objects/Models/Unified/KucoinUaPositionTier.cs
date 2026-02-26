using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Position tier
    /// </summary>
    public record KucoinUaPositionTier
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Tier
        /// </summary>
        [JsonPropertyName("tier")]
        public int Tier { get; set; }
        /// <summary>
        /// Max quantity
        /// </summary>
        [JsonPropertyName("maxSize")]
        public decimal MaxQuantity { get; set; }
        /// <summary>
        /// Min quantity
        /// </summary>
        [JsonPropertyName("minSize")]
        public decimal MinQuantity { get; set; }
        /// <summary>
        /// Max leverage
        /// </summary>
        [JsonPropertyName("maxLeverage")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// Initial margin rate
        /// </summary>
        [JsonPropertyName("initialMarginRate")]
        public decimal InitialMarginRate { get; set; }
        /// <summary>
        /// Maintain margin rate
        /// </summary>
        [JsonPropertyName("maintainMarginRate")]
        public decimal MaintainMarginRate { get; set; }
    }


}
