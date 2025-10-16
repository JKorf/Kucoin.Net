using Kucoin.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Futures symbol
    /// </summary>
    public record KucoinFuturesSymbol
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
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
        /// Max order quantity in base asset
        /// </summary>
        [JsonPropertyName("maxBaseOrderSize")]
        public decimal MaxBaseOrderQuantity { get; set; }
        /// <summary>
        /// Price tick quantity
        /// </summary>
        [JsonPropertyName("tickSize")]
        public decimal TickQuantity { get; set; }
        /// <summary>
        /// Trading status
        /// </summary>
        [JsonPropertyName("tradingStatus")]
        public TradingStatus TradingStatus { get; set; }
        /// <summary>
        /// Settlement asset
        /// </summary>
        [JsonPropertyName("settlementCurrency")]
        public string SettlementAsset { get; set; } = string.Empty;
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonPropertyName("contractType")]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// Is inverse
        /// </summary>
        [JsonPropertyName("isInverse")]
        public bool IsInverse { get; set; }
        /// <summary>
        /// Launch time
        /// </summary>
        [JsonPropertyName("launchTime")]
        public DateTime LaunchTime { get; set; }
        /// <summary>
        /// Expiry time
        /// </summary>
        [JsonPropertyName("expiryTime")]
        public DateTime? ExpiryTime { get; set; }
        /// <summary>
        /// Settlement time
        /// </summary>
        [JsonPropertyName("settlementTime")]
        public DateTime? SettlementTime { get; set; }
        /// <summary>
        /// Max price
        /// </summary>
        [JsonPropertyName("maxPrice")]
        public decimal MaxPrice { get; set; }
        /// <summary>
        /// Lot size
        /// </summary>
        [JsonPropertyName("lotSize")]
        public decimal LotSize { get; set; }
        /// <summary>
        /// Contract size
        /// </summary>
        [JsonPropertyName("unitSize")]
        public decimal ContractSize { get; set; }
        /// <summary>
        /// Maker fee rate
        /// </summary>
        [JsonPropertyName("makerFeeRate")]
        public decimal MakerFeeRate { get; set; }
        /// <summary>
        /// Taker fee rate
        /// </summary>
        [JsonPropertyName("takerFeeRate")]
        public decimal TakerFeeRate { get; set; }
        /// <summary>
        /// Settlement fee rate
        /// </summary>
        [JsonPropertyName("settlementFeeRate")]
        public decimal? SettlementFeeRate { get; set; }
        /// <summary>
        /// Max leverage
        /// </summary>
        [JsonPropertyName("maxLeverage")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// Index source exchanges
        /// </summary>
        [JsonPropertyName("indexSourceExchanges")]
        public string[] IndexSourceExchanges { get; set; } = [];
        /// <summary>
        /// Maintenance margin ratio limit
        /// </summary>
        [JsonPropertyName("mmrLimit")]
        public decimal MaintenanceMarginRatioLimit { get; set; }
        /// <summary>
        /// Maintenance margin ratio leverage constant
        /// </summary>
        [JsonPropertyName("mmrLevConstant")]
        public decimal MaintenanceMarginRatioLeverageConstant { get; set; }
    }


}
