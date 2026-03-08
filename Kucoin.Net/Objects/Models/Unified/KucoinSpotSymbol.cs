using Kucoin.Net.Enums;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Spot symbol
    /// </summary>
    public record KucoinSpotSymbol
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol name
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
        /// ["<c>minBaseOrderSize</c>"] Min order quantity in base asset
        /// </summary>
        [JsonPropertyName("minBaseOrderSize")]
        public decimal MinBaseOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>minQuoteOrderSize</c>"] Min order quantity in quote asset
        /// </summary>
        [JsonPropertyName("minQuoteOrderSize")]
        public decimal MinQuoteOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>maxBaseOrderSize</c>"] Max order quantity in base asset
        /// </summary>
        [JsonPropertyName("maxBaseOrderSize")]
        public decimal MaxBaseOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>maxQuoteOrderSize</c>"] Max order quantity in quote asset
        /// </summary>
        [JsonPropertyName("maxQuoteOrderSize")]
        public decimal MaxQuoteOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>baseOrderStep</c>"] Base asset quantity step
        /// </summary>
        [JsonPropertyName("baseOrderStep")]
        public decimal BaseOrderStep { get; set; }
        /// <summary>
        /// ["<c>quoteOrderStep</c>"] Quote asset quantity step
        /// </summary>
        [JsonPropertyName("quoteOrderStep")]
        public decimal QuoteOrderStep { get; set; }
        /// <summary>
        /// ["<c>tickSize</c>"] Price tick quantity
        /// </summary>
        [JsonPropertyName("tickSize")]
        public decimal TickQuantity { get; set; }
        /// <summary>
        /// ["<c>feeCurrency</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("feeCurrency")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>tradingStatus</c>"] Trading status
        /// </summary>
        [JsonPropertyName("tradingStatus")]
        public TradingStatus TradingStatus { get; set; }
        /// <summary>
        /// ["<c>marginMode</c>"] Margin mode
        /// </summary>
        [JsonPropertyName("marginMode")]
        public MarginEnabledMode IsMarginEnabled { get; set; }
        /// <summary>
        /// ["<c>priceLimitRatio</c>"] Price limit ratio
        /// </summary>
        [JsonPropertyName("priceLimitRatio")]
        public decimal PriceLimitRatio { get; set; }
        /// <summary>
        /// ["<c>feeCategory</c>"] Fee category
        /// </summary>
        [JsonPropertyName("feeCategory")]
        public int FeeCategory { get; set; }
        /// <summary>
        /// ["<c>makerFeeCoefficient</c>"] Maker fee coefficient
        /// </summary>
        [JsonPropertyName("makerFeeCoefficient")]
        public decimal MakerFeeCoefficient { get; set; }
        /// <summary>
        /// ["<c>takerFeeCoefficient</c>"] Taker fee coefficient
        /// </summary>
        [JsonPropertyName("takerFeeCoefficient")]
        public decimal TakerFeeCoefficient { get; set; }
        /// <summary>
        /// ["<c>st</c>"] Under special treatment
        /// </summary>
        [JsonPropertyName("st")]
        public bool SpecialTreatment { get; set; }
    }


}
