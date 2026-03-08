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
        /// ["<c>maxBaseOrderSize</c>"] Max order quantity in base asset
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
    }


}
