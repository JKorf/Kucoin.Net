using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects.Futures
{
    /// <summary>
    /// Position info
    /// </summary>
    public class KucoinPosition
    {
        /// <summary>
        /// Position id
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Auto deposit margin or not
        /// </summary>
        public bool AutoDeposit { get; set; }
        /// <summary>
        /// Maintenance margin requirement
        /// </summary>
        [JsonProperty("maintMarginReq")]
        public decimal MaintenanceMarginRequirement { get; set; }
        /// <summary>
        /// Risk limit 
        /// </summary>
        public decimal RiskLimit { get; set; }
        /// <summary>
        /// Leverage off the order
        /// </summary>
        public decimal RealLeverage { get; set; }
        /// <summary>
        /// Cross mode or not
        /// </summary>
        public bool CrossMode { get; set; }
        /// <summary>
        /// ADL ranking percentile
        /// </summary>
        [JsonProperty("delevPercentage")]
        public decimal DeleveragePercentage { get; set; }
        /// <summary>
        /// Opening time
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        [JsonProperty("openingTimestamp")]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        [JsonProperty("currentTimestamp")]
        public DateTime CurrentTime { get; set; }
        /// <summary>
        /// Current position quantity
        /// </summary>
        [JsonProperty("currentQty")]
        public decimal CurrentQuantity { get; set; }
        /// <summary>
        /// Current position value
        /// </summary>
        public decimal CurrentCost { get; set; }
        /// <summary>
        /// Current commission
        /// </summary>
        [JsonProperty("currentComm")]
        public decimal CurrentCommission { get; set; }
        /// <summary>
        /// Unrealised value
        /// </summary>
        public decimal UnrealisedCost { get; set; }
        /// <summary>
        /// Accumulated realised gross profit value
        /// </summary>
        public decimal RealisedGrossCost { get; set; }
        /// <summary>
        /// Current realised position value
        /// </summary>
        public decimal RealisedCost { get; set; }
        /// <summary>
        /// Is open
        /// </summary>
        public bool IsOpen { get; set; }
        /// <summary>
        /// Mark price
        /// </summary>
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// Mark value
        /// </summary>
        public decimal MarkValue { get; set; }
        /// <summary>
        /// Position value
        /// </summary>
        [JsonProperty("posCost")]
        public decimal PositionValue { get; set; }
        /// <summary>
        /// Added margin
        /// </summary>
        [JsonProperty("posCross")]
        public decimal PositionCross { get; set; }
        /// <summary>
        /// Leverage margin
        /// </summary>
        [JsonProperty("posInit")]
        public decimal PositionInit { get; set; }
        /// <summary>
        /// Bankruptcy cost
        /// </summary>
        [JsonProperty("posComm")]
        public decimal PositionComm { get; set; }
        /// <summary>
        /// Funding fees paid out
        /// </summary>
        [JsonProperty("posLoss")]
        public decimal PositionLoss { get; set; }
        /// <summary>
        /// Position margin
        /// </summary>
        [JsonProperty("posMargin")]
        public decimal PositionMargin { get; set; }
        /// <summary>
        /// Maintenance margin
        /// </summary>
        [JsonProperty("posMaint")]
        public decimal PositionMaintenance { get; set; }
        /// <summary>
        /// Position margin
        /// </summary>
        [JsonProperty("maintMargin")]
        public decimal MaintenanceMargin { get; set; }
        /// <summary>
        /// Accumulated realised gross profit value
        /// </summary>
        public decimal RealisedGrossPnl { get; set; }
        /// <summary>
        /// Realised profit and loss
        /// </summary>
        public decimal RealisedPnl { get; set; }
        /// <summary>
        /// Unrealised profit and loss
        /// </summary>
        public decimal UnrealisedPnl { get; set; }
        /// <summary>
        /// Profit-loss ratio of the position
        /// </summary>
        [JsonProperty("unrealisedPnlPcnt")]
        public decimal UnrealisedPnlPercentage { get; set; }
        /// <summary>
        /// Rate of return on investment
        /// </summary>
        [JsonProperty("unrealisedRoePcnt")]
        public decimal UnrealisedRoePercentage { get; set; }
        /// <summary>
        /// Average entry price
        /// </summary>
        [JsonProperty("avgEntryPrice")]
        public decimal AverageEntryPrice { get; set; }
        /// <summary>
        /// Liquidation price
        /// </summary>
        public decimal LiquidationPrice { get; set; }
        /// <summary>
        /// Bankruptcy price
        /// </summary>
        public decimal BankruptPrice { get; set; }
        /// <summary>
        /// Asset used to clear and settle the trades
        /// </summary>
        [JsonProperty("settleCurrency")]
        public string SettleAsset { get; set; } = string.Empty;
    }
}
