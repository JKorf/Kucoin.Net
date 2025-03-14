using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Value of the order
        /// </summary>
        [JsonPropertyName("value")]
        public decimal QuoteQantity { get; set; }
        /// <summary>
        /// Filled value
        /// </summary>
        [JsonPropertyName("dealValue")]
        public decimal? ExecutedValue { get; set; }
        /// <summary>
        /// Filled quantity
        /// </summary>
        [JsonPropertyName("dealSize")]
        public decimal? ExecutedQuantity { get; set; }
        /// <summary>
        /// Filled value
        /// </summary>
        [JsonPropertyName("filledValue")]
        public decimal QuoteQuantityFilled { get; set; }
        /// <summary>
        /// Filled quantity
        /// </summary>
        [JsonPropertyName("filledSize")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// The type of the stop order
        /// </summary>
        [JsonPropertyName("stop")]
        public StopCondition? StopOrderType { get; set; }
        /// <summary>
        /// Stop price type
        /// </summary>
        [JsonPropertyName("stopPriceType")]
        public StopPriceType? StopPriceType { get; set; }
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// Force hold
        /// </summary>
        [JsonPropertyName("forceHold")]
        public bool ForceHold { get; set; }
        /// <summary>
        /// Close order
        /// </summary>
        [JsonPropertyName("closeOrder")]
        public bool CloseOrder { get; set; }
        /// <summary>
        /// Reduce only
        /// </summary>
        [JsonPropertyName("reduceOnly")]
        public bool ReduceOnly { get; set; }
        /// <summary>
        /// Settle asset
        /// </summary>
        [JsonPropertyName("settleCurrency")]
        public string SettleAsset { get; set; } = string.Empty;
        /// <summary>
        /// The time the order was last updated
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("updatedAt")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// Order create time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("orderTime")]
        public DateTime? OrderTime { get; set; }
        /// <summary>
        /// End time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("endAt")]
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// Status
        /// </summary>

        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// Tags
        /// </summary>
        [JsonPropertyName("tags")]
        public string? Tags { get; set; }
        /// <summary>
        /// Margin mode
        /// </summary>

        [JsonPropertyName("marginMode")]
        public FuturesMarginMode? MarginMode { get; set; }
        /// <summary>
        /// Average fill price
        /// </summary>
        [JsonPropertyName("avgDealPrice")]
        public decimal? AveragePrice { get; set; }
    }
}
