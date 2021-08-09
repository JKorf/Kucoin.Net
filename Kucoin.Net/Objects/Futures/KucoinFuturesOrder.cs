using CryptoExchange.Net.Converters;
using Kucoin.Net.Enums;
using Newtonsoft.Json;
using System;
using Kucoin.Net.Converters;

namespace Kucoin.Net.Objects.Futures
{
    /// <summary>
    /// Futures order info
    /// </summary>
    public class KucoinFuturesOrder: KucoinOrderBase
    {
        /// <summary>
        /// Value of the order
        /// </summary>
        public decimal Value { get; set; }
        /// <summary>
        /// Filled value
        /// </summary>
        public decimal FilledValue { get; set; }
        /// <summary>
        /// Filled quantity
        /// </summary>
        public decimal FilledQuantity { get; set; }
        /// <summary>
        /// The type of the stop order
        /// </summary>
        [JsonConverter(typeof(OrderTypeConverter))]
        [JsonProperty("stop")]
        public KucoinOrderType StopOrderType { get; set; }
        /// <summary>
        /// Stop price type
        /// </summary>
        [JsonConverter(typeof(StopPriceTypeConverter))]
        public StopPriceType StopPriceType { get; set; }
        /// <summary>
        /// Leverage
        /// </summary>
        public int Leverage { get; set; }
        /// <summary>
        /// Force hold
        /// </summary>
        public bool ForceHold { get; set; }
        /// <summary>
        /// Close order
        /// </summary>
        public bool CloseOrder { get; set; }
        /// <summary>
        /// Reduce only
        /// </summary>
        public bool ReduceOnly { get; set; }
        /// <summary>
        /// Settle currency
        /// </summary>
        public string SettleCurrency { get; set; } = string.Empty;
        /// <summary>
        /// The time the order was last updated
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime UpdatedAt { get; set; }
    }
}
