using System;
using Kucoin.Net.Converters;


namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Contract info
    /// </summary>
    [SerializationModel]
    public record KucoinContract
    {
        /// <summary>
        /// ["<c>baseCurrency</c>"] Base asset
        /// </summary>
        [JsonPropertyName("baseCurrency")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>displaySymbol</c>"] Display name
        /// </summary>
        [JsonPropertyName("displaySymbol")]
        public string DisplaySymbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>displayBaseCurrency</c>"] Display base asset
        /// </summary>
        [JsonPropertyName("displayBaseCurrency")]
        public string DisplayBaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fairMethod</c>"] Fair method
        /// </summary>
        [JsonPropertyName("fairMethod")]
        public string FairMethod { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fundingBaseSymbol</c>"] Funding base symbol
        /// </summary>
        [JsonPropertyName("fundingBaseSymbol")]
        public string FundingBaseSymbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fundingQuoteSymbol</c>"] Funding quote symbol
        /// </summary>
        [JsonPropertyName("fundingQuoteSymbol")]
        public string FundingQuoteSymbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fundingRateSymbol</c>"] Funding rate symbol
        /// </summary>
        [JsonPropertyName("fundingRateSymbol")]
        public string FundingRateSymbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>indexSymbol</c>"] Index symbol
        /// </summary>
        [JsonPropertyName("indexSymbol")]
        public string IndexSymbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>initialMargin</c>"] Initial margin
        /// </summary>
        [JsonPropertyName("initialMargin")]
        public decimal InitialMargin { get; set; }
        /// <summary>
        /// ["<c>isDeleverage</c>"] Enabled ADL or not
        /// </summary>
        [JsonPropertyName("isDeleverage")]
        public bool IsDeleverage { get; set; }
        /// <summary>
        /// ["<c>isInverse</c>"] Reverse contract or not
        /// </summary>
        [JsonPropertyName("isInverse")]
        public bool IsInverse { get; set; }
        /// <summary>
        /// ["<c>isQuanto</c>"] Quanto or not
        /// </summary>
        [JsonPropertyName("isQuanto")]
        public bool IsQuanto { get; set; }
        /// <summary>
        /// ["<c>lotSize</c>"] Minimum lot size
        /// </summary>
        [JsonPropertyName("lotSize")]
        public decimal LotSize { get; set; }
        /// <summary>
        /// ["<c>maintainMargin</c>"] Maintenance margin requirement
        /// </summary>
        [JsonPropertyName("maintainMargin")]
        public decimal MaintainMargin { get; set; }
        /// <summary>
        /// ["<c>makerFeeRate</c>"] Maker fee
        /// </summary>
        [JsonPropertyName("makerFeeRate")]
        public decimal MakerFeeRate { get; set; }
        /// <summary>
        /// ["<c>makerFixFee</c>"] Fixed maker fee
        /// </summary>
        [JsonPropertyName("makerFixFee")]
        public decimal MakerFixFee { get; set; }
        /// <summary>
        /// ["<c>markMethod</c>"] Marking method
        /// </summary>
        [JsonPropertyName("markMethod")]
        public string MarkMethod { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>maxOrderQty</c>"] Maximum order quantity
        /// </summary>
        [JsonPropertyName("maxOrderQty")]
        public decimal MaxOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>maxPrice</c>"] Maximum price
        /// </summary>
        [JsonPropertyName("maxPrice")]
        public decimal MaxPrice { get; set; }
        /// <summary>
        /// ["<c>maxRiskLimit</c>"] Maximum risk limit
        /// </summary>
        [JsonPropertyName("maxRiskLimit")]
        public decimal MaxRiskLimit { get; set; }
        /// <summary>
        /// ["<c>minRiskLimit</c>"] Minimum risk limit
        /// </summary>
        [JsonPropertyName("minRiskLimit")]
        public decimal MinRiskLimit { get; set; }
        /// <summary>
        /// ["<c>multiplier</c>"] Contract multiplier
        /// </summary>
        [JsonPropertyName("multiplier")]
        public decimal Multiplier { get; set; }
        /// <summary>
        /// ["<c>quoteCurrency</c>"] Quote asset
        /// </summary>
        [JsonPropertyName("quoteCurrency")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>riskStep</c>"] Risk limit increment
        /// </summary>
        [JsonPropertyName("riskStep")]
        public decimal RiskStep { get; set; }
        /// <summary>
        /// ["<c>rootSymbol</c>"] Contract group
        /// </summary>
        [JsonPropertyName("rootSymbol")]
        public string RootSymbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>takerFeeRate</c>"] Taker fee
        /// </summary>
        [JsonPropertyName("takerFeeRate")]
        public decimal TakerFeeRate { get; set; }
        /// <summary>
        /// ["<c>takerFixFee</c>"] Taker fixed fee
        /// </summary>
        [JsonPropertyName("takerFixFee")]
        public decimal TakerFixFee { get; set; }
        /// <summary>
        /// ["<c>tickSize</c>"] Minimum price change
        /// </summary>
        [JsonPropertyName("tickSize")]
        public decimal TickSize { get; set; }
        /// <summary>
        /// ["<c>indexPriceTickSize</c>"] Minimum index price change
        /// </summary>
        [JsonPropertyName("indexPriceTickSize")]
        public decimal IndexPriceTickSize { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Type of contract
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>maxLeverage</c>"] Maximum leverage
        /// </summary>
        [JsonPropertyName("maxLeverage")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// ["<c>volumeOf24h</c>"] 24 hour volume
        /// </summary>
        [JsonPropertyName("volumeOf24h")]
        public decimal Volume24H { get; set; }
        /// <summary>
        /// ["<c>turnoverOf24h</c>"] 24 hour turnover
        /// </summary>
        [JsonPropertyName("turnoverOf24h")]
        public decimal Turnover24H { get; set; }
        /// <summary>
        /// ["<c>openInterest</c>"] Open interest
        /// </summary>
        [JsonPropertyName("openInterest")]
        public decimal? OpenInterest { get; set; }
        /// <summary>
        /// ["<c>lowPrice</c>"] 24 hour low price
        /// </summary>
        [JsonPropertyName("lowPrice")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// ["<c>highPrice</c>"] 24 hour high price
        /// </summary>
        [JsonPropertyName("highPrice")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// ["<c>priceChgPct</c>"] 24 hour change percentage
        /// </summary>
        [JsonPropertyName("priceChgPct")]
        public decimal PriceChangePercentage { get; set; }
        /// <summary>
        /// ["<c>priceChg</c>"] 24 hour change
        /// </summary>
        [JsonPropertyName("priceChg")]
        public decimal PriceChange { get; set; }
        /// <summary>
        /// ["<c>firstOpenDate</c>"] First open time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("firstOpenDate")]
        public DateTime? FirstOpenDate { get; set; }
        /// <summary>
        /// ["<c>expireDate</c>"] Expire time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("expireDate")]
        public DateTime? ExpireDate { get; set; }
        /// <summary>
        /// ["<c>settleDate</c>"] Settle time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("settleDate")]
        public DateTime? SettleDate { get; set; }
        /// <summary>
        /// ["<c>settleCurrency</c>"] Settle asset
        /// </summary>
        [JsonPropertyName("settleCurrency")]
        public string? SettleAsset { get; set; }
        /// <summary>
        /// ["<c>settlementSymbol</c>"] Settlement symbol
        /// </summary>
        [JsonPropertyName("settlementSymbol")]
        public string? SettlementSymbol { get; set; }
        /// <summary>
        /// ["<c>settlementFee</c>"] Settle fee
        /// </summary>
        [JsonPropertyName("settlementFee")]
        public decimal? SettlementFee { get; set; }
        /// <summary>
        /// ["<c>fundingFeeRate</c>"] Funding fee rate
        /// </summary>
        [JsonPropertyName("fundingFeeRate")]
        public decimal? FundingFeeRate { get; set; }
        /// <summary>
        /// ["<c>predictedFundingFeeRate</c>"] Predicted funding fee rate
        /// </summary>
        [JsonPropertyName("predictedFundingFeeRate")]
        public decimal? PredictedFundingFeeRate { get; set; }
        /// <summary>
        /// ["<c>nextFundingRateTime</c>"] Next funding rate time. This time may not be accurate up to a couple of seconds.
        /// This is due to the fact that the API returns this value as an offset from the current time, 
        /// but we have no way of knowing the exact time the API returned this value.
        /// </summary>
        [JsonConverter(typeof(NextFundingRateTimeConverter))]
        [JsonPropertyName("nextFundingRateTime")]
        public DateTime? NextFundingRateTime { get; set; }
        /// <summary>
        /// ["<c>sourceExchanges</c>"] Source exchanges
        /// </summary>
        [JsonPropertyName("sourceExchanges")]
        public string[] SourceExchanges { get; set; } = Array.Empty<string>();
        /// <summary>
        /// ["<c>markPrice</c>"] Mark price
        /// </summary>
        [JsonPropertyName("markPrice")]
        public decimal? MarkPrice { get; set; }
        /// <summary>
        /// ["<c>indexPrice</c>"] Index price
        /// </summary>
        [JsonPropertyName("indexPrice")]
        public decimal? IndexPrice { get; set; }
        /// <summary>
        /// ["<c>lastTradePrice</c>"] Last traded price
        /// </summary>
        [JsonPropertyName("lastTradePrice")]
        public decimal? LastTradePrice { get; set; }
        /// <summary>
        /// ["<c>premiumsSymbol1M</c>"] Premiums symbol
        /// </summary>
        [JsonPropertyName("premiumsSymbol1M")]
        public string? PremiumsSymbol1M { get; set; }
        /// <summary>
        /// ["<c>premiumsSymbol8H</c>"] Premiums symbol
        /// </summary>
        [JsonPropertyName("premiumsSymbol8H")]
        public string? PremiumsSymbol8H { get; set; }
        /// <summary>
        /// ["<c>fundingBaseSymbol1M</c>"] Funding base symbol
        /// </summary>
        [JsonPropertyName("fundingBaseSymbol1M")]
        public string? FundingBaseSymbol1M { get; set; }
        /// <summary>
        /// ["<c>fundingQuoteSymbol1M</c>"] Funding quote symbol
        /// </summary>
        [JsonPropertyName("fundingQuoteSymbol1M")]
        public string? FundingQuoteSymbol1M { get; set; }
        /// <summary>
        /// ["<c>supportCross</c>"] Whether symbols supports cross margin position
        /// </summary>
        [JsonPropertyName("supportCross")]
        public bool SupportsCross { get; set; }
        /// <summary>
        /// ["<c>buyLimit</c>"] Maximum limit buying price
        /// </summary>
        [JsonPropertyName("buyLimit")]
        public decimal? MaxLimitBuyPrice { get; set; }
        /// <summary>
        /// ["<c>sellLimit</c>"] Minimum limit selling price
        /// </summary>
        [JsonPropertyName("sellLimit")]
        public decimal? MinLimitSellPrice { get; set; }
        /// <summary>
        /// ["<c>effectiveFundingRateCycleStartTime</c>"] Funding rate time interval (fundingRateGranularity) configuration start effective time
        /// </summary>
        [JsonPropertyName("effectiveFundingRateCycleStartTime")]
        public DateTime? EffectiveFundingRateCycleStartTime { get; set; }
        /// <summary>
        /// ["<c>currentFundingRateGranularity</c>"] Current effective funding rate period granularity (e.g., 8 hours/4 hours)
        /// </summary>
        [JsonPropertyName("currentFundingRateGranularity")]
        public int? CurrentFundingRateGranularity { get; set; }
    }
}
