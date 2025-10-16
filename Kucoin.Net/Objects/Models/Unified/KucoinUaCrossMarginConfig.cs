using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Cross margin config
    /// </summary>
    public record KucoinUaCrossMarginConfig
    {
        /// <summary>
        /// Max leverage
        /// </summary>
        [JsonPropertyName("maxLeverage")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// Alert risk ratio
        /// </summary>
        [JsonPropertyName("alertRiskRatio")]
        public decimal AlertRiskRatio { get; set; }
        /// <summary>
        /// Liquidation risk ratio
        /// </summary>
        [JsonPropertyName("liquidationRiskRatio")]
        public decimal LiquidationRiskRatio { get; set; }
        /// <summary>
        /// Asset list
        /// </summary>
        [JsonPropertyName("currencyList")]
        public string[] AssetList { get; set; } = [];
    }
}
