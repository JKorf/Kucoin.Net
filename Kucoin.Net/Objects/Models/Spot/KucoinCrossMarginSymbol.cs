using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    [SerializationModel]
    internal record KucoinCrossMarginSymbols
    {
        [JsonPropertyName("items")]
        public KucoinCrossMarginSymbol[] Items { get; set; } = Array.Empty<KucoinCrossMarginSymbol>();
    }

    /// <summary>
    /// Cross margin symbol
    /// </summary>
    [SerializationModel]
    public record KucoinCrossMarginSymbol
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
        /// ["<c>enableTrading</c>"] Trading enabled
        /// </summary>
        [JsonPropertyName("enableTrading")]
        public bool TradingEnabled { get; set; }
        /// <summary>
        /// ["<c>market</c>"] Market
        /// </summary>
        [JsonPropertyName("market")]
        public string Market { get; set; } = string.Empty;
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
        /// ["<c>baseIncrement</c>"] Base asset step
        /// </summary>
        [JsonPropertyName("baseIncrement")]
        public decimal BaseAssetStep { get; set; }
        /// <summary>
        /// ["<c>baseMinSize</c>"] Minimal order quantity (base asset)
        /// </summary>
        [JsonPropertyName("baseMinSize")]
        public decimal MinOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>quoteIncrement</c>"] Quote asset step
        /// </summary>
        [JsonPropertyName("quoteIncrement")]
        public decimal QuoteAssetStep { get; set; }
        /// <summary>
        /// ["<c>quoteMinSize</c>"] Minimal order value (quote asset)
        /// </summary>
        [JsonPropertyName("quoteMinSize")]
        public decimal MinQuoteOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>baseMaxSize</c>"] Max order quantity
        /// </summary>
        [JsonPropertyName("baseMaxSize")]
        public decimal MaxOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>quoteMaxSize</c>"] Max order value (quote asset)
        /// </summary>
        [JsonPropertyName("quoteMaxSize")]
        public decimal MaxOrderValue { get; set; }
        /// <summary>
        /// ["<c>priceIncrement</c>"] Price step
        /// </summary>
        [JsonPropertyName("priceIncrement")]
        public decimal PriceStep { get; set; }
        /// <summary>
        /// ["<c>feeCurrency</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("feeCurrency")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>priceLimitRate</c>"] Price protection threshold
        /// </summary>
        [JsonPropertyName("priceLimitRate")]
        public decimal PriceLimitRate { get; set; }
        /// <summary>
        /// ["<c>minFunds</c>"] Minimum trading amount
        /// </summary>
        [JsonPropertyName("minFunds")]
        public decimal MinOrderValue { get; set; }
    }

}
