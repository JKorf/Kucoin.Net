using System;
using Kucoin.Net.Enums;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Order info
    /// </summary>
    [SerializationModel]
    public record KucoinOrder: KucoinOrderBase
    {        
        /// <summary>
        /// ["<c>opType</c>"] The operation type
        /// </summary>
        [JsonPropertyName("opType")]
        public OrderOperationType? OperationType { get; set; }        
        /// <summary>
        /// ["<c>funds</c>"] The funds of the order
        /// </summary>
        [JsonPropertyName("funds")]
        public decimal? QuoteQuantity { get; set; }
        /// <summary>
        /// ["<c>dealFunds</c>"] The funds of the deal
        /// </summary>
        [JsonPropertyName("dealFunds")]
        public decimal? QuoteQuantityFilled { get; set; }
        /// <summary>
        /// ["<c>dealSize</c>"] The quantity of the deal
        /// </summary>
        [JsonPropertyName("dealSize")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] The fee of the order
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>feeCurrency</c>"] The asset of the fee
        /// </summary>
        [JsonPropertyName("feeCurrency")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>stop</c>"] The stop condition
        /// </summary>
        [JsonPropertyName("stop")]
        public StopCondition? Stop { get; set; }
        /// <summary>
        /// ["<c>cancelAfter</c>"] Time after which the order is canceled
        /// </summary>
        [JsonPropertyName("cancelAfter")]
        public int? CancelAfter { get; set; }
        /// <summary>
        /// ["<c>channel</c>"] The source of the order
        /// </summary>
        [JsonPropertyName("channel")]
        public string Channel { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>tags</c>"] Tags for the order
        /// </summary>
        [JsonPropertyName("tags")]
        public string Tags { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>tradeType</c>"] Trade type
        /// </summary>
        [JsonPropertyName("tradeType")]
        public TradeType TradeType { get; set; }
    }

    /// <summary>
    /// Stop order info
    /// </summary>
    [SerializationModel]
    public record KucoinStopOrder: KucoinOrder
    {
        /// <summary>
        /// ["<c>userId</c>"] User id
        /// </summary>
        [JsonPropertyName("userId")]
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public StopOrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>orderTime</c>"] Time after which the order is canceled
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("orderTime")]
        public DateTime OrderTime { get; set; }
        /// <summary>
        /// ["<c>domainId</c>"] Domain id
        /// </summary>
        [JsonPropertyName("domainId")]
        public string DomainId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>tradeSource</c>"] Trade source
        /// </summary>
        [JsonPropertyName("tradeSource")]
        public string TradeSource { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>takerFeeRate</c>"] Taker fee rate
        /// </summary>
        [JsonPropertyName("takerFeeRate")]
        public decimal TakerFeeRate { get; set; }
        /// <summary>
        /// ["<c>makerFeeRate</c>"] Taker fee rate
        /// </summary>
        [JsonPropertyName("makerFeeRate")]
        public decimal MakerFeeRate { get; set; }
        /// <summary>
        /// ["<c>stopTriggerTime</c>"] Time stop order was triggered
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("stopTriggerTime")]
        public DateTime? StopTriggerTime { get; set; }
    }
}
