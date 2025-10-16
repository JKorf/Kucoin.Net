using Kucoin.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Spot symbol
    /// </summary>
    public record KucoinSpotSymbol
    {
        /// <summary>
        /// Symbol name
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
        /// Min order quantity in base asset
        /// </summary>
        [JsonPropertyName("minBaseOrderSize")]
        public decimal MinBaseOrderQuantity { get; set; }
        /// <summary>
        /// Min order quantity in quote asset
        /// </summary>
        [JsonPropertyName("minQuoteOrderSize")]
        public decimal MinQuoteOrderQuantity { get; set; }
        /// <summary>
        /// Max order quantity in base asset
        /// </summary>
        [JsonPropertyName("maxBaseOrderSize")]
        public decimal MaxBaseOrderQuantity { get; set; }
        /// <summary>
        /// Max order quantity in quote asset
        /// </summary>
        [JsonPropertyName("maxQuoteOrderSize")]
        public decimal MaxQuoteOrderQuantity { get; set; }
        /// <summary>
        /// Base asset quantity step
        /// </summary>
        [JsonPropertyName("baseOrderStep")]
        public decimal BaseOrderStep { get; set; }
        /// <summary>
        /// Quote asset quantity step
        /// </summary>
        [JsonPropertyName("quoteOrderStep")]
        public decimal QuoteOrderStep { get; set; }
        /// <summary>
        /// Price tick quantity
        /// </summary>
        [JsonPropertyName("tickSize")]
        public decimal TickQuantity { get; set; }
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonPropertyName("feeCurrency")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Trading status
        /// </summary>
        [JsonPropertyName("tradingStatus")]
        public TradingStatus TradingStatus { get; set; }
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonPropertyName("marginMode")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// Price limit ratio
        /// </summary>
        [JsonPropertyName("priceLimitRatio")]
        public decimal PriceLimitRatio { get; set; }
        /// <summary>
        /// Fee category
        /// </summary>
        [JsonPropertyName("feeCategory")]
        public int FeeCategory { get; set; }
        /// <summary>
        /// Maker fee coefficient
        /// </summary>
        [JsonPropertyName("makerFeeCoefficient")]
        public decimal MakerFeeCoefficient { get; set; }
        /// <summary>
        /// Taker fee coefficient
        /// </summary>
        [JsonPropertyName("takerFeeCoefficient")]
        public decimal TakerFeeCoefficient { get; set; }
        /// <summary>
        /// Under special treatment
        /// </summary>
        [JsonPropertyName("st")]
        public bool SpecialTreatment { get; set; }
    }


}
