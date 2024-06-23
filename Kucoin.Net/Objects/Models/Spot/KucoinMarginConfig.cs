using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Margin configuration
    /// </summary>
    public record KucoinMarginConfig
    {
        /// <summary>
        /// Available assets for margin trade
        /// </summary>
        [JsonProperty("currencyList")]
        public IEnumerable<string> Assets { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Warning debt ratio
        /// </summary>
        public decimal WarningDebtRatio { get; set; }
        /// <summary>
        /// Forced liquidation ratio
        /// </summary>
        [JsonProperty("liqDebtRatio")]
        public decimal LiquidationDebtRatio { get; set; }
        /// <summary>
        /// Max leverage
        /// </summary>
        public decimal MaxLeverage { get; set; }
    }
}
