using Kucoin.Net.Enums;
using System;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Futures symbol
    /// </summary>
    public record KucoinFuturesSymbol
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
        /// ["<c>displayBaseCurrency</c>"] Display base asset
        /// </summary>
        [JsonPropertyName("displayBaseCurrency")]
        public string DisplayBaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quoteCurrency</c>"] Quote asset
        /// </summary>
        [JsonPropertyName("quoteCurrency")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>maxBaseOrderSize</c>"] Max order quantity in contracts
        /// </summary>
        [JsonPropertyName("maxBaseOrderSize")]
        public decimal MaxBaseOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>tickSize</c>"] Price tick quantity
        /// </summary>
        [JsonPropertyName("tickSize")]
        public decimal TickQuantity { get; set; }
        /// <summary>
        /// ["<c>tradingStatus</c>"] Trading status
        /// </summary>
        [JsonPropertyName("tradingStatus")]
        public TradingStatus TradingStatus { get; set; }
        /// <summary>
        /// ["<c>settlementCurrency</c>"] Settlement asset
        /// </summary>
        [JsonPropertyName("settlementCurrency")]
        public string SettlementAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contractType</c>"] Contract type
        /// </summary>
        [JsonPropertyName("contractType")]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// ["<c>isInverse</c>"] Is inverse
        /// </summary>
        [JsonPropertyName("isInverse")]
        public bool IsInverse { get; set; }
        /// <summary>
        /// ["<c>launchTime</c>"] Launch time
        /// </summary>
        [JsonPropertyName("launchTime")]
        public DateTime LaunchTime { get; set; }
        /// <summary>
        /// ["<c>expiryTime</c>"] Expiry time
        /// </summary>
        [JsonPropertyName("expiryTime")]
        public DateTime? ExpiryTime { get; set; }
        /// <summary>
        /// ["<c>settlementTime</c>"] Settlement time
        /// </summary>
        [JsonPropertyName("settlementTime")]
        public DateTime? SettlementTime { get; set; }
        /// <summary>
        /// ["<c>maxPrice</c>"] Max price
        /// </summary>
        [JsonPropertyName("maxPrice")]
        public decimal MaxPrice { get; set; }
        /// <summary>
        /// ["<c>lotSize</c>"] Lot size
        /// </summary>
        [JsonPropertyName("lotSize")]
        public decimal LotSize { get; set; }
        /// <summary>
        /// ["<c>unitSize</c>"] Contract size
        /// </summary>
        [JsonPropertyName("unitSize")]
        public decimal ContractSize { get; set; }
        /// <summary>
        /// ["<c>makerFeeRate</c>"] Maker fee rate
        /// </summary>
        [JsonPropertyName("makerFeeRate")]
        public decimal MakerFeeRate { get; set; }
        /// <summary>
        /// ["<c>takerFeeRate</c>"] Taker fee rate
        /// </summary>
        [JsonPropertyName("takerFeeRate")]
        public decimal TakerFeeRate { get; set; }
        /// <summary>
        /// ["<c>settlementFeeRate</c>"] Settlement fee rate
        /// </summary>
        [JsonPropertyName("settlementFeeRate")]
        public decimal? SettlementFeeRate { get; set; }
        /// <summary>
        /// ["<c>maxLeverage</c>"] Max leverage
        /// </summary>
        [JsonPropertyName("maxLeverage")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// ["<c>indexSourceExchanges</c>"] Index source exchanges
        /// </summary>
        [JsonPropertyName("indexSourceExchanges")]
        public string[] IndexSourceExchanges { get; set; } = [];
        /// <summary>
        /// ["<c>mmrLimit</c>"] Maintenance margin ratio limit
        /// </summary>
        [JsonPropertyName("mmrLimit")]
        public decimal MaintenanceMarginRatioLimit { get; set; }
        /// <summary>
        /// ["<c>mmrLevConstant</c>"] Maintenance margin ratio leverage constant
        /// </summary>
        [JsonPropertyName("mmrLevConstant")]
        public decimal MaintenanceMarginRatioLeverageConstant { get; set; }
        /// <summary>
        /// ["<c>maxMarketOrderSize</c>"] Max market order size
        /// </summary>
        [JsonPropertyName("maxMarketOrderSize")]
        public decimal MaxMarketOrderSize { get; set; }
        /// <summary>
        /// ["<c>preMarketToPerpDate</c>"] Time when a pre-market contract converts to a perpetual contract
        /// </summary>
        [JsonPropertyName("preMarketToPerpDate")]
        public DateTime? PreMarketToPerpDate { get; set; }


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
        /// <summary>
        /// ["<c>buyLimit</c>"] Max current buy price
        /// </summary>
        [JsonPropertyName("buyLimit")]
        public decimal BuyLimit { get; set; }
        /// <summary>
        /// ["<c>sellLimit</c>"] Min current sell price
        /// </summary>
        [JsonPropertyName("sellLimit")]
        public decimal SellLimit { get; set; }
        /// <summary>
        /// ["<c>marketStage</c>"] Market stage
        /// </summary>
        [JsonPropertyName("marketStage")]
        public string MarketStage { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>assetClass</c>"] Asset class
        /// </summary>
        [JsonPropertyName("assetClass")]
        public AssetClass AssetClass { get; set; }
        /// <summary>
        /// ["<c>subMarketType</c>"] Sub market type
        /// </summary>
        [JsonPropertyName("subMarketType")]
        public SubMarketType SubMarketType { get; set; }
        /// <summary>
        /// ["<c>indexPriceTickSize</c>"] Index price tick size
        /// </summary>
        [JsonPropertyName("indexPriceTickSize")]
        public decimal IndexPriceTickSize { get; set; }
    }


}
