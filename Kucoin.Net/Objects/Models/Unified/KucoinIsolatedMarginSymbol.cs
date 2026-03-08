namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Isolated margin symbol
    /// </summary>
    public record KucoinIsolatedMarginSymbol
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>name</c>"] Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>baseCurrency</c>"] Base asset
        /// </summary>
        [JsonPropertyName("baseCurrency")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quoteCurrency</c>"] Quote asset
        /// </summary>
        [JsonPropertyName("quoteCurrency")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>maxLeverage</c>"] Max leverage
        /// </summary>
        [JsonPropertyName("maxLeverage")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// ["<c>alertRiskRatio</c>"] Alert risk ratio
        /// </summary>
        [JsonPropertyName("alertRiskRatio")]
        public decimal AlertRiskRatio { get; set; }
        /// <summary>
        /// ["<c>liquidationRiskRatio</c>"] Liquidation risk ratio
        /// </summary>
        [JsonPropertyName("liquidationRiskRatio")]
        public decimal LiquidationRiskRatio { get; set; }
        /// <summary>
        /// ["<c>baseBorrowEnable</c>"] Base borrow enable
        /// </summary>
        [JsonPropertyName("baseBorrowEnable")]
        public bool BaseBorrowEnable { get; set; }
        /// <summary>
        /// ["<c>quoteBorrowEnable</c>"] Quote borrow enable
        /// </summary>
        [JsonPropertyName("quoteBorrowEnable")]
        public bool QuoteBorrowEnable { get; set; }
        /// <summary>
        /// ["<c>baseTransferInEnable</c>"] Base transfer in enable
        /// </summary>
        [JsonPropertyName("baseTransferInEnable")]
        public bool BaseTransferInEnable { get; set; }
        /// <summary>
        /// ["<c>quoteTransferInEnable</c>"] Quote transfer in enable
        /// </summary>
        [JsonPropertyName("quoteTransferInEnable")]
        public bool QuoteTransferInEnable { get; set; }
    }


}
