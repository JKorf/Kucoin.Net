using System;
using Kucoin.Net.Enums;


namespace Kucoin.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Base record for a stream update
    /// </summary>
    [SerializationModel]
    public record KucoinStreamOrderBaseUpdate
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol of the update
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>ts</c>"] The timestamp of the event
        /// </summary>
        [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>type</c>"] The type of the update
        /// </summary>
        [JsonPropertyName("type")]
        public MatchUpdateType? UpdateType { get; set; }
        /// <summary>
        /// ["<c>side</c>"] The side of the order
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>orderId</c>"] The order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>orderType</c>"] The type of the order
        /// </summary>
        [JsonPropertyName("orderType")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// ["<c>price</c>"] The price of the order
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>clientOid</c>"] The client order id
        /// </summary>
        [JsonPropertyName("clientOid")]
        public string? ClientOrderid { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Order status
        /// </summary>
        [JsonPropertyName("status")]
        public ExtendedOrderStatus? Status { get; set; }
        /// <summary>
        /// ["<c>orderTime</c>"] Order time
        /// </summary>
        [JsonPropertyName("orderTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? OrderTime { get; set; }
        /// <summary>
        /// ["<c>originSize</c>"] Origin quantity
        /// </summary>
        [JsonPropertyName("originSize")]
        public decimal OriginalQuantity { get; set; }
        /// <summary>
        /// ["<c>originFunds</c>"] Origin value
        /// </summary>
        [JsonPropertyName("originFunds")]
        public decimal OriginalValue { get; set; }
    }

    /// <summary>
    /// New order update
    /// </summary>
    [SerializationModel]
    public record KucoinStreamOrderNewUpdate : KucoinStreamOrderBaseUpdate
    {
    }
    
    /// <summary>
    /// Order update
    /// </summary>
    [SerializationModel]
    public record KucoinStreamOrderUpdate : KucoinStreamOrderBaseUpdate
    {
        /// <summary>
        /// ["<c>size</c>"] The quantity of the order
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>oldSize</c>"] Quantity before the update
        /// </summary>
        [JsonPropertyName("oldSize")]
        public decimal? OldQuantity { get; set; }
        /// <summary>
        /// ["<c>filledSize</c>"] Quantity filled
        /// </summary>
        [JsonPropertyName("filledSize")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>remainSize</c>"] Quantity remaining
        /// </summary>
        [JsonPropertyName("remainSize")]
        public decimal QuantityRemaining { get; set; }
        /// <summary>
        /// ["<c>remainFunds</c>"] Quantity remaining
        /// </summary>
        [JsonPropertyName("remainFunds")]
        public decimal? QuoteQuantityRemaining { get; set; }
        /// <summary>
        /// ["<c>canceledSize</c>"] Quantity canceled
        /// </summary>
        [JsonPropertyName("canceledSize")]
        public decimal QuantityCanceled { get; set; }
        /// <summary>
        /// ["<c>canceledFunds</c>"] Value canceled
        /// </summary>
        [JsonPropertyName("canceledFunds")]
        public decimal ValueCanceled { get; set; }

    }

    /// <summary>
    /// Stream order update (match)
    /// </summary>
    [SerializationModel]
    public record KucoinStreamOrderMatchUpdate : KucoinStreamOrderUpdate
    {
        /// <summary>
        /// ["<c>tradeId</c>"] The trade id
        /// </summary>
        [JsonPropertyName("tradeId")]
        public string TradeId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>matchPrice</c>"] The price of the match
        /// </summary>
        [JsonPropertyName("matchPrice")]
        public decimal MatchPrice { get; set; }
        /// <summary>
        /// ["<c>matchSize</c>"] The quantity of the match
        /// </summary>
        [JsonPropertyName("matchSize")]
        public decimal MatchQuantity { get; set; }
        /// <summary>
        /// ["<c>liquidity</c>"] The liquidity
        /// </summary>
        [JsonPropertyName("liquidity")]
        public LiquidityType Liquidity { get; set; }
        /// <summary>
        /// ["<c>feeType</c>"] Type of fee paid
        /// </summary>
        [JsonPropertyName("feeType")]
        public FeeType FeeType { get; set; }
    }
}
