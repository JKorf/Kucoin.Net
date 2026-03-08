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
        /// ["<c>oi</c>"] Order id
        /// </summary>
        [JsonPropertyName("oi")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>ci</c>"] Client order id
        /// </summary>
        [JsonPropertyName("ci")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>os</c>"] Order status
        /// </summary>
        [JsonPropertyName("os")]
        public UnifiedOrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>eT</c>"] Order event type
        /// </summary>
        [JsonPropertyName("eT")]
        public UnifiedOrderUpdateType EventType { get; set; }
        /// <summary>
        /// ["<c>tT</c>"] Trade type
        /// </summary>
        [JsonPropertyName("tT")]
        public UnifiedAccountType TradeType { get; set; }
        /// <summary>
        /// ["<c>s</c>"] Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>S</c>"] Side
        /// </summary>
        [JsonPropertyName("S")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>oT</c>"] Order type
        /// </summary>
        [JsonPropertyName("oT")]
        public OrderType? OrderType { get; set; }
        /// <summary>
        /// ["<c>pS</c>"] Position side
        /// </summary>
        [JsonPropertyName("pS")]
        public PositionSide? PositionSide { get; set; }
        /// <summary>
        /// ["<c>lR</c>"] Liquidity role of the last trade for this order
        /// </summary>
        [JsonPropertyName("lR")]
        public LiquidityType? LastTradeRole { get; set; }
        /// <summary>
        /// ["<c>oS</c>"] Order source
        /// </summary>
        [JsonPropertyName("oS")]
        public string? OrderSource { get; set; }
        /// <summary>
        /// ["<c>p</c>"] Order price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>l</c>"] Leverage
        /// </summary>
        [JsonPropertyName("l")]
        public decimal? Leverage { get; set; }
        /// <summary>
        /// ["<c>mM</c>"] Margin mode
        /// </summary>
        [JsonPropertyName("mM")]
        public MarginMode? MarginMode { get; set; }
        /// <summary>
        /// ["<c>ti</c>"] Trade id of the last trade for this order
        /// </summary>
        [JsonPropertyName("ti")]
        public long? LastTradeId { get; set; }
        /// <summary>
        /// ["<c>q</c>"] Order quantity
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>qU</c>"] Order quantity unit
        /// </summary>
        [JsonPropertyName("qU")]
        public QuantityUnit? QuantityUnit { get; set; }
        /// <summary>
        /// ["<c>fS</c>"] Order quantity filled
        /// </summary>
        [JsonPropertyName("fS")]
        public decimal TotalQuantityFilled { get; set; }
        /// <summary>
        /// ["<c>lS</c>"] Quantity of the last trade for this order
        /// </summary>
        [JsonPropertyName("lS")]
        public decimal? LastTradeQuantity { get; set; }
        /// <summary>
        /// ["<c>ls</c>"] Price of the last trade for this order
        /// </summary>
        [JsonPropertyName("ls")]
        public decimal? LastTradePrice { get; set; }
        /// <summary>
        /// ["<c>aP</c>"] Average fill price
        /// </summary>
        [JsonPropertyName("aP")]
        public decimal? AveragePrice { get; set; }
        /// <summary>
        /// ["<c>f</c>"] Total settle fee
        /// </summary>
        [JsonPropertyName("f")]
        public decimal? TotalSettleFee { get; set; }
        /// <summary>
        /// ["<c>fC</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("fC")]
        public string? FeeAsset { get; set; }
        /// <summary>
        /// ["<c>t</c>"] Total tax
        /// </summary>
        [JsonPropertyName("t")]
        public decimal? TotalTax { get; set; }
        /// <summary>
        /// ["<c>cR</c>"] Cancellation reason
        /// </summary>
        [JsonPropertyName("cR")]
        public string? CancellationReason { get; set; }
        /// <summary>
        /// ["<c>cS</c>"] Canceled quantity
        /// </summary>
        [JsonPropertyName("cS")]
        public decimal? CanceledQuantity { get; set; }
        /// <summary>
        /// ["<c>rS</c>"] Canceled quantity
        /// </summary>
        [JsonPropertyName("rS")]
        public decimal? RemainingQuantity { get; set; }
        /// <summary>
        /// ["<c>tD</c>"] Trigger direction
        /// </summary>
        [JsonPropertyName("tD")]
        public StopType? TriggerDirection { get; set; }
        /// <summary>
        /// ["<c>tP</c>"] Trigger price
        /// </summary>
        [JsonPropertyName("tP")]
        public decimal? TriggerPrice { get; set; }
        /// <summary>
        /// ["<c>tPT</c>"] Trigger price type
        /// </summary>
        [JsonPropertyName("tPT")]
        public StopPriceType? TriggerPriceType { get; set; }
        /// <summary>
        /// ["<c>pP</c>"] Take profit trigger price
        /// </summary>
        [JsonPropertyName("pP")]
        public decimal? TakeProfitTriggerPrice { get; set; }
        /// <summary>
        /// ["<c>pPT</c>"] Take profit trigger price
        /// </summary>
        [JsonPropertyName("pPT")]
        public StopPriceType? TakeProfitTriggerPriceType { get; set; }
        /// <summary>
        /// ["<c>pOP</c>"] Take profit execution price
        /// </summary>
        [JsonPropertyName("pOP")]
        public decimal? TakeProfitExecutionPrice { get; set; }
        /// <summary>
        /// ["<c>lP</c>"] Stop loss trigger price
        /// </summary>
        [JsonPropertyName("lP")]
        public decimal? StopLossTriggerPrice { get; set; }
        /// <summary>
        /// ["<c>lOP</c>"] Take profit execution price
        /// </summary>
        [JsonPropertyName("lOP")]
        public decimal? StopLossExecutionPrice { get; set; }
        /// <summary>
        /// ["<c>lPT</c>"] Stop loss trigger price
        /// </summary>
        [JsonPropertyName("lPT")]
        public StopPriceType? StopLossTriggerPriceType { get; set; }
        /// <summary>
        /// ["<c>toi</c>"] Original order ID that was triggered
        /// </summary>
        [JsonPropertyName("toi")]
        public string? OriginalTriggerOrderId { get; set; }
        /// <summary>
        /// ["<c>stp</c>"] Self trade prevention mode
        /// </summary>
        [JsonPropertyName("stp")]
        public SelfTradePrevention? Stp { get; set; }
        /// <summary>
        /// ["<c>rO</c>"] Reduce only
        /// </summary>
        [JsonPropertyName("rO")]
        public bool? ReduceOnly { get; set; }
        /// <summary>
        /// ["<c>pO</c>"] Post only
        /// </summary>
        [JsonPropertyName("pO")]
        public bool? PostOnly { get; set; }
        /// <summary>
        /// ["<c>tIF</c>"] Time in force
        /// </summary>
        [JsonPropertyName("tIF")]
        public TimeInForce? TimeInForce { get; set; }
        /// <summary>
        /// ["<c>O</c>"] Order creation time
        /// </summary>
        [JsonPropertyName("O")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>U</c>"] Order update time
        /// </summary>
        [JsonPropertyName("U")]
        public DateTime UpdateTime { get; set; }
    }
}
