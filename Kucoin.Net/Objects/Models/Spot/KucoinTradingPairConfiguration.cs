namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Trading Pair Configuration
    /// </summary>
    [SerializationModel]
    public record KucoinTradingPairConfiguration
    {
        /// <summary>
        /// ["<c>symbol</c>"] The Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>symbolName</c>"] Symbol Name
        /// </summary>
        [JsonPropertyName("symbolName")]
        public string SymbolName { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>baseCurrency</c>"] Base Asset
        /// </summary>
        [JsonPropertyName("baseCurrency")]
        public string BaseAsset { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>quoteCurrency</c>"] Quote Asset
        /// </summary>
        [JsonPropertyName("quoteCurrency")]
        public string QuoteAsset { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>maxLeverage</c>"] Max Leverage
        /// </summary>
        [JsonPropertyName("maxLeverage")]
        public decimal MaxLeverage { get; set; }
        
        /// <summary>
        /// ["<c>flDebtRatio</c>"] FlDebtRatio
        /// </summary>
        [JsonPropertyName("flDebtRatio")]
        public decimal FlDebtRatio { get; set; }

        /// <summary>
        /// ["<c>tradeEnable</c>"] Trade Enable
        /// </summary>
        [JsonPropertyName("tradeEnable")]
        public bool TradeEnable { get; set; }

        /// <summary>
        /// ["<c>autoRenewMaxDebtRatio</c>"] Auto Renew Max Debt Ratio
        /// </summary>
        [JsonPropertyName("autoRenewMaxDebtRatio")]
        public decimal AutoRenewMaxDebtRatio { get; set; }

        /// <summary>
        /// ["<c>baseBorrowEnable</c>"] Base Borrow Enable
        /// </summary>
        [JsonPropertyName("baseBorrowEnable")]
        public bool BaseBorrowEnable { get; set; }

        /// <summary>
        /// ["<c>quoteBorrowEnable</c>"] Quote Borrow Enable
        /// </summary>
        [JsonPropertyName("quoteBorrowEnable")]
        public bool QuoteBorrowEnable { get; set; }

        /// <summary>
        /// ["<c>baseTransferInEnable</c>"] Base Transfer In Enable
        /// </summary>
        [JsonPropertyName("baseTransferInEnable")]
        public bool BaseTransferInEnable { get; set; }

        /// <summary>
        /// ["<c>quoteTransferInEnable</c>"] Quote Transfer In Enable
        /// </summary>
        [JsonPropertyName("quoteTransferInEnable")]
        public bool QuoteTransferInEnable { get; set; }
    }
}
