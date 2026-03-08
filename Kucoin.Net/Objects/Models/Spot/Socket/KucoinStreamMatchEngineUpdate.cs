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
        /// ["<c>symbol</c>"] The symbol of the update
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>sequence</c>"] Update sequence
        /// </summary>
        [JsonPropertyName("sequence")]
        public long Sequence { get; set; }
        /// <summary>
        /// ["<c>ts</c>"] The timestamp of the event
        /// </summary>
        [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>orderId</c>"] Id of the order
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>clientOId</c>"] Id of the order
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
        /// ["<c>side</c>"] Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>size</c>"] Quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>orderTime</c>"] Order time
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
        /// ["<c>reason</c>"] Reason of the done update
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
        /// ["<c>size</c>"] New quantity of the order
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
        /// ["<c>price</c>"] Price of the match
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Match side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>size</c>"] Match quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>remainingSize</c>"] Remaing quantity on the order
        /// </summary>
        [JsonPropertyName("remainingSize")]
        public decimal QuantityRemaining { get; set; }
        /// <summary>
        /// ["<c>takerOrderId</c>"] Order id of taker
        /// </summary>
        [JsonPropertyName("takerOrderId")]
        public string TakerOrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>makerOrderId</c>"] Order id of maker
        /// </summary>
        [JsonPropertyName("makerOrderId")]
        public string MakerOrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>tradeId</c>"] Id of the trade
        /// </summary>
        [JsonPropertyName("tradeId")]
        public string TradeId { get; set; } = string.Empty;
    }
}
