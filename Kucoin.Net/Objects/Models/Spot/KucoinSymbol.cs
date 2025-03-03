

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Symbol info
    /// </summary>
    public record KucoinSymbol
    {
        /// <summary>
        /// The symbol identifier
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The name of the symbol
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The market the symbol is on
        /// </summary>
        [JsonPropertyName("market")]
        public string Market { get; set; } = string.Empty;
        /// <summary>
        /// The base asset
        /// </summary>
        [JsonPropertyName("baseCurrency")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// The quote asset
        /// </summary>
        [JsonPropertyName("quoteCurrency")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// The min order quantity in the base asset
        /// </summary>
        [JsonPropertyName("baseMinSize")]
        public decimal BaseMinQuantity { get; set; }
        /// <summary>
        /// The min order quantity in the quote asset
        /// </summary>
        [JsonPropertyName("quoteMinSize")]
        public decimal QuoteMinQuantity { get; set; }
        /// <summary>
        /// The max order quantity in the base asset
        /// </summary>
        [JsonPropertyName("baseMaxSize")]
        public decimal BaseMaxQuantity { get; set; }
        /// <summary>
        /// The max order quantity in the quote asset
        /// </summary>
        [JsonPropertyName("quoteMaxSize")]
        public decimal QuoteMaxQuantity { get; set; }
        /// <summary>
        /// The quantity of an order when using the quantity field must be a multiple of this
        /// </summary>
        [JsonPropertyName("baseIncrement")]
        public decimal BaseIncrement { get; set; }
        /// <summary>
        /// The funds of an order when using the funds field must be a multiple of this
        /// </summary>
        [JsonPropertyName("quoteIncrement")]
        public decimal QuoteIncrement { get; set; }
        /// <summary>
        /// The price of an order must be a multiple of this
        /// </summary>
        [JsonPropertyName("priceIncrement")]
        public decimal PriceIncrement { get; set; }
        /// <summary>
        /// The price limit rate
        /// </summary>
        [JsonPropertyName("priceLimitRate")]
        public decimal PriceLimitRate { get; set; }
        /// <summary>
        /// The asset the fee will be on
        /// </summary>
        [JsonPropertyName("feeCurrency")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Whether margin is enabled
        /// </summary>
        [JsonPropertyName("isMarginEnabled")]
        public bool IsMarginEnabled { get; set; }
        /// <summary>
        /// Whether trading is enabled
        /// </summary>
        [JsonPropertyName("enableTrading")]
        public bool EnableTrading { get; set; }
        /// <summary>
        /// Minimum spot and margin trade amounts
        /// </summary>
        [JsonPropertyName("minFunds")]
        public decimal? MinFunds { get; set; }
    }
}
