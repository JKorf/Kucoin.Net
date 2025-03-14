using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
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
        /// Base asset
        /// </summary>
        [JsonPropertyName("baseCurrency")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// Fair method
        /// </summary>
        [JsonPropertyName("fairMethod")]
        public string FairMethod { get; set; } = string.Empty;
        /// <summary>
        /// Funding base symbol
        /// </summary>
        [JsonPropertyName("fundingBaseSymbol")]
        public string FundingBaseSymbol { get; set; } = string.Empty;
        /// <summary>
        /// Funding quote symbol
        /// </summary>
        [JsonPropertyName("fundingQuoteSymbol")]
        public string FundingQuoteSymbol { get; set; } = string.Empty;
        /// <summary>
        /// Funding rate symbol
        /// </summary>
        [JsonPropertyName("fundingRateSymbol")]
        public string FundingRateSymbol { get; set; } = string.Empty;
        /// <summary>
        /// Index symbol
        /// </summary>
        [JsonPropertyName("indexSymbol")]
        public string IndexSymbol { get; set; } = string.Empty;
        /// <summary>
        /// Initial margin
        /// </summary>
        [JsonPropertyName("initialMargin")]
        public decimal InitialMargin { get; set; }
        /// <summary>
        /// Enabled ADL or not
        /// </summary>
        [JsonPropertyName("isDeleverage")]
        public bool IsDeleverage { get; set; }
        /// <summary>
        /// Reverse contract or not
        /// </summary>
        [JsonPropertyName("isInverse")]
        public bool IsInverse { get; set; }
        /// <summary>
        /// Quanto or not
        /// </summary>
        [JsonPropertyName("isQuanto")]
        public bool IsQuanto { get; set; }
        /// <summary>
        /// Minimum lot size
        /// </summary>
        [JsonPropertyName("lotSize")]
        public decimal LotSize { get; set; }
        /// <summary>
        /// Maintenance margin requirement
        /// </summary>
        [JsonPropertyName("maintainMargin")]
        public decimal MaintainMargin { get; set; }
        /// <summary>
        /// Maker fee
        /// </summary>
        [JsonPropertyName("makerFeeRate")]
        public decimal MakerFeeRate { get; set; }
        /// <summary>
        /// Fixed maker fee
        /// </summary>
        [JsonPropertyName("makerFixFee")]
        public decimal MakerFixFee { get; set; }
        /// <summary>
        /// Marking method
        /// </summary>
        [JsonPropertyName("markMethod")]
        public string MarkMethod { get; set; } = string.Empty;
        /// <summary>
        /// Maximum order quantity
        /// </summary>
        [JsonPropertyName("maxOrderQty")]
        public decimal MaxOrderQuantity { get; set; }
        /// <summary>
        /// Maximum price
        /// </summary>
        [JsonPropertyName("maxPrice")]
        public decimal MaxPrice { get; set; }
        /// <summary>
        /// Maximum risk limit
        /// </summary>
        [JsonPropertyName("maxRiskLimit")]
        public decimal MaxRiskLimit { get; set; }
        /// <summary>
        /// Minimum risk limit
        /// </summary>
        [JsonPropertyName("minRiskLimit")]
        public decimal MinRiskLimit { get; set; }
        /// <summary>
        /// Contract multiplier
        /// </summary>
        [JsonPropertyName("multiplier")]
        public decimal Multiplier { get; set; }
        /// <summary>
        /// Quote asset
        /// </summary>
        [JsonPropertyName("quoteCurrency")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// Risk limit increment
        /// </summary>
        [JsonPropertyName("riskStep")]
        public decimal RiskStep { get; set; }
        /// <summary>
        /// Contract group
        /// </summary>
        [JsonPropertyName("rootSymbol")]
        public string RootSymbol { get; set; } = string.Empty;
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Taker fee
        /// </summary>
        [JsonPropertyName("takerFeeRate")]
        public decimal TakerFeeRate { get; set; }
        /// <summary>
        /// Taker fixed fee
        /// </summary>
        [JsonPropertyName("takerFixFee")]
        public decimal TakerFixFee { get; set; }
        /// <summary>
        /// Minimum price change
        /// </summary>
        [JsonPropertyName("tickSize")]
        public decimal TickSize { get; set; }
        /// <summary>
        /// Minimum index price change
        /// </summary>
        [JsonPropertyName("indexPriceTickSize")]
        public decimal IndexPriceTickSize { get; set; }
        /// <summary>
        /// Type of contract
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// Maximum leverage
        /// </summary>
        [JsonPropertyName("maxLeverage")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// 24 hour volume
        /// </summary>
        [JsonPropertyName("volumeOf24h")]
        public decimal Volume24H { get; set; }
        /// <summary>
        /// 24 hour turnover
        /// </summary>
        [JsonPropertyName("turnoverOf24h")]
        public decimal Turnover24H { get; set; }
        /// <summary>
        /// Open interest
        /// </summary>
        [JsonPropertyName("openInterest")]
        public decimal? OpenInterest { get; set; }
        /// <summary>
        /// 24 hour low price
        /// </summary>
        [JsonPropertyName("lowPrice")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// 24 hour high price
        /// </summary>
        [JsonPropertyName("highPrice")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// 24 hour change percentage
        /// </summary>
        [JsonPropertyName("priceChgPct")]
        public decimal PriceChangePercentage { get; set; }
        /// <summary>
        /// 24 hour change
        /// </summary>
        [JsonPropertyName("priceChg")]
        public decimal PriceChange { get; set; }
        /// <summary>
        /// First open time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("firstOpenDate")]
        public DateTime? FirstOpenDate { get; set; }
        /// <summary>
        /// Expire time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("expireDate")]
        public DateTime? ExpireDate { get; set; }
        /// <summary>
        /// Settle time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("settleDate")]
        public DateTime? SettleDate { get; set; }
        /// <summary>
        /// Settle asset
        /// </summary>
        [JsonPropertyName("settleCurrency")]
        public string? SettleAsset { get; set; }
        /// <summary>
        /// Settlement symbol
        /// </summary>
        [JsonPropertyName("settlementSymbol")]
        public string? SettlementSymbol { get; set; }
        /// <summary>
        /// Settle fee
        /// </summary>
        [JsonPropertyName("settlementFee")]
        public decimal? SettlementFee { get; set; }
        /// <summary>
        /// Funding fee rate
        /// </summary>
        [JsonPropertyName("fundingFeeRate")]
        public decimal? FundingFeeRate { get; set; }
        /// <summary>
        /// Predicted funding fee rate
        /// </summary>
        [JsonPropertyName("predictedFundingFeeRate")]
        public decimal? PredictedFundingFeeRate { get; set; }
        /// <summary>
        /// Next funding rate time. This time may not be accurate up to a couple of seconds.
        /// This is due to the fact that the API returns this value as an offset from the current time, 
        /// but we have no way of knowing the exact time the API returned this value.
        /// </summary>
        [JsonConverter(typeof(NextFundingRateTimeConverter))]
        [JsonPropertyName("nextFundingRateTime")]
        public DateTime? NextFundingRateTime { get; set; }
        /// <summary>
        /// Source exchanges
        /// </summary>
        [JsonPropertyName("sourceExchanges")]
        public string[] SourceExchanges { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Mark price
        /// </summary>
        [JsonPropertyName("markPrice")]
        public decimal? MarkPrice { get; set; }
        /// <summary>
        /// Index price
        /// </summary>
        [JsonPropertyName("indexPrice")]
        public decimal? IndexPrice { get; set; }
        /// <summary>
        /// Last traded price
        /// </summary>
        [JsonPropertyName("lastTradePrice")]
        public decimal? LastTradePrice { get; set; }
        /// <summary>
        /// Premiums symbol
        /// </summary>
        [JsonPropertyName("premiumsSymbol1M")]
        public string? PremiumsSymbol1M { get; set; }
        /// <summary>
        /// Premiums symbol
        /// </summary>
        [JsonPropertyName("premiumsSymbol8H")]
        public string? PremiumsSymbol8H { get; set; }
        /// <summary>
        /// Funding base symbol
        /// </summary>
        [JsonPropertyName("fundingBaseSymbol1M")]
        public string? FundingBaseSymbol1M { get; set; }
        /// <summary>
        /// Funding quote symbol
        /// </summary>
        [JsonPropertyName("fundingQuoteSymbol1M")]
        public string? FundingQuoteSymbol1M { get; set; }
        /// <summary>
        /// Whether symbols supports cross margin position
        /// </summary>
        [JsonPropertyName("supportCross")]
        public bool SupportsCross { get; set; }
        /// <summary>
        /// Maximum limit buying price
        /// </summary>
        [JsonPropertyName("buyLimit")]
        public decimal? MaxLimitBuyPrice { get; set; }
        /// <summary>
        /// Minimum limit selling price
        /// </summary>
        [JsonPropertyName("sellLimit")]
        public decimal? MinLimitSellPrice { get; set; }
    }
}
