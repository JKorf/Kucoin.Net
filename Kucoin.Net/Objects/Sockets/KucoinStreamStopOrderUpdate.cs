using System;
using System.Collections.Generic;
using System.Text;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Converts;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Sockets
{
    /// <summary>
    /// Stop order update
    /// </summary>
    public class KucoinStreamStopOrderUpdate
    {
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// Order price
        /// </summary>
        public decimal OrderPrice { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonConverter(typeof(OrderTypeConverter))]
        public KucoinOrderType OrderType { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public KucoinOrderSide OrderSide { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Stop
        /// </summary>
        public string Stop { get; set; }
        /// <summary>
        /// Stop price
        /// </summary>
        public decimal StopPrice { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// Trade type
        /// </summary>
        public string TradeType { get; set; }
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
        public string Type { get; set; }
    }
}
