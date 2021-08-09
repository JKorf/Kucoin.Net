using System;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Converters;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Spot.Socket
{
    /// <summary>
    /// Base class for a stream update
    /// </summary>
    public class KucoinStreamMatchEngineUpdate
    {
        /// <summary>
        /// The symbol of the update
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Update sequence
        /// </summary>
        public long Sequence { get; set; }
        /// <summary>
        /// The timestamp of the event
        /// </summary>
        [JsonProperty("ts"), JsonConverter(typeof(TimestampNanoSecondsConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Id of the order
        /// </summary>
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Id of the order
        /// </summary>
        [JsonProperty("clientOId")]
        public string ClientOrderId { get; set; } = string.Empty;
    }
    
    /// <summary>
    /// Stream order open update
    /// </summary>
    public class KucoinStreamMatchEngineOpenUpdate : KucoinStreamMatchEngineUpdate
    {
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
        /// Order time
        /// </summary>
        [JsonConverter(typeof(TimestampNanoSecondsConverter))]
        public DateTime OrderTime { get; set; }
    }

    /// <summary>
    /// Stream order done update
    /// </summary>
    public class KucoinStreamMatchEngineDoneUpdate : KucoinStreamMatchEngineUpdate
    {
        /// <summary>
        /// Reason of the done update
        /// </summary>
        [JsonConverter(typeof(MatchUpdateReasonConverter))]
        public KucoinMatchUpdateReason Reason { get; set; }
    }

    /// <summary>
    /// Stream order change update
    /// </summary>
    public class KucoinStreamMatchEngineChangeUpdate : KucoinStreamMatchEngineUpdate
    {
        /// <summary>
        /// New quantity of the order
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
    }

    /// <summary>
    /// Stream order match update
    /// </summary>
    public class KucoinStreamMatchEngineMatchUpdate : KucoinStreamMatchEngineUpdate
    {
        /// <summary>
        /// Price of the match
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Match side
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public KucoinOrderSide Side { get; set; }
        /// <summary>
        /// Match quantity
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Remaing quantity on the order
        /// </summary>
        [JsonProperty("remainingSize")]
        public decimal RemainingQuantity { get; set; }
        /// <summary>
        /// Order id of taker
        /// </summary>
        public string TakerOrderId { get; set; } = string.Empty;
        /// <summary>
        /// Order id of maker
        /// </summary>
        public string MakerOrderId { get; set; } = string.Empty;
        /// <summary>
        /// Id of the trade
        /// </summary>
        public string TradeId { get; set; } = string.Empty;
    }
}
