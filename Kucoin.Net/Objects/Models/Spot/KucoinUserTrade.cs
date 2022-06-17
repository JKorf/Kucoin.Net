using System;
using Kucoin.Net.Converters;
using Kucoin.Net.Enums;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// User trade info
    /// </summary>
    public class KucoinUserTrade: KucoinTradeBase
    {        
        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonConverter(typeof(OrderTypeConverter))]
        public OrderType Type { get; set; }        
        /// <summary>
        /// The funds of the fill
        /// </summary>
        [JsonProperty("funds")]
        public decimal QuoteQuantity { get; set; }        
        /// <summary>
        /// The stop condition of the fill
        /// </summary>
        [JsonConverter(typeof(StopConditionConverter))]
        public StopCondition Stop { get; set; }
        /// <summary>
        /// The id of the counter order
        /// </summary>
        public string CounterOrderId { get; set; } = string.Empty;
        /// <summary>
        /// Was forced to become taker
        /// </summary>
        public bool ForceTaker { get; set; }
        /// <summary>
        /// Trade type
        /// </summary>
        [JsonConverter(typeof(TradeTypeConverter))]
        public TradeType TradeType { get; set; } = TradeType.SpotTrade;
    }
}
