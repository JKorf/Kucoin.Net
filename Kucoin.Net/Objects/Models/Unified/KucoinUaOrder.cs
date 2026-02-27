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
        /// Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("orderType")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Position side
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide? PositionSide { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// Time in force
        /// </summary>
        [JsonPropertyName("timeInForce")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// Order time
        /// </summary>
        [JsonPropertyName("orderTime")]
        public DateTime OrderTime { get; set; }
        /// <summary>
        /// Self trade prevention mode
        /// </summary>
        [JsonPropertyName("stp")]
        public SelfTradePrevention? Stp { get; set; }
        /// <summary>
        /// Cancel after
        /// </summary>
        [JsonPropertyName("cancelAfter")]
        public long? CancelAfter { get; set; }
        /// <summary>
        /// Post only
        /// </summary>
        [JsonPropertyName("postOnly")]
        public bool PostOnly { get; set; }
        /// <summary>
        /// Reduce only
        /// </summary>
        [JsonPropertyName("reduceOnly")]
        public bool ReduceOnly { get; set; }
        /// <summary>
        /// Trigger direction
        /// </summary>
        [JsonPropertyName("triggerDirection")]
        public StopType? TriggerDirection { get; set; }
        /// <summary>
        /// Trigger price
        /// </summary>
        [JsonPropertyName("triggerPrice")]
        public decimal? TriggerPrice { get; set; }
        /// <summary>
        /// Trigger price type
        /// </summary>
        [JsonPropertyName("triggerPriceType")]
        public StopPriceType? TriggerPriceType { get; set; }
        /// <summary>
        /// Tp trigger price
        /// </summary>
        [JsonPropertyName("tpTriggerPrice")]
        public decimal? TpTriggerPrice { get; set; }
        /// <summary>
        /// Tp trigger price type
        /// </summary>
        [JsonPropertyName("tpTriggerPriceType")]
        public StopPriceType? TpTriggerPriceType { get; set; }
        /// <summary>
        /// Sl trigger price
        /// </summary>
        [JsonPropertyName("slTriggerPrice")]
        public decimal? SlTriggerPrice { get; set; }
        /// <summary>
        /// Sl trigger price type
        /// </summary>
        [JsonPropertyName("slTriggerPriceType")]
        public StopPriceType? SlTriggerPriceType { get; set; }
        /// <summary>
        /// Quantity filled
        /// </summary>
        [JsonPropertyName("filledSize")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// Average price
        /// </summary>
        [JsonPropertyName("avgPrice")]
        public decimal AveragePrice { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonPropertyName("feeCurrency")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Tax
        /// </summary>
        [JsonPropertyName("tax")]
        public decimal Tax { get; set; }
        /// <summary>
        /// Updated time
        /// </summary>
        [JsonPropertyName("updatedTime")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// Trigger order id
        /// </summary>
        [JsonPropertyName("triggerOrderId")]
        public string? TriggerOrderId { get; set; }
        /// <summary>
        /// Cancel reason
        /// </summary>
        [JsonPropertyName("cancelReason"), JsonConverter(typeof(NumberStringConverter))]
        public string? CancelReason { get; set; }
        /// <summary>
        /// Cancel quantity
        /// </summary>
        [JsonPropertyName("cancelSize")]
        public decimal CancelQuantity { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("clientOid")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Quantity unit
        /// </summary>
        [JsonPropertyName("sizeUnit")]
        public QuantityUnit QuantityUnit { get; set; }
        /// <summary>
        /// Trade type
        /// </summary>
        [JsonPropertyName("tradeType")]
        public UnifiedAccountType? AccountType { get; set; }
        /// <summary>
        /// Last order trade id
        /// </summary>
        [JsonPropertyName("tradeId")]
        public string? TradeId { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonPropertyName("status")]
        public UnifiedOrderStatus Status { get; set; }
    }


}
