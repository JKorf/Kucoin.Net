using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Isolated margin symbol
    /// </summary>
    public record KucoinIsolatedMarginSymbol
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Base asset
        /// </summary>
        [JsonPropertyName("baseCurrency")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// Quote asset
        /// </summary>
        [JsonPropertyName("quoteCurrency")]
        public string QuoteAsset { get; set; } = string.Empty;
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
        /// Base borrow enable
        /// </summary>
        [JsonPropertyName("baseBorrowEnable")]
        public bool BaseBorrowEnable { get; set; }
        /// <summary>
        /// Quote borrow enable
        /// </summary>
        [JsonPropertyName("quoteBorrowEnable")]
        public bool QuoteBorrowEnable { get; set; }
        /// <summary>
        /// Base transfer in enable
        /// </summary>
        [JsonPropertyName("baseTransferInEnable")]
        public bool BaseTransferInEnable { get; set; }
        /// <summary>
        /// Quote transfer in enable
        /// </summary>
        [JsonPropertyName("quoteTransferInEnable")]
        public bool QuoteTransferInEnable { get; set; }
    }


}
