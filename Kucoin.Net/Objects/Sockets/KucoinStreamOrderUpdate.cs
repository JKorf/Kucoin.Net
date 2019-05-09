using CryptoExchange.Net.Converters;
using Kucoin.Net.Converts;
using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects.Sockets
{
    public class KucoinStreamOrderBaseUpdate
    {
        /// <summary>
        /// The sequence of the update
        /// </summary>
        public long Sequence { get; set; }
        /// <summary>
        /// The symbol of the update
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// The timestamp of the event
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(TimestampNanoSecondsConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// The type of the update
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(MatchUpdateTypeConverter))]
        public KucoinMatchUpdateType UpdateType { get; set; }
    }

    public class KucoinStreamOrderReceivedUpdate: KucoinStreamOrderBaseUpdate
    {
        /// <summary>
        /// The side of the order
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public KucoinOrderSide Side { get; set; }
        /// <summary>
        /// The order id
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// The price of the order
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// The client order id
        /// </summary>
        [JsonProperty("clientOid")]
        public string ClientOrderid { get; set; }
        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonConverter(typeof(OrderTypeConverter))]
        public KucoinOrderType OrderType { get; set; }
    }

    public class KucoinStreamOrderOpenUpdate : KucoinStreamOrderBaseUpdate
    {
        /// <summary>
        /// The side of the order
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public KucoinOrderSide Side { get; set; }
        /// <summary>
        /// The quantity of the order
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The id of the order
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// The price of the order
        /// </summary>
        public decimal Price { get; set; }
    }

    public class KucoinStreamOrderDoneUpdate : KucoinStreamOrderBaseUpdate
    {
        /// <summary>
        /// The reason for the done update
        /// </summary>
        [JsonConverter(typeof(MatchUpdateReasonConverter))]
        public KucoinMatchUpdateReason Reason { get; set; }
        /// <summary>
        /// The side of the order
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public KucoinOrderSide Side { get; set; }
        /// <summary>
        /// The quantity of the order
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The id of the order
        /// </summary>
        public string OrderId { get; set; }
    }

    public class KucoinStreamOrderMatchUpdate : KucoinStreamOrderBaseUpdate
    {
        /// <summary>
        /// The order id of the taker
        /// </summary>
        public string TakerOrderId { get; set; }
        /// <summary>
        /// The orer id of the maker
        /// </summary>
        public string MakerOrderId { get; set; }
        /// <summary>
        /// The trade id
        /// </summary>
        public string TradeId { get; set; }
        /// <summary>
        /// The side of the order
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public KucoinOrderSide Side { get; set; }
        /// <summary>
        /// The quantity of the order
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The price of the order
        /// </summary>
        public decimal Price { get; set; }
    }

    public class KucoinStreamOrderChangeUpdate : KucoinStreamOrderBaseUpdate
    {
        /// <summary>
        /// The id of the order
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// The side of the order
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public KucoinOrderSide Side { get; set; }
        /// <summary>
        /// The new quantity
        /// </summary>
        [JsonProperty("newSize")]
        public decimal NewQuantity { get; set; }
        /// <summary>
        /// The old quantity
        /// </summary>
        [JsonProperty("oldSize")]
        public decimal OldQuantity { get; set; }
        /// <summary>
        /// The price of the order
        /// </summary>
        public decimal Price { get; set; }
    }

    public class KucoinStreamOrderStopUpdate : KucoinStreamOrderBaseUpdate
    {
        /// <summary>
        /// The id of the order
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// The side of the order
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public KucoinOrderSide Side { get; set; }
        /// <summary>
        /// The funds of the order
        /// </summary>
        public decimal Funds { get; set; }
        /// <summary>
        /// The stop type of ther order
        /// </summary>
        [JsonConverter(typeof(StopConditionConverter))]
        public KucoinStopCondition StopType { get; set; }
    }
}
