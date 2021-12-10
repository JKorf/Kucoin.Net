using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Margin
{
    /// <summary>
    /// Margin configuration information
    /// </summary>
    public class KucoinMarginConfigurationInfo
    {
        /// <summary>
        /// Available currencies for margin trade
        /// </summary>
        public IEnumerable<string>? CurrencyList { get; set; } = Array.Empty<string>();

        /// <summary>
        /// The warning debt ratio of the forced liquidation
        /// </summary>
        public decimal WarningDebtRatio { get; set; }

        /// <summary>
        /// The debt ratio of the forced liquidation
        /// </summary>
        [JsonProperty("liqDebtRatio")]
        public decimal LiquidationDebtRatio { get; set; }

        /// <summary>
        /// Max leverage available
        /// </summary>
        public int MaxLeverage { get; set; }
    }
}
