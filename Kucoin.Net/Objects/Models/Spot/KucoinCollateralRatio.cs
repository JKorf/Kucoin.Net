using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Collateral ratio
    /// </summary>
    public record KucoinCollateralRatios
    {
        /// <summary>
        /// Asset list
        /// </summary>
        [JsonPropertyName("currencyList")]
        public string[] Assets { get; set; } = [];

        /// <summary>
        /// Items
        /// </summary>
        [JsonPropertyName("items")]
        public KucoinCollateralRatio[] Items { get; set; } = [];
    }

    /// <summary>
    /// Collateral ratio info
    /// </summary>
    public record KucoinCollateralRatio
    {
        /// <summary>
        /// Lower limit
        /// </summary>
        [JsonPropertyName("lowerLimit")]
        public decimal LowerLimit { get; set; }
        /// <summary>
        /// Upper limit
        /// </summary>
        [JsonPropertyName("upperLimit")]
        public decimal UpperLimit { get; set; }
        /// <summary>
        /// Collateral ratio
        /// </summary>
        [JsonPropertyName("collateralRatio")]
        public decimal CollateralRatio { get; set; }
    }

}
