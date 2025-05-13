using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using Kucoin.Net.Enums;


namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Base record for position info
    /// </summary>
    [SerializationModel]
    public record KucoinPositionBase
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Auto deposit margin or not
        /// </summary>
        [JsonPropertyName("autoDeposit")]
        public bool AutoDeposit { get; set; }
        /// <summary>
        /// Maintenance margin requirement
        /// </summary>
        [JsonPropertyName("maintMarginReq")]
        public decimal MaintenanceMarginRequirement { get; set; }
        /// <summary>
        /// Risk limit 
        /// </summary>
        [JsonPropertyName("riskLimit")]
        public decimal RiskLimit { get; set; }
        /// <summary>
        /// Leverage off the order
        /// </summary>
        [JsonPropertyName("realLeverage")]
        public decimal RealLeverage { get; set; }
        /// <summary>
        /// Cross mode or not
        /// </summary>
        [JsonPropertyName("crossMode")]
        public bool CrossMode { get; set; }
        /// <summary>
        /// ADL ranking percentile
        /// </summary>
        [JsonPropertyName("delevPercentage")]
        public decimal DeleveragePercentage { get; set; }
        /// <summary>
        /// Opening time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("openingTimestamp")]
        public DateTime? OpenTime { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("currentTimestamp")]
        public DateTime CurrentTime { get; set; }
        /// <summary>
        /// Current position quantity
        /// </summary>
        [JsonPropertyName("currentQty")]
        public decimal CurrentQuantity { get; set; }
        /// <summary>
        /// Current position value
        /// </summary>
        [JsonPropertyName("currentCost")]
        public decimal CurrentCost { get; set; }
        /// <summary>
        /// Current commission
        /// </summary>
        [JsonPropertyName("currentComm")]
        public decimal CurrentCommission { get; set; }
        /// <summary>
        /// Unrealized value
        /// </summary>
        [JsonPropertyName("unrealisedCost")]
        public decimal UnrealizedCost { get; set; }
        /// <summary>
        /// Accumulated realized gross profit value
        /// </summary>
        [JsonPropertyName("realisedGrossCost")]
        public decimal RealizedGrossCost { get; set; }
        /// <summary>
        /// Current realized position value
        /// </summary>
        [JsonPropertyName("realisedCost")]
        public decimal RealizedCost { get; set; }
        /// <summary>
        /// Is open
        /// </summary>
        [JsonPropertyName("isOpen")]
        public bool IsOpen { get; set; }
        /// <summary>
        /// Is inverse
        /// </summary>
        [JsonPropertyName("isInverse")]
        public bool IsInverse { get; set; }
        /// <summary>
        /// Mark price
        /// </summary>
        [JsonPropertyName("markPrice")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// Mark value
        /// </summary>
        [JsonPropertyName("markValue")]
        public decimal MarkValue { get; set; }
        /// <summary>
        /// Position value
        /// </summary>
        [JsonPropertyName("posCost")]
        public decimal PositionValue { get; set; }
        /// <summary>
        /// Added margin
        /// </summary>
        [JsonPropertyName("posCross")]
        public decimal PositionCross { get; set; }
        /// <summary>
        /// Leverage margin
        /// </summary>
        [JsonPropertyName("posInit")]
        public decimal PositionInit { get; set; }
        /// <summary>
        /// Bankruptcy cost
        /// </summary>
        [JsonPropertyName("posComm")]
        public decimal PositionComm { get; set; }
        /// <summary>
        /// Funding fees paid out
        /// </summary>
        [JsonPropertyName("posLoss")]
        public decimal PositionLoss { get; set; }
        /// <summary>
        /// Position margin
        /// </summary>
        [JsonPropertyName("posMargin")]
        public decimal PositionMargin { get; set; }
        /// <summary>
        /// Maintenance margin
        /// </summary>
        [JsonPropertyName("posMaint")]
        public decimal PositionMaintenance { get; set; }
        /// <summary>
        /// Position margin
        /// </summary>
        [JsonPropertyName("maintMargin")]
        public decimal MaintenanceMargin { get; set; }
        /// <summary>
        /// Accumulated realized gross profit value
        /// </summary>
        [JsonPropertyName("realisedGrossPnl")]
        public decimal RealizedGrossPnl { get; set; }
        /// <summary>
        /// realized profit and loss
        /// </summary>
        [JsonPropertyName("realisedPnl")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealisedPnl")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// Profit-loss ratio of the position
        /// </summary>
        [JsonPropertyName("unrealisedPnlPcnt")]
        public decimal UnrealizedPnlPercentage { get; set; }
        /// <summary>
        /// Rate of return on investment
        /// </summary>
        [JsonPropertyName("unrealisedRoePcnt")]
        public decimal UnrealizedRoePercentage { get; set; }
        /// <summary>
        /// Average entry price
        /// </summary>
        [JsonPropertyName("avgEntryPrice")]
        public decimal AverageEntryPrice { get; set; }
        /// <summary>
        /// Liquidation price
        /// </summary>
        [JsonPropertyName("liquidationPrice")]
        public decimal LiquidationPrice { get; set; }
        /// <summary>
        /// Bankruptcy price
        /// </summary>
        [JsonPropertyName("bankruptPrice")]
        public decimal BankruptPrice { get; set; }
        /// <summary>
        /// Asset used to clear and settle the trades
        /// </summary>
        [JsonPropertyName("settleCurrency")]
        public string SettleAsset { get; set; } = string.Empty;
        /// <summary>
        /// Risk limit level
        /// </summary>
        [JsonPropertyName("riskLimitLevel")]
        public int? RiskLimitLevel { get; set; }
        /// <summary>
        /// User id
        /// </summary>
        [JsonPropertyName("userId")]
        public long? UserId { get; set; }
        /// <summary>
        /// Maintenance margin rate
        /// </summary>
        [JsonPropertyName("maintainMargin")]
        public decimal MaintenanceMarginRate { get; set; }
        /// <summary>
        /// Margin mode
        /// </summary>

        [JsonPropertyName("marginMode")]
        public FuturesMarginMode? MarginMode { get; set; }
        /// <summary>
        /// Position side
        /// </summary>

        [JsonPropertyName("positionSide")]
        public PositionSide? PositionSide { get; set; }
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal? Leverage { get; set; }
        /// <summary>
        /// The current remaining unsettled funding fee for the position Only applicable to Isolated Margin
        /// </summary>
        [JsonPropertyName("posFunding")]
        public decimal? PositionFunding { get; set; }
    }

    /// <summary>
    /// Position info
    /// </summary>
    [SerializationModel]
    public record KucoinPosition: KucoinPositionBase
    {
        /// <summary>
        /// Position id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
    }

    /// <summary>
    /// Position update
    /// </summary>
    [SerializationModel]
    public record KucoinPositionUpdate: KucoinPositionBase
    {
        /// <summary>
        /// Change reason
        /// </summary>
        [JsonPropertyName("changeReason")]
        public string ChangeReason { get; set; } = string.Empty;
    }
}
