using System;
using Kucoin.Net.Enums;


namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Futures order info
    /// </summary>
    [SerializationModel]
    public record KucoinFuturesOrder: KucoinOrderBase
    {
        /// <summary>
        /// ["<c>value</c>"] Value of the order
        /// </summary>
        [JsonPropertyName("value")]
        public decimal QuoteQantity { get; set; }
        /// <summary>
        /// ["<c>dealValue</c>"] Filled value
        /// </summary>
        [JsonPropertyName("dealValue")]
        public decimal? ExecutedValue { get; set; }
        /// <summary>
        /// ["<c>dealSize</c>"] Filled quantity
        /// </summary>
        [JsonPropertyName("dealSize")]
        public decimal? ExecutedQuantity { get; set; }
        /// <summary>
        /// ["<c>filledValue</c>"] Filled value
        /// </summary>
        [JsonPropertyName("filledValue")]
        public decimal QuoteQuantityFilled { get; set; }
        /// <summary>
        /// ["<c>filledSize</c>"] Filled quantity
        /// </summary>
        [JsonPropertyName("filledSize")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>stop</c>"] The type of the stop order
        /// </summary>
        [JsonPropertyName("stop")]
        public StopCondition? StopOrderType { get; set; }
        /// <summary>
        /// ["<c>stopPriceType</c>"] Stop price type
        /// </summary>
        [JsonPropertyName("stopPriceType")]
        public StopPriceType? StopPriceType { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// ["<c>forceHold</c>"] Force hold
        /// </summary>
        [JsonPropertyName("forceHold")]
        public bool ForceHold { get; set; }
        /// <summary>
        /// ["<c>closeOrder</c>"] Close order
        /// </summary>
        [JsonPropertyName("closeOrder")]
        public bool CloseOrder { get; set; }
        /// <summary>
        /// ["<c>reduceOnly</c>"] Reduce only
        /// </summary>
        [JsonPropertyName("reduceOnly")]
        public bool ReduceOnly { get; set; }
        /// <summary>
        /// ["<c>settleCurrency</c>"] Settle asset
        /// </summary>
        [JsonPropertyName("settleCurrency")]
        public string SettleAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>updatedAt</c>"] The time the order was last updated
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("updatedAt")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// ["<c>orderTime</c>"] Order create time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("orderTime")]
        public DateTime? OrderTime { get; set; }
        /// <summary>
        /// ["<c>endAt</c>"] End time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("endAt")]
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>

        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>tags</c>"] Tags
        /// </summary>
        [JsonPropertyName("tags")]
        public string? Tags { get; set; }
        /// <summary>
        /// ["<c>marginMode</c>"] Margin mode
        /// </summary>

        [JsonPropertyName("marginMode")]
        public FuturesMarginMode? MarginMode { get; set; }
        /// <summary>
        /// ["<c>avgDealPrice</c>"] Average fill price
        /// </summary>
        [JsonPropertyName("avgDealPrice")]
        public decimal? AveragePrice { get; set; }
        /// <summary>
        /// ["<c>positionSide</c>"] Position side
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide? PositionSide { get; set; }
    }
}
