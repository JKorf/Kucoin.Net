using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using Kucoin.Net.Enums;


namespace Kucoin.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Base record for a stream update
    /// </summary>
    [SerializationModel]
    public record KucoinStreamMatchEngineUpdate
    {
        /// <summary>
        /// The symbol of the update
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Update sequence
        /// </summary>
        [JsonPropertyName("sequence")]
        public long Sequence { get; set; }
        /// <summary>
        /// The timestamp of the event
        /// </summary>
        [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Id of the order
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Id of the order
        /// </summary>
        [JsonPropertyName("clientOId")]
        public string ClientOrderId { get; set; } = string.Empty;
    }
    
    /// <summary>
    /// Stream order open update
    /// </summary>
    [SerializationModel]
    public record KucoinStreamMatchEngineOpenUpdate : KucoinStreamMatchEngineUpdate
    {
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Order time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("orderTime")]
        public DateTime OrderTime { get; set; }
    }

    /// <summary>
    /// Stream order done update
    /// </summary>
    [SerializationModel]
    public record KucoinStreamMatchEngineDoneUpdate : KucoinStreamMatchEngineUpdate
    {
        /// <summary>
        /// Reason of the done update
        /// </summary>
        [JsonPropertyName("reason")]
        public MatchUpdateReason Reason { get; set; }
    }

    /// <summary>
    /// Stream order change update
    /// </summary>
    [SerializationModel]
    public record KucoinStreamMatchEngineChangeUpdate : KucoinStreamMatchEngineUpdate
    {
        /// <summary>
        /// New quantity of the order
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
    }

    /// <summary>
    /// Stream order match update
    /// </summary>
    [SerializationModel]
    public record KucoinStreamMatchEngineMatchUpdate : KucoinStreamMatchEngineUpdate
    {
        /// <summary>
        /// Price of the match
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Match side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Match quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Remaing quantity on the order
        /// </summary>
        [JsonPropertyName("remainingSize")]
        public decimal QuantityRemaining { get; set; }
        /// <summary>
        /// Order id of taker
        /// </summary>
        [JsonPropertyName("takerOrderId")]
        public string TakerOrderId { get; set; } = string.Empty;
        /// <summary>
        /// Order id of maker
        /// </summary>
        [JsonPropertyName("makerOrderId")]
        public string MakerOrderId { get; set; } = string.Empty;
        /// <summary>
        /// Id of the trade
        /// </summary>
        [JsonPropertyName("tradeId")]
        public string TradeId { get; set; } = string.Empty;
    }
}
