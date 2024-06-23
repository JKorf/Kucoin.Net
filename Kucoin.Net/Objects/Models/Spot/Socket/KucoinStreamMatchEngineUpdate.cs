using System;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Converters;
using Kucoin.Net.Enums;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Base record for a stream update
    /// </summary>
    public record KucoinStreamMatchEngineUpdate
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
        [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
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
    public record KucoinStreamMatchEngineOpenUpdate : KucoinStreamMatchEngineUpdate
    {
        /// <summary>
        /// Order side
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public OrderSide Side { get; set; }
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
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime OrderTime { get; set; }
    }

    /// <summary>
    /// Stream order done update
    /// </summary>
    public record KucoinStreamMatchEngineDoneUpdate : KucoinStreamMatchEngineUpdate
    {
        /// <summary>
        /// Reason of the done update
        /// </summary>
        [JsonConverter(typeof(MatchUpdateReasonConverter))]
        public MatchUpdateReason Reason { get; set; }
    }

    /// <summary>
    /// Stream order change update
    /// </summary>
    public record KucoinStreamMatchEngineChangeUpdate : KucoinStreamMatchEngineUpdate
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
    public record KucoinStreamMatchEngineMatchUpdate : KucoinStreamMatchEngineUpdate
    {
        /// <summary>
        /// Price of the match
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Match side
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Match quantity
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Remaing quantity on the order
        /// </summary>
        [JsonProperty("remainingSize")]
        public decimal QuantityRemaining { get; set; }
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
