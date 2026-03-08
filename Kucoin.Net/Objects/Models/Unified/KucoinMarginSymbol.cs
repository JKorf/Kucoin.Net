namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Cross margin symbol
    /// </summary>
    public record KucoinMarginSymbol
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
        /// ["<c>market</c>"] Market
        /// </summary>
        [JsonPropertyName("market")]
        public string Market { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>minBaseOrderSize</c>"] Min base order quantity
        /// </summary>
        [JsonPropertyName("minBaseOrderSize")]
        public decimal MinBaseOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>minQuoteOrderSize</c>"] Min quote order quantity
        /// </summary>
        [JsonPropertyName("minQuoteOrderSize")]
        public decimal MinQuoteOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>maxBaseOrderSize</c>"] Max base order quantity
        /// </summary>
        [JsonPropertyName("maxBaseOrderSize")]
        public decimal MaxBaseOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>maxQuoteOrderSize</c>"] Max quote order quantity
        /// </summary>
        [JsonPropertyName("maxQuoteOrderSize")]
        public decimal MaxQuoteOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>baseOrderStep</c>"] Base order step
        /// </summary>
        [JsonPropertyName("baseOrderStep")]
        public decimal BaseOrderStep { get; set; }
        /// <summary>
        /// ["<c>quoteOrderStep</c>"] Quote order step
        /// </summary>
        [JsonPropertyName("quoteOrderStep")]
        public decimal QuoteOrderStep { get; set; }
        /// <summary>
        /// ["<c>tickSize</c>"] Tick quantity
        /// </summary>
        [JsonPropertyName("tickSize")]
        public decimal TickQuantity { get; set; }
        /// <summary>
        /// ["<c>feeCurrency</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("feeCurrency")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>priceLimitRatio</c>"] Price limit ratio
        /// </summary>
        [JsonPropertyName("priceLimitRatio")]
        public decimal PriceLimitRatio { get; set; }
    }


}
