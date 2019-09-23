using CryptoExchange.Net.Converters;
using Kucoin.Net.Converts;
using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects
{
    /// <summary>
    /// Fill info
    /// </summary>
    public class KucoinFill
    {
        /// <summary>
        /// The symbol the fill is for
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonConverter(typeof(OrderTypeConverter))]
        public KucoinOrderType Type { get; set; }
        /// <summary>
        /// The side of the fill
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public KucoinOrderSide Side { get; set; }
        /// <summary>
        /// The price of the fill
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// The quantity of the fill
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The funds of the fill
        /// </summary>
        public decimal Funds { get; set; }
        /// <summary>
        /// The amount of fee of the fill
        /// </summary>
        public decimal Fee { get; set; }
        /// <summary>
        /// The rate of the fee
        /// </summary>
        public decimal FeeRate { get; set; }
        /// <summary>
        /// The currency of the fee
        /// </summary>
        public string FeeCurrency { get; set; }
        /// <summary>
        /// The stop condition of the fill
        /// </summary>
        [JsonConverter(typeof(StopConditionConverter))]
        public KucoinStopCondition Stop { get; set; }
        /// <summary>
        /// The time the fill was created
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// The id of the trade
        /// </summary>
        public string TradeId { get; set; }
        /// <summary>
        /// The id of the order
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// The id of the counter order
        /// </summary>
        public string CounterOrderId { get; set; }
        /// <summary>
        /// Maker or taker
        /// </summary>
        [JsonConverter(typeof(LiquidityTypeConverter))]
        public KucoinLiquidityType Liquidity { get; set; }
        /// <summary>
        /// Was forced to become taker
        /// </summary>
        public bool ForceTaker { get; set; }
    }
}
