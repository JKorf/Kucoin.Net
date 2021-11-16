using System;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Converters;
using Kucoin.Net.Enums;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models
{
    /// <summary>
    /// Stop order update
    /// </summary>
    public abstract class KucoinStreamStopOrderUpdateBase
    {
        /// <summary>
        /// Order side
        /// </summary>
        public abstract OrderSide OrderSide { get; set; }

        /// <summary>
        /// Creation time
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        [JsonProperty("createdAt")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonProperty("orderId")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Order price
        /// </summary>
        public decimal OrderPrice { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonConverter(typeof(OrderTypeConverter))]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Stop
        /// </summary>
        [JsonConverter(typeof(StopConditionConverter))]
        public StopCondition Stop { get; set; }
        /// <summary>
        /// Stop price
        /// </summary>
        public decimal StopPrice { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Trade type
        /// </summary>
        [JsonConverter(typeof(TradeTypeConverter))]
        public TradeType TradeType { get; set; }
        /// <summary>
        /// Trigger was success
        /// </summary>
        public bool TriggerSuccess { get; set; }
        /// <summary>
        /// Update timestamp
        /// </summary>
        [JsonProperty("ts"), JsonConverter(typeof(TimestampNanoSecondsConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Update type
        /// </summary>
        public string Type { get; set; } = string.Empty;
    }
}
