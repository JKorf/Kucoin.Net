using System;
using System.Collections.Generic;


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
        [JsonPropertyName("currencyList")]
        public IEnumerable<string> Assets { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Warning debt ratio
        /// </summary>
        [JsonPropertyName("warningDebtRatio")]
        public decimal WarningDebtRatio { get; set; }
        /// <summary>
        /// Forced liquidation ratio
        /// </summary>
        [JsonPropertyName("liqDebtRatio")]
        public decimal LiquidationDebtRatio { get; set; }
        /// <summary>
        /// Max leverage
        /// </summary>
        [JsonPropertyName("maxLeverage")]
        public decimal MaxLeverage { get; set; }
    }
}
