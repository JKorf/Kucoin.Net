using Kucoin.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Order info
    /// </summary>
    public record KucoinUaOrder
    {
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>orderType</c>"] Order type
        /// </summary>
        [JsonPropertyName("orderType")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>positionSide</c>"] Position side
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide? PositionSide { get; set; }
        /// <summary>
        /// ["<c>size</c>"] Quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>timeInForce</c>"] Time in force
        /// </summary>
        [JsonPropertyName("timeInForce")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// ["<c>orderTime</c>"] Order time
        /// </summary>
        [JsonPropertyName("orderTime")]
        public DateTime OrderTime { get; set; }
        /// <summary>
        /// ["<c>stp</c>"] Self trade prevention mode
        /// </summary>
        [JsonPropertyName("stp")]
        public SelfTradePrevention? Stp { get; set; }
        /// <summary>
        /// ["<c>cancelAfter</c>"] Cancel after
        /// </summary>
        [JsonPropertyName("cancelAfter")]
        public long? CancelAfter { get; set; }
        /// <summary>
        /// ["<c>postOnly</c>"] Post only
        /// </summary>
        [JsonPropertyName("postOnly")]
        public bool PostOnly { get; set; }
        /// <summary>
        /// ["<c>reduceOnly</c>"] Reduce only
        /// </summary>
        [JsonPropertyName("reduceOnly")]
        public bool ReduceOnly { get; set; }
        /// <summary>
        /// ["<c>triggerDirection</c>"] Trigger direction
        /// </summary>
        [JsonPropertyName("triggerDirection")]
        public StopType? TriggerDirection { get; set; }
        /// <summary>
        /// ["<c>triggerPrice</c>"] Trigger price
        /// </summary>
        [JsonPropertyName("triggerPrice")]
        public decimal? TriggerPrice { get; set; }
        /// <summary>
        /// ["<c>triggerPriceType</c>"] Trigger price type
        /// </summary>
        [JsonPropertyName("triggerPriceType")]
        public StopPriceType? TriggerPriceType { get; set; }
        /// <summary>
        /// ["<c>tpTriggerPrice</c>"] Tp trigger price
        /// </summary>
        [JsonPropertyName("tpTriggerPrice")]
        public decimal? TpTriggerPrice { get; set; }
        /// <summary>
        /// ["<c>tpTriggerPriceType</c>"] Tp trigger price type
        /// </summary>
        [JsonPropertyName("tpTriggerPriceType")]
        public StopPriceType? TpTriggerPriceType { get; set; }
        /// <summary>
        /// ["<c>slTriggerPrice</c>"] Sl trigger price
        /// </summary>
        [JsonPropertyName("slTriggerPrice")]
        public decimal? SlTriggerPrice { get; set; }
        /// <summary>
        /// ["<c>slTriggerPriceType</c>"] Sl trigger price type
        /// </summary>
        [JsonPropertyName("slTriggerPriceType")]
        public StopPriceType? SlTriggerPriceType { get; set; }
        /// <summary>
        /// ["<c>filledSize</c>"] Quantity filled
        /// </summary>
        [JsonPropertyName("filledSize")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>avgPrice</c>"] Average price
        /// </summary>
        [JsonPropertyName("avgPrice")]
        public decimal AveragePrice { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>feeCurrency</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("feeCurrency")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>tax</c>"] Tax
        /// </summary>
        [JsonPropertyName("tax")]
        public decimal Tax { get; set; }
        /// <summary>
        /// ["<c>updatedTime</c>"] Updated time
        /// </summary>
        [JsonPropertyName("updatedTime")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// ["<c>triggerOrderId</c>"] Trigger order id
        /// </summary>
        [JsonPropertyName("triggerOrderId")]
        public string? TriggerOrderId { get; set; }
        /// <summary>
        /// ["<c>cancelReason</c>"] Cancel reason
        /// </summary>
        [JsonPropertyName("cancelReason"), JsonConverter(typeof(NumberStringConverter))]
        public string? CancelReason { get; set; }
        /// <summary>
        /// ["<c>cancelSize</c>"] Cancel quantity
        /// </summary>
        [JsonPropertyName("cancelSize")]
        public decimal CancelQuantity { get; set; }
        /// <summary>
        /// ["<c>clientOid</c>"] Client order id
        /// </summary>
        [JsonPropertyName("clientOid")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>sizeUnit</c>"] Quantity unit
        /// </summary>
        [JsonPropertyName("sizeUnit")]
        public QuantityUnit QuantityUnit { get; set; }
        /// <summary>
        /// ["<c>tradeType</c>"] Trade type
        /// </summary>
        [JsonPropertyName("tradeType")]
        public UnifiedAccountType? AccountType { get; set; }
        /// <summary>
        /// ["<c>tradeId</c>"] Last order trade id
        /// </summary>
        [JsonPropertyName("tradeId")]
        public string? TradeId { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Order status
        /// </summary>
        [JsonPropertyName("status")]
        public UnifiedOrderStatus Status { get; set; }
    }


}
