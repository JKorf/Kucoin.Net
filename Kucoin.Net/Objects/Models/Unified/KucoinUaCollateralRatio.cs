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
        /// ["<c>currency</c>"] Asset name
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>cdrConfigs</c>"] CDR configs
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
        /// ["<c>tier</c>"] Tier
        /// </summary>
        [JsonPropertyName("tier")]
        public int Tier { get; set; }
        /// <summary>
        /// ["<c>min</c>"] Min
        /// </summary>
        [JsonPropertyName("min")]
        public decimal Min { get; set; }
        /// <summary>
        /// ["<c>max</c>"] Max
        /// </summary>
        [JsonPropertyName("max")]
        public decimal Max { get; set; }
        /// <summary>
        /// ["<c>cdr</c>"] CDR
        /// </summary>
        [JsonPropertyName("cdr")]
        public decimal Cdr { get; set; }
        /// <summary>
        /// ["<c>cdrEquity</c>"] CDR equity
        /// </summary>
        [JsonPropertyName("cdrEquity")]
        public decimal CdrEquity { get; set; }

    }
}
