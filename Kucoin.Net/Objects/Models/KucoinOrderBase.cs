using System;
using Kucoin.Net.Enums;


namespace Kucoin.Net.Objects.Models
{
    /// <summary>
    /// Order info
    /// </summary>
    [SerializationModel]
    public record KucoinOrderBase
    {
        /// <summary>
        /// ["<c>id</c>"] The id of the order
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbol</c>"] the symbol of the order
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>type</c>"] The type of the order
        /// </summary>
        [JsonPropertyName("type")]
        public OrderType Type { get; set; }
        /// <summary>
        /// ["<c>side</c>"] The side of the order
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>price</c>"] The price of the order
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>size</c>"] The quantity of the order
        /// </summary>
        [JsonPropertyName("size")]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// ["<c>stopTriggered</c>"] Whether the stop condition is triggered
        /// </summary>
        [JsonPropertyName("stopTriggered")]
        public bool? StopTriggered { get; set; }
        /// <summary>
        /// ["<c>stopPrice</c>"] The stop price
        /// </summary>
        [JsonPropertyName("stopPrice")]
        public decimal? StopPrice { get; set; }

        /// <summary>
        /// ["<c>timeInForce</c>"] The time in force of the order
        /// </summary>
        [JsonPropertyName("timeInForce")]
        public TimeInForce? TimeInForce { get; set; }
        /// <summary>
        /// ["<c>postOnly</c>"] Whether the order is post only
        /// </summary>
        [JsonPropertyName("postOnly")]
        public bool PostOnly { get; set; }
        /// <summary>
        /// ["<c>hidden</c>"] Whether the order is hidden
        /// </summary>
        [JsonPropertyName("hidden")]
        public bool Hidden { get; set; }
        /// <summary>
        /// ["<c>iceberg</c>"] Whether it is an iceberg order
        /// </summary>
        [JsonPropertyName("iceberg")]
        public bool Iceberg { get; set; }
        /// <summary>
        /// ["<c>visibleSize</c>"] The max visible size of the iceberg
        /// </summary>
        [JsonPropertyName("visibleSize")]
        public decimal? VisibleIcebergSize { get; set; }
        /// <summary>
        /// ["<c>clientOid</c>"] The client order id
        /// </summary>
        [JsonPropertyName("clientOid")]
        public string ClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>remark</c>"] Remark for the order
        /// </summary>
        [JsonPropertyName("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>isActive</c>"] Whether the order is active
        /// </summary>
        [JsonPropertyName("isActive")]
        public virtual bool? IsActive { get; set; }
        /// <summary>
        /// ["<c>cancelExist</c>"] If there is a cancel request for this order
        /// </summary>
        [JsonPropertyName("cancelExist")]
        public bool CancelExist { get; set; }
        /// <summary>
        /// ["<c>createdAt</c>"] The time the order was created
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("createdAt")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>stp</c>"] The self trade prevention type
        /// </summary>
        [JsonPropertyName("stp")]
        public SelfTradePrevention? SelfTradePrevention { get; set; }
    }
}
