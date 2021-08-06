using CryptoExchange.Net.Converters;
using Kucoin.Net.Converts;
using Kucoin.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kucoin.Net.Objects.Sockets
{
    /// <summary>
    /// Futures order update
    /// </summary>
    public class KucoinStreamFuturesOrderUpdate
    {
        /// <summary>
        /// Order id
        /// </summary>
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The type of the update
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(MatchUpdateTypeConverter))]
        public KucoinMatchUpdateType UpdateType { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonConverter(typeof(FuturesOrderStatusConverter))]
        public FuturesOrderStatus Status { get; set; }
        /// <summary>
        /// Match quantity (for match update types)
        /// </summary>
        [JsonProperty("matchSize")]
        public decimal MatchQuantity { get; set; }
        /// <summary>
        /// Match price (for match update types)
        /// </summary>
        public decimal MatchPrice { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonConverter(typeof(OrderTypeConverter))]
        public KucoinOrderType OrderType { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public KucoinOrderSide Side { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Remaining quantity
        /// </summary>
        [JsonProperty("remainSize")]
        public decimal RemainingQuantitiy { get; set; }
        /// <summary>
        /// Filled quantity
        /// </summary>
        [JsonProperty("filledSize")]
        public decimal FilledQuantity { get; set; }
        /// <summary>
        /// Canceled quantity
        /// </summary>
        [JsonProperty("canceledSize")]
        public decimal CanceledQuantity { get; set; }
        /// <summary>
        /// Trade id
        /// </summary>
        public string TradeId { get; set; } = string.Empty;
        /// <summary>
        /// Client order id
        /// </summary>
        public string ClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// Order timestamp
        /// </summary>
        [JsonConverter(typeof(TimestampNanoSecondsConverter))]
        public DateTime OrderTime { get; set; }
        /// <summary>
        /// Quantity before the update
        /// </summary>
        [JsonProperty("oldSize")]
        public decimal OldQuantity { get; set; }
        /// <summary>
        /// Trade direction
        /// </summary>
        public KucoinLiquidityType Liquidity { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(TimestampNanoSecondsConverter))]
        public DateTime Timestamp { get; set; }
    }
}
