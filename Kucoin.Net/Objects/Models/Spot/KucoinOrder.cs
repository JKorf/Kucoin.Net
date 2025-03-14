using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// The operation type
        /// </summary>
        [JsonPropertyName("opType")]
        public OrderOperationType? OperationType { get; set; }        
        /// <summary>
        /// The funds of the order
        /// </summary>
        [JsonPropertyName("funds")]
        public decimal? QuoteQuantity { get; set; }
        /// <summary>
        /// The funds of the deal
        /// </summary>
        [JsonPropertyName("dealFunds")]
        public decimal? QuoteQuantityFilled { get; set; }
        /// <summary>
        /// The quantity of the deal
        /// </summary>
        [JsonPropertyName("dealSize")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// The fee of the order
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// The asset of the fee
        /// </summary>
        [JsonPropertyName("feeCurrency")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// The stop condition
        /// </summary>
        [JsonPropertyName("stop")]
        public StopCondition? Stop { get; set; }
        /// <summary>
        /// Time after which the order is canceled
        /// </summary>
        [JsonPropertyName("cancelAfter")]
        public int? CancelAfter { get; set; }
        /// <summary>
        /// The source of the order
        /// </summary>
        [JsonPropertyName("channel")]
        public string Channel { get; set; } = string.Empty;
        /// <summary>
        /// Tags for the order
        /// </summary>
        [JsonPropertyName("tags")]
        public string Tags { get; set; } = string.Empty;
        /// <summary>
        /// Trade type
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
        /// User id
        /// </summary>
        [JsonPropertyName("userId")]
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public StopOrderStatus Status { get; set; }
        /// <summary>
        /// Time after which the order is canceled
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("orderTime")]
        public DateTime OrderTime { get; set; }
        /// <summary>
        /// Domain id
        /// </summary>
        [JsonPropertyName("domainId")]
        public string DomainId { get; set; } = string.Empty;
        /// <summary>
        /// Trade source
        /// </summary>
        [JsonPropertyName("tradeSource")]
        public string TradeSource { get; set; } = string.Empty;
        /// <summary>
        /// Taker fee rate
        /// </summary>
        [JsonPropertyName("takerFeeRate")]
        public decimal TakerFeeRate { get; set; }
        /// <summary>
        /// Taker fee rate
        /// </summary>
        [JsonPropertyName("makerFeeRate")]
        public decimal MakerFeeRate { get; set; }
        /// <summary>
        /// Time stop order was triggered
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("stopTriggerTime")]
        public DateTime? StopTriggerTime { get; set; }
    }
}
