using CryptoExchange.Net.Converters;
using Kucoin.Net.Converts;
using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects.Sockets
{
    /// <summary>
    /// Base class for a stream update
    /// </summary>
    public class KucoinStreamOrderBaseUpdate
    {
        /// <summary>
        /// The symbol of the update
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The timestamp of the event
        /// </summary>
        [JsonProperty("ts"), JsonConverter(typeof(TimestampNanoSecondsConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// The type of the update
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(MatchUpdateTypeConverter))]
        public KucoinMatchUpdateType UpdateType { get; set; }
        /// <summary>
        /// The side of the order
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public KucoinOrderSide Side { get; set; }
        /// <summary>
        /// The order id
        /// </summary>
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonConverter(typeof(OrderTypeConverter))]
        public KucoinOrderType OrderType { get; set; }
        /// <summary>
        /// The price of the order
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// The client order id
        /// </summary>
        [JsonProperty("clientOid")]
        public string ClientOrderid { get; set; } = string.Empty;
        /// <summary>
        /// The quantity of the order
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Quantity filled
        /// </summary>
        [JsonProperty("filledSize")]
        public decimal FilledQuantity { get; set; }
        /// <summary>
        /// Quantity remaining
        /// </summary>
        [JsonProperty("remainSize")]
        public decimal RemainingQuantity { get; set; }
    }
    
    /// <summary>
    /// Stream order update (match)
    /// </summary>
    public class KucoinStreamOrderMatchUpdate : KucoinStreamOrderBaseUpdate
    {
        /// <summary>
        /// The trade id
        /// </summary>
        public string TradeId { get; set; } = string.Empty;
        /// <summary>
        /// The price of the match
        /// </summary>
        public decimal MatchPrice { get; set; }
        /// <summary>
        /// The quantity of the match
        /// </summary>
        [JsonProperty("matchSize")]
        public decimal MatchQuantity { get; set; }
        /// <summary>
        /// The liquidity
        /// </summary>
        [JsonConverter(typeof(LiquidityTypeConverter))]
        public KucoinLiquidityType Liquidity { get; set; }
    }
}
