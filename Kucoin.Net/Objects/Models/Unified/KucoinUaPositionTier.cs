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
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>tier</c>"] Tier
        /// </summary>
        [JsonPropertyName("tier")]
        public int Tier { get; set; }
        /// <summary>
        /// ["<c>maxSize</c>"] Max quantity
        /// </summary>
        [JsonPropertyName("maxSize")]
        public decimal MaxQuantity { get; set; }
        /// <summary>
        /// ["<c>minSize</c>"] Min quantity
        /// </summary>
        [JsonPropertyName("minSize")]
        public decimal MinQuantity { get; set; }
        /// <summary>
        /// ["<c>maxLeverage</c>"] Max leverage
        /// </summary>
        [JsonPropertyName("maxLeverage")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// ["<c>initialMarginRate</c>"] Initial margin rate
        /// </summary>
        [JsonPropertyName("initialMarginRate")]
        public decimal InitialMarginRate { get; set; }
        /// <summary>
        /// ["<c>maintainMarginRate</c>"] Maintain margin rate
        /// </summary>
        [JsonPropertyName("maintainMarginRate")]
        public decimal MaintainMarginRate { get; set; }
    }


}
