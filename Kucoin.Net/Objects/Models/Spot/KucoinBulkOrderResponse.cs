using System;
using Kucoin.Net.Enums;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// The response for bulk order creation
    /// </summary>
    [SerializationModel]
    public record KucoinBulkOrderResponse
    {
        /// <summary>
        /// ["<c>data</c>"] List of orders
        /// </summary>
        [JsonPropertyName("data")]
        public KucoinBulkOrderResponseEntry[] Orders { get; set; } = default!;
    }

    /// <summary>
    /// The order model in bulk order creation response
    /// </summary>
    [SerializationModel]
    public record KucoinBulkOrderResponseEntry
    {
        /// <summary>
        /// ["<c>id</c>"] The id of the order
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>clientOid</c>"] The client order id
        /// </summary>
        [JsonPropertyName("clientOid")]
        public string ClientOrderId { get; set; } = string.Empty;
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
        /// ["<c>funds</c>"] The funds of the order
        /// </summary>
        [JsonPropertyName("funds")]
        public decimal? QuoteQuantity { get; set; }
        /// <summary>
        /// ["<c>stp</c>"] The self trade prevention type
        /// </summary>
        [JsonPropertyName("stp")]
        public SelfTradePrevention? SelfTradePrevention { get; set; }
        /// <summary>
        /// ["<c>stop</c>"] The stop condition
        /// </summary>
        [JsonPropertyName("stop")]
        public StopCondition? Stop { get; set; }
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
        /// ["<c>cancelAfter</c>"] Time after which the order is canceled
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("cancelAfter")]
        public DateTime CancelAfter { get; set; }
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
        /// ["<c>channel</c>"] The source of the order
        /// </summary>
        [JsonPropertyName("channel")]
        public string Channel { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public BulkOrderCreationStatus Status { get; set; }
        /// <summary>
        /// ["<c>failMsg</c>"] The cause of failure
        /// </summary>
        [JsonPropertyName("failMsg")]
        public string? FailMsg { get; set; }
    }
}
