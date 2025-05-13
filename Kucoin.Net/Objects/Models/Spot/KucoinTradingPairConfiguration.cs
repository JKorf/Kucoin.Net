using CryptoExchange.Net.Converters.SystemTextJson;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Trading Pair Configuration
    /// </summary>
    [SerializationModel]
    public record KucoinTradingPairConfiguration
    {
        /// <summary>
        /// The Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Symbol Name
        /// </summary>
        [JsonPropertyName("symbolName")]
        public string SymbolName { get; set; } = string.Empty;

        /// <summary>
        /// Base Asset
        /// </summary>
        [JsonPropertyName("baseCurrency")]
        public string BaseAsset { get; set; } = string.Empty;

        /// <summary>
        /// Quote Asset
        /// </summary>
        [JsonPropertyName("quoteCurrency")]
        public string QuoteAsset { get; set; } = string.Empty;

        /// <summary>
        /// Max Leverage
        /// </summary>
        [JsonPropertyName("maxLeverage")]
        public decimal MaxLeverage { get; set; }
        
        /// <summary>
        /// FlDebtRatio
        /// </summary>
        [JsonPropertyName("flDebtRatio")]
        public decimal FlDebtRatio { get; set; }

        /// <summary>
        /// Trade Enable
        /// </summary>
        [JsonPropertyName("tradeEnable")]
        public bool TradeEnable { get; set; }

        /// <summary>
        /// Auto Renew Max Debt Ratio
        /// </summary>
        [JsonPropertyName("autoRenewMaxDebtRatio")]
        public decimal AutoRenewMaxDebtRatio { get; set; }

        /// <summary>
        /// Base Borrow Enable
        /// </summary>
        [JsonPropertyName("baseBorrowEnable")]
        public bool BaseBorrowEnable { get; set; }

        /// <summary>
        /// Quote Borrow Enable
        /// </summary>
        [JsonPropertyName("quoteBorrowEnable")]
        public bool QuoteBorrowEnable { get; set; }

        /// <summary>
        /// Base Transfer In Enable
        /// </summary>
        [JsonPropertyName("baseTransferInEnable")]
        public bool BaseTransferInEnable { get; set; }

        /// <summary>
        /// Quote Transfer In Enable
        /// </summary>
        [JsonPropertyName("quoteTransferInEnable")]
        public bool QuoteTransferInEnable { get; set; }
    }
}
