using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Cross margin symbol
    /// </summary>
    public record KucoinMarginSymbol
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
        /// Market
        /// </summary>
        [JsonPropertyName("market")]
        public string Market { get; set; } = string.Empty;
        /// <summary>
        /// Min base order quantity
        /// </summary>
        [JsonPropertyName("minBaseOrderSize")]
        public decimal MinBaseOrderQuantity { get; set; }
        /// <summary>
        /// Min quote order quantity
        /// </summary>
        [JsonPropertyName("minQuoteOrderSize")]
        public decimal MinQuoteOrderQuantity { get; set; }
        /// <summary>
        /// Max base order quantity
        /// </summary>
        [JsonPropertyName("maxBaseOrderSize")]
        public decimal MaxBaseOrderQuantity { get; set; }
        /// <summary>
        /// Max quote order quantity
        /// </summary>
        [JsonPropertyName("maxQuoteOrderSize")]
        public decimal MaxQuoteOrderQuantity { get; set; }
        /// <summary>
        /// Base order step
        /// </summary>
        [JsonPropertyName("baseOrderStep")]
        public decimal BaseOrderStep { get; set; }
        /// <summary>
        /// Quote order step
        /// </summary>
        [JsonPropertyName("quoteOrderStep")]
        public decimal QuoteOrderStep { get; set; }
        /// <summary>
        /// Tick quantity
        /// </summary>
        [JsonPropertyName("tickSize")]
        public decimal TickQuantity { get; set; }
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonPropertyName("feeCurrency")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Price limit ratio
        /// </summary>
        [JsonPropertyName("priceLimitRatio")]
        public decimal PriceLimitRatio { get; set; }
    }


}
