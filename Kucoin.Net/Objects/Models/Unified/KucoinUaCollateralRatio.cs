using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Collateral ratio
    /// </summary>
    public record KucoinUaCollateralRatio
    {
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// CDR configs
        /// </summary>
        [JsonPropertyName("cdrConfigs")]
        public KucoinUaCollateralConfig[] Configs { get; set; } = [];
    }

    /// <summary>
    /// Config
    /// </summary>
    public record KucoinUaCollateralConfig
    {
        /// <summary>
        /// Tier
        /// </summary>
        [JsonPropertyName("tier")]
        public int Tier { get; set; }
        /// <summary>
        /// Min
        /// </summary>
        [JsonPropertyName("min")]
        public decimal Min { get; set; }
        /// <summary>
        /// Max
        /// </summary>
        [JsonPropertyName("max")]
        public decimal Max { get; set; }
        /// <summary>
        /// CDR
        /// </summary>
        [JsonPropertyName("cdr")]
        public decimal Cdr { get; set; }
        /// <summary>
        /// CDR equity
        /// </summary>
        [JsonPropertyName("cdrEquity")]
        public decimal CdrEquity { get; set; }

    }
}
