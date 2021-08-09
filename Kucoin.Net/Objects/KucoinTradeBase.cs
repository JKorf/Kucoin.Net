using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using Kucoin.Net.Converters;

namespace Kucoin.Net.Objects
{
    /// <summary>
    /// Trade info
    /// </summary>
    public class KucoinTradeBase
    {
        /// <summary>
        /// The symbol the fill is for
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
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
        public string FeeCurrency { get; set; } = string.Empty;

        /// <summary>
        /// The time the fill was created
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// The id of the trade
        /// </summary>
        public string TradeId { get; set; } = string.Empty;
        /// <summary>
        /// The id of the order
        /// </summary>
        public string OrderId { get; set; } = string.Empty;

        /// <summary>
        /// Maker or taker
        /// </summary>
        [JsonConverter(typeof(LiquidityTypeConverter))]
        public KucoinLiquidityType Liquidity { get; set; }
    }
}
