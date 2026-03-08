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
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>autoDeposit</c>"] Auto deposit margin or not
        /// </summary>
        [JsonPropertyName("autoDeposit")]
        public bool AutoDeposit { get; set; }
        /// <summary>
        /// ["<c>maintMarginReq</c>"] Maintenance margin requirement
        /// </summary>
        [JsonPropertyName("maintMarginReq")]
        public decimal MaintenanceMarginRequirement { get; set; }
        /// <summary>
        /// ["<c>riskLimit</c>"] Risk limit 
        /// </summary>
        [JsonPropertyName("riskLimit")]
        public decimal RiskLimit { get; set; }
        /// <summary>
        /// ["<c>realLeverage</c>"] Leverage off the order
        /// </summary>
        [JsonPropertyName("realLeverage")]
        public decimal RealLeverage { get; set; }
        /// <summary>
        /// ["<c>crossMode</c>"] Cross mode or not
        /// </summary>
        [JsonPropertyName("crossMode")]
        public bool CrossMode { get; set; }
        /// <summary>
        /// ["<c>delevPercentage</c>"] ADL ranking percentile
        /// </summary>
        [JsonPropertyName("delevPercentage")]
        public decimal DeleveragePercentage { get; set; }
        /// <summary>
        /// ["<c>openingTimestamp</c>"] Opening time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("openingTimestamp")]
        public DateTime? OpenTime { get; set; }
        /// <summary>
        /// ["<c>currentTimestamp</c>"] Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("currentTimestamp")]
        public DateTime CurrentTime { get; set; }
        /// <summary>
        /// ["<c>currentQty</c>"] Current position quantity
        /// </summary>
        [JsonPropertyName("currentQty")]
        public decimal CurrentQuantity { get; set; }
        /// <summary>
        /// ["<c>currentCost</c>"] Current position value
        /// </summary>
        [JsonPropertyName("currentCost")]
        public decimal CurrentCost { get; set; }
        /// <summary>
        /// ["<c>currentComm</c>"] Current commission
        /// </summary>
        [JsonPropertyName("currentComm")]
        public decimal CurrentCommission { get; set; }
        /// <summary>
        /// ["<c>unrealisedCost</c>"] Unrealized value
        /// </summary>
        [JsonPropertyName("unrealisedCost")]
        public decimal UnrealizedCost { get; set; }
        /// <summary>
        /// ["<c>realisedGrossCost</c>"] Accumulated realized gross profit value
        /// </summary>
        [JsonPropertyName("realisedGrossCost")]
        public decimal RealizedGrossCost { get; set; }
        /// <summary>
        /// ["<c>realisedCost</c>"] Current realized position value
        /// </summary>
        [JsonPropertyName("realisedCost")]
        public decimal RealizedCost { get; set; }
        /// <summary>
        /// ["<c>isOpen</c>"] Is open
        /// </summary>
        [JsonPropertyName("isOpen")]
        public bool IsOpen { get; set; }
        /// <summary>
        /// ["<c>isInverse</c>"] Is inverse
        /// </summary>
        [JsonPropertyName("isInverse")]
        public bool IsInverse { get; set; }
        /// <summary>
        /// ["<c>markPrice</c>"] Mark price
        /// </summary>
        [JsonPropertyName("markPrice")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// ["<c>markValue</c>"] Mark value
        /// </summary>
        [JsonPropertyName("markValue")]
        public decimal MarkValue { get; set; }
        /// <summary>
        /// ["<c>posCost</c>"] Position value
        /// </summary>
        [JsonPropertyName("posCost")]
        public decimal PositionValue { get; set; }
        /// <summary>
        /// ["<c>posCross</c>"] Added margin
        /// </summary>
        [JsonPropertyName("posCross")]
        public decimal PositionCross { get; set; }
        /// <summary>
        /// ["<c>posInit</c>"] Leverage margin
        /// </summary>
        [JsonPropertyName("posInit")]
        public decimal PositionInit { get; set; }
        /// <summary>
        /// ["<c>posComm</c>"] Bankruptcy cost
        /// </summary>
        [JsonPropertyName("posComm")]
        public decimal PositionComm { get; set; }
        /// <summary>
        /// ["<c>posLoss</c>"] Funding fees paid out
        /// </summary>
        [JsonPropertyName("posLoss")]
        public decimal PositionLoss { get; set; }
        /// <summary>
        /// ["<c>posMargin</c>"] Position margin
        /// </summary>
        [JsonPropertyName("posMargin")]
        public decimal PositionMargin { get; set; }
        /// <summary>
        /// ["<c>posMaint</c>"] Maintenance margin
        /// </summary>
        [JsonPropertyName("posMaint")]
        public decimal PositionMaintenance { get; set; }
        /// <summary>
        /// ["<c>maintMargin</c>"] Position margin
        /// </summary>
        [JsonPropertyName("maintMargin")]
        public decimal MaintenanceMargin { get; set; }
        /// <summary>
        /// ["<c>realisedGrossPnl</c>"] Accumulated realized gross profit value
        /// </summary>
        [JsonPropertyName("realisedGrossPnl")]
        public decimal RealizedGrossPnl { get; set; }
        /// <summary>
        /// ["<c>realisedPnl</c>"] realized profit and loss
        /// </summary>
        [JsonPropertyName("realisedPnl")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// ["<c>unrealisedPnl</c>"] Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealisedPnl")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>unrealisedPnlPcnt</c>"] Profit-loss ratio of the position
        /// </summary>
        [JsonPropertyName("unrealisedPnlPcnt")]
        public decimal UnrealizedPnlPercentage { get; set; }
        /// <summary>
        /// ["<c>unrealisedRoePcnt</c>"] Rate of return on investment
        /// </summary>
        [JsonPropertyName("unrealisedRoePcnt")]
        public decimal UnrealizedRoePercentage { get; set; }
        /// <summary>
        /// ["<c>avgEntryPrice</c>"] Average entry price
        /// </summary>
        [JsonPropertyName("avgEntryPrice")]
        public decimal AverageEntryPrice { get; set; }
        /// <summary>
        /// ["<c>liquidationPrice</c>"] Liquidation price
        /// </summary>
        [JsonPropertyName("liquidationPrice")]
        public decimal LiquidationPrice { get; set; }
        /// <summary>
        /// ["<c>bankruptPrice</c>"] Bankruptcy price
        /// </summary>
        [JsonPropertyName("bankruptPrice")]
        public decimal BankruptPrice { get; set; }
        /// <summary>
        /// ["<c>settleCurrency</c>"] Asset used to clear and settle the trades
        /// </summary>
        [JsonPropertyName("settleCurrency")]
        public string SettleAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>riskLimitLevel</c>"] Risk limit level
        /// </summary>
        [JsonPropertyName("riskLimitLevel")]
        public int? RiskLimitLevel { get; set; }
        /// <summary>
        /// ["<c>userId</c>"] User id
        /// </summary>
        [JsonPropertyName("userId")]
        public long? UserId { get; set; }
        /// <summary>
        /// ["<c>maintainMargin</c>"] Maintenance margin rate
        /// </summary>
        [JsonPropertyName("maintainMargin")]
        public decimal MaintenanceMarginRate { get; set; }
        /// <summary>
        /// ["<c>marginMode</c>"] Margin mode
        /// </summary>

        [JsonPropertyName("marginMode")]
        public FuturesMarginMode? MarginMode { get; set; }
        /// <summary>
        /// ["<c>positionSide</c>"] Position side
        /// </summary>

        [JsonPropertyName("positionSide")]
        public PositionSide? PositionSide { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal? Leverage { get; set; }
        /// <summary>
        /// ["<c>posFunding</c>"] The current remaining unsettled funding fee for the position Only applicable to Isolated Margin
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
        /// ["<c>id</c>"] Position id
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
        /// ["<c>changeReason</c>"] Change reason
        /// </summary>
        [JsonPropertyName("changeReason")]
        public string ChangeReason { get; set; } = string.Empty;
    }
}
