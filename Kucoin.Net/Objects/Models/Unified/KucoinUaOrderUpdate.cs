using Kucoin.Net.Enums;
using Kucoin.Net.Objects.Models.Spot;
using System;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Order update
    /// </summary>
    public record KucoinUaOrderUpdate
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("oi")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("ci")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonPropertyName("os")]
        public UnifiedOrderStatus Status { get; set; }
        /// <summary>
        /// Order event type
        /// </summary>
        [JsonPropertyName("eT")]
        public UnifiedOrderUpdateType EventType { get; set; }
        /// <summary>
        /// Trade type
        /// </summary>
        [JsonPropertyName("tT")]
        public UnifiedAccountType TradeType { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Side
        /// </summary>
        [JsonPropertyName("S")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("oT")]
        public OrderType? OrderType { get; set; }
        /// <summary>
        /// Position side
        /// </summary>
        [JsonPropertyName("pS")]
        public PositionSide? PositionSide { get; set; }
        /// <summary>
        /// Liquidity role of the last trade for this order
        /// </summary>
        [JsonPropertyName("lR")]
        public LiquidityType? LastTradeRole { get; set; }
        /// <summary>
        /// Order source
        /// </summary>
        [JsonPropertyName("oS")]
        public string? OrderSource { get; set; }
        /// <summary>
        /// Order price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal? Price { get; set; }
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("l")]
        public decimal? Leverage { get; set; }
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonPropertyName("mM")]
        public MarginMode? MarginMode { get; set; }
        /// <summary>
        /// Trade id of the last trade for this order
        /// </summary>
        [JsonPropertyName("ti")]
        public long? LastTradeId { get; set; }
        /// <summary>
        /// Order quantity
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Order quantity unit
        /// </summary>
        [JsonPropertyName("qU")]
        public QuantityUnit? QuantityUnit { get; set; }
        /// <summary>
        /// Order quantity filled
        /// </summary>
        [JsonPropertyName("fS")]
        public decimal TotalQuantityFilled { get; set; }
        /// <summary>
        /// Quantity of the last trade for this order
        /// </summary>
        [JsonPropertyName("lS")]
        public decimal? LastTradeQuantity { get; set; }
        /// <summary>
        /// Price of the last trade for this order
        /// </summary>
        [JsonPropertyName("ls")]
        public decimal? LastTradePrice { get; set; }
        /// <summary>
        /// Average fill price
        /// </summary>
        [JsonPropertyName("aP")]
        public decimal? AveragePrice { get; set; }
        /// <summary>
        /// Total settle fee
        /// </summary>
        [JsonPropertyName("f")]
        public decimal? TotalSettleFee { get; set; }
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonPropertyName("fC")]
        public string? FeeAsset { get; set; }
        /// <summary>
        /// Total tax
        /// </summary>
        [JsonPropertyName("t")]
        public decimal? TotalTax { get; set; }
        /// <summary>
        /// Cancellation reason
        /// </summary>
        [JsonPropertyName("cR")]
        public string? CancellationReason { get; set; }
        /// <summary>
        /// Canceled quantity
        /// </summary>
        [JsonPropertyName("cS")]
        public decimal? CanceledQuantity { get; set; }
        /// <summary>
        /// Canceled quantity
        /// </summary>
        [JsonPropertyName("rS")]
        public decimal? RemainingQuantity { get; set; }
        /// <summary>
        /// Trigger direction
        /// </summary>
        [JsonPropertyName("tD")]
        public StopType? TriggerDirection { get; set; }
        /// <summary>
        /// Trigger price
        /// </summary>
        [JsonPropertyName("tP")]
        public decimal? TriggerPrice { get; set; }
        /// <summary>
        /// Trigger price type
        /// </summary>
        [JsonPropertyName("tPT")]
        public StopPriceType? TriggerPriceType { get; set; }
        /// <summary>
        /// Take profit trigger price
        /// </summary>
        [JsonPropertyName("pP")]
        public decimal? TakeProfitTriggerPrice { get; set; }
        /// <summary>
        /// Take profit trigger price
        /// </summary>
        [JsonPropertyName("pPT")]
        public StopPriceType? TakeProfitTriggerPriceType { get; set; }
        /// <summary>
        /// Take profit execution price
        /// </summary>
        [JsonPropertyName("pOP")]
        public decimal? TakeProfitExecutionPrice { get; set; }
        /// <summary>
        /// Stop loss trigger price
        /// </summary>
        [JsonPropertyName("lP")]
        public decimal? StopLossTriggerPrice { get; set; }
        /// <summary>
        /// Take profit execution price
        /// </summary>
        [JsonPropertyName("lOP")]
        public decimal? StopLossExecutionPrice { get; set; }
        /// <summary>
        /// Stop loss trigger price
        /// </summary>
        [JsonPropertyName("lPT")]
        public StopPriceType? StopLossTriggerPriceType { get; set; }
        /// <summary>
        /// Original order ID that was triggered
        /// </summary>
        [JsonPropertyName("toi")]
        public string? OriginalTriggerOrderId { get; set; }
        /// <summary>
        /// Self trade prevention mode
        /// </summary>
        [JsonPropertyName("stp")]
        public SelfTradePrevention? Stp { get; set; }
        /// <summary>
        /// Reduce only
        /// </summary>
        [JsonPropertyName("rO")]
        public bool? ReduceOnly { get; set; }
        /// <summary>
        /// Post only
        /// </summary>
        [JsonPropertyName("pO")]
        public bool? PostOnly { get; set; }
        /// <summary>
        /// Time in force
        /// </summary>
        [JsonPropertyName("tIF")]
        public TimeInForce? TimeInForce { get; set; }
        /// <summary>
        /// Order creation time
        /// </summary>
        [JsonPropertyName("O")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Order update time
        /// </summary>
        [JsonPropertyName("U")]
        public DateTime UpdateTime { get; set; }
    }
}
