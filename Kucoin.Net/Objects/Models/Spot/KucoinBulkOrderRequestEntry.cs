using Kucoin.Net.Enums;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// The order model to be sent via bulk order endpoint
    /// </summary>
    [SerializationModel]
    public record KucoinBulkOrderRequestEntry
    {
        /// <summary>
        /// ["<c>clientOid</c>"] The client order id
        /// </summary>
        [JsonPropertyName("clientOid")]
        public string ClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>side</c>"] The side of the order
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>price</c>"] The price of the order
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>size</c>"] The quantity of the order
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>type</c>"] The type of the order
        /// </summary>
        [JsonPropertyName("type")]
        public NewOrderType Type { get; set; }

        /// <summary>
        /// ["<c>remark</c>"] Remark for the order
        /// </summary>
        [JsonPropertyName("remark"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Remark { get; set; }
        /// <summary>
        /// ["<c>stop</c>"] The stop condition
        /// </summary>
        [JsonPropertyName("stop"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public StopCondition? Stop { get; set; }
        /// <summary>
        /// ["<c>stopPrice</c>"] The stop price
        /// </summary>
        [JsonPropertyName("stopPrice"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? StopPrice { get; set; }
        /// <summary>
        /// ["<c>stp</c>"] The self trade prevention type
        /// </summary>
        [JsonPropertyName("stp"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public SelfTradePrevention? SelfTradePrevention { get; set; }
        /// <summary>
        /// ["<c>tradeType</c>"] Trade type
        /// </summary>
        [JsonPropertyName("tradeType"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TradeType? TradeType { get; set; }
        /// <summary>
        /// ["<c>timeInForce</c>"] The time in force of the order
        /// </summary>
        [JsonPropertyName("timeInForce"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TimeInForce? TimeInForce { get; set; }
        /// <summary>
        /// ["<c>cancelAfter</c>"] Time after which the order is canceled
        /// </summary>
        [JsonPropertyName("cancelAfter"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int? CancelAfter { get; set; }
        /// <summary>
        /// ["<c>postOnly</c>"] Whether the order is post only
        /// </summary>
        [JsonPropertyName("postOnly"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool? PostOnly { get; set; }
        /// <summary>
        /// ["<c>hidden</c>"] Whether the order is hidden
        /// </summary>
        [JsonPropertyName("hidden"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool? Hidden { get; set; }
        /// <summary>
        /// ["<c>iceberg</c>"] Whether it is an iceberg order
        /// </summary>
        [JsonPropertyName("iceberg"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool? Iceberg { get; set; }
        /// <summary>
        /// ["<c>visibleSize</c>"] The max visible size of the iceberg
        /// </summary>
        [JsonPropertyName("visibleSize"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? VisibleIcebergSize { get; set; }
    }
}
