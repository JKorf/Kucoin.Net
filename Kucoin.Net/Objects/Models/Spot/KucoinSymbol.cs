using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Symbol info
    /// </summary>
    [SerializationModel]
    public record KucoinSymbol
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol identifier
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>name</c>"] The name of the symbol
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>market</c>"] The market the symbol is on
        /// </summary>
        [JsonPropertyName("market")]
        public string Market { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>baseCurrency</c>"] The base asset
        /// </summary>
        [JsonPropertyName("baseCurrency")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quoteCurrency</c>"] The quote asset
        /// </summary>
        [JsonPropertyName("quoteCurrency")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>baseMinSize</c>"] The min order quantity in the base asset
        /// </summary>
        [JsonPropertyName("baseMinSize")]
        public decimal BaseMinQuantity { get; set; }
        /// <summary>
        /// ["<c>quoteMinSize</c>"] The min order quantity in the quote asset
        /// </summary>
        [JsonPropertyName("quoteMinSize")]
        public decimal QuoteMinQuantity { get; set; }
        /// <summary>
        /// ["<c>baseMaxSize</c>"] The max order quantity in the base asset
        /// </summary>
        [JsonPropertyName("baseMaxSize")]
        public decimal BaseMaxQuantity { get; set; }
        /// <summary>
        /// ["<c>quoteMaxSize</c>"] The max order quantity in the quote asset
        /// </summary>
        [JsonPropertyName("quoteMaxSize")]
        public decimal QuoteMaxQuantity { get; set; }
        /// <summary>
        /// ["<c>baseIncrement</c>"] The quantity of an order when using the quantity field must be a multiple of this
        /// </summary>
        [JsonPropertyName("baseIncrement")]
        public decimal BaseIncrement { get; set; }
        /// <summary>
        /// ["<c>quoteIncrement</c>"] The funds of an order when using the funds field must be a multiple of this
        /// </summary>
        [JsonPropertyName("quoteIncrement")]
        public decimal QuoteIncrement { get; set; }
        /// <summary>
        /// ["<c>priceIncrement</c>"] The price of an order must be a multiple of this
        /// </summary>
        [JsonPropertyName("priceIncrement")]
        public decimal PriceIncrement { get; set; }
        /// <summary>
        /// ["<c>priceLimitRate</c>"] The price limit rate
        /// </summary>
        [JsonPropertyName("priceLimitRate")]
        public decimal PriceLimitRate { get; set; }
        /// <summary>
        /// ["<c>feeCurrency</c>"] The asset the fee will be on
        /// </summary>
        [JsonPropertyName("feeCurrency")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>isMarginEnabled</c>"] Whether margin is enabled
        /// </summary>
        [JsonPropertyName("isMarginEnabled")]
        public bool IsMarginEnabled { get; set; }
        /// <summary>
        /// ["<c>enableTrading</c>"] Whether trading is enabled
        /// </summary>
        [JsonPropertyName("enableTrading")]
        public bool EnableTrading { get; set; }
        /// <summary>
        /// ["<c>minFunds</c>"] Minimum spot and margin trade amounts
        /// </summary>
        [JsonPropertyName("minFunds")]
        public decimal? MinFunds { get; set; }

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
        /// ["<c>st</c>"] Is special treatment symbol
        /// </summary>
        [JsonPropertyName("st")]
        public bool SpecialTreatment { get; set; }
        /// <summary>
        /// ["<c>callauctionIsEnabled</c>"] Is call auction enabled
        /// </summary>
        [JsonPropertyName("callauctionIsEnabled")]
        public bool IsCallAuction { get; set; }
        /// <summary>
        /// ["<c>callauctionPriceFloor</c>"] Call auction price floor
        /// </summary>
        [JsonPropertyName("callauctionPriceFloor")]
        public decimal? CallAuctionPriceFloor { get; set; }
        /// <summary>
        /// ["<c>callauctionPriceCeiling</c>"] Call auction price ceiling
        /// </summary>
        [JsonPropertyName("callauctionPriceCeiling")]
        public decimal? CallAuctionPriceCeiling { get; set; }
        /// <summary>
        /// ["<c>callauctionFirstStageStartTime</c>"] Call auction first stage start time
        /// </summary>
        [JsonPropertyName("callauctionFirstStageStartTime")]
        public DateTime? CallAuctionFirstStageStartTime { get; set; }
        /// <summary>
        /// ["<c>callauctionSecondStageStartTime</c>"] Call auction second stage start time
        /// </summary>
        [JsonPropertyName("callauctionSecondStageStartTime")]
        public DateTime? CallAuctionSecondStageStartTime { get; set; }
        /// <summary>
        /// ["<c>callauctionThirdStageStartTime</c>"] Call auction third stage start time
        /// </summary>
        [JsonPropertyName("callauctionThirdStageStartTime")]
        public DateTime? CallAuctionThirdStageStartTime { get; set; }
        /// <summary>
        /// ["<c>tradingStartTime</c>"] Start trading time (end of call auction)
        /// </summary>
        [JsonPropertyName("tradingStartTime")]
        public DateTime? TradingStartTime { get; set; }
    }
}
