using System;
using Kucoin.Net.Enums;


namespace Kucoin.Net.Objects.Models
{
    /// <summary>
    /// Stop order update
    /// </summary>
    [SerializationModel]
    public record KucoinStreamStopOrderUpdateBase
    {
        /// <summary>
        /// ["<c>side</c>"] Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide OrderSide { get; set; }

        /// <summary>
        /// ["<c>createdAt</c>"] Creation time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("createdAt")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>orderPrice</c>"] Order price
        /// </summary>
        [JsonPropertyName("orderPrice")]
        public decimal? OrderPrice { get; set; }
        /// <summary>
        /// ["<c>orderType</c>"] Order type
        /// </summary>
        [JsonPropertyName("orderType")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// ["<c>size</c>"] Quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>stop</c>"] Stop
        /// </summary>
        [JsonPropertyName("stop")]
        public StopCondition Stop { get; set; }
        /// <summary>
        /// ["<c>stopPrice</c>"] Stop price
        /// </summary>
        [JsonPropertyName("stopPrice")]
        public decimal StopPrice { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>tradeType</c>"] Trade type
        /// </summary>
        [JsonPropertyName("tradeType")]
        public TradeType TradeType { get; set; }
        /// <summary>
        /// ["<c>triggerSuccess</c>"] Trigger was success
        /// </summary>
        [JsonPropertyName("triggerSuccess")]
        public bool TriggerSuccess { get; set; }
        /// <summary>
        /// ["<c>ts</c>"] Update timestamp
        /// </summary>
        [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Update type
        /// </summary>
        [JsonPropertyName("type")]
        public StopOrderEvent Type { get; set; }

        /// <summary>
        /// ["<c>marginMode</c>"] Margin mode
        /// </summary>

        [JsonPropertyName("marginMode")]
        public FuturesMarginMode? MarginMode { get; set; }
    }
}
