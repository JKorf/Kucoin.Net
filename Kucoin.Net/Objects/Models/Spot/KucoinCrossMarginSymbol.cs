using CryptoExchange.Net.Converters.SystemTextJson;

using System;
using System.Collections.Generic;

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
        /// Trading enabled
        /// </summary>
        [JsonPropertyName("enableTrading")]
        public bool TradingEnabled { get; set; }
        /// <summary>
        /// Market
        /// </summary>
        [JsonPropertyName("market")]
        public string Market { get; set; } = string.Empty;
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
        /// Base asset step
        /// </summary>
        [JsonPropertyName("baseIncrement")]
        public decimal BaseAssetStep { get; set; }
        /// <summary>
        /// Minimal order quantity (base asset)
        /// </summary>
        [JsonPropertyName("baseMinSize")]
        public decimal MinOrderQuantity { get; set; }
        /// <summary>
        /// Quote asset step
        /// </summary>
        [JsonPropertyName("quoteIncrement")]
        public decimal QuoteAssetStep { get; set; }
        /// <summary>
        /// Minimal order value (quote asset)
        /// </summary>
        [JsonPropertyName("quoteMinSize")]
        public decimal MinQuoteOrderQuantity { get; set; }
        /// <summary>
        /// Max order quantity
        /// </summary>
        [JsonPropertyName("baseMaxSize")]
        public decimal MaxOrderQuantity { get; set; }
        /// <summary>
        /// Max order value (quote asset)
        /// </summary>
        [JsonPropertyName("quoteMaxSize")]
        public decimal MaxOrderValue { get; set; }
        /// <summary>
        /// Price step
        /// </summary>
        [JsonPropertyName("priceIncrement")]
        public decimal PriceStep { get; set; }
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonPropertyName("feeCurrency")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Price protection threshold
        /// </summary>
        [JsonPropertyName("priceLimitRate")]
        public decimal PriceLimitRate { get; set; }
        /// <summary>
        /// Minimum trading amount
        /// </summary>
        [JsonPropertyName("minFunds")]
        public decimal MinOrderValue { get; set; }
    }

}
