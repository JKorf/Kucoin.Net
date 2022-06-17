using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Symbol info
    /// </summary>
    public class KucoinSymbol
    {
        /// <summary>
        /// The symbol identifier
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The name of the symbol
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The market the symbol is on
        /// </summary>
        public string Market { get; set; } = string.Empty;
        /// <summary>
        /// The base asset
        /// </summary>
        [JsonProperty("baseCurrency")]
        public string BaseCurrency { get; set; } = string.Empty;
        /// <summary>
        /// The quote asset
        /// </summary>
        [JsonProperty("quoteCurrency")]
        public string QuoteCurrency { get; set; } = string.Empty;
        /// <summary>
        /// The min order quantity in the base asset
        /// </summary>
        [JsonProperty("baseMinSize")]
        public decimal BaseMinSize { get; set; }
        /// <summary>
        /// The min order quantity in the quote asset
        /// </summary>
        [JsonProperty("quoteMinSize")]
        public decimal QuoteMinSize { get; set; }
        /// <summary>
        /// The max order quantity in the base asset
        /// </summary>
        [JsonProperty("baseMaxSize")]
        public decimal BaseMaxSize { get; set; }
        /// <summary>
        /// The max order quantity in the quote asset
        /// </summary>
        [JsonProperty("quoteMaxSize")]
        public decimal QuoteMaxSize { get; set; }
        /// <summary>
        /// The quantity of an order when using the quantity field must be a multiple of this
        /// </summary>
        public decimal BaseIncrement { get; set; }
        /// <summary>
        /// The funds of an order when using the funds field must be a multiple of this
        /// </summary>
        public decimal QuoteIncrement { get; set; }
        /// <summary>
        /// The price of an order must be a multiple of this
        /// </summary>
        public decimal PriceIncrement { get; set; }
        /// <summary>
        /// The price limit rate
        /// </summary>
        public decimal PriceLimitRate { get; set; }
        /// <summary>
        /// The asset the fee will be on
        /// </summary>
        [JsonProperty("feeCurrency")]
        public string FeeCurrency { get; set; } = string.Empty;
        /// <summary>
        /// Whether margin is enabled
        /// </summary>
        public bool IsMarginEnabled { get; set; }
        /// <summary>
        /// Whether trading is enabled
        /// </summary>
        public bool EnableTrading { get; set; }
    }
}
