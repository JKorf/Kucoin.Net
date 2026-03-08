using System;
using Kucoin.Net.Enums;


namespace Kucoin.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Futures order update
    /// </summary>
    [SerializationModel]
    public record KucoinStreamFuturesOrderUpdate
    {
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>type</c>"] The type of the update
        /// </summary>
        [JsonPropertyName("type")]
        public MatchUpdateType UpdateType { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Order status
        /// </summary>
        [JsonPropertyName("status")]
        public ExtendedOrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>matchSize</c>"] Match quantity (for match update types)
        /// </summary>
        [JsonPropertyName("matchSize")]
        public decimal? MatchQuantity { get; set; }
        /// <summary>
        /// ["<c>matchPrice</c>"] Match price (for match update types)
        /// </summary>
        [JsonPropertyName("matchPrice")]
        public decimal? MatchPrice { get; set; }
        /// <summary>
        /// ["<c>orderType</c>"] Order type
        /// </summary>
        [JsonPropertyName("orderType")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>size</c>"] Quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>remainSize</c>"] Remaining quantity
        /// </summary>
        [JsonPropertyName("remainSize")]
        public decimal QuantityRemaining { get; set; }
        /// <summary>
        /// ["<c>filledSize</c>"] Filled quantity
        /// </summary>
        [JsonPropertyName("filledSize")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>canceledSize</c>"] Canceled quantity
        /// </summary>
        [JsonPropertyName("canceledSize")]
        public decimal QuantityCanceled { get; set; }
        /// <summary>
        /// ["<c>tradeId</c>"] Trade id
        /// </summary>
        [JsonPropertyName("tradeId")]
        public string? TradeId { get; set; }
        /// <summary>
        /// ["<c>clientOid</c>"] Client order id
        /// </summary>
        [JsonPropertyName("clientOid")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>orderTime</c>"] Order timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("orderTime")]
        public DateTime OrderTime { get; set; }
        /// <summary>
        /// ["<c>oldSize</c>"] Quantity before the update
        /// </summary>
        [JsonPropertyName("oldSize")]
        public decimal? OldQuantity { get; set; }
        /// <summary>
        /// ["<c>liquidity</c>"] Trade direction
        /// </summary>
        [JsonPropertyName("liquidity")]
        public LiquidityType? Liquidity { get; set; }
        /// <summary>
        /// ["<c>ts</c>"] Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>feeType</c>"] Trade direction
        /// </summary>

        [JsonPropertyName("feeType")]
        public FeeType? FeeType { get; set; }

        /// <summary>
        /// ["<c>marginMode</c>"] Margin mode
        /// </summary>

        [JsonPropertyName("marginMode")]
        public FuturesMarginMode? MarginMode { get; set; }

        /// <summary>
        /// ["<c>tradeType</c>"] Trade type
        /// </summary>

        [JsonPropertyName("tradeType")]
        public FuturesTradeType? TradeType { get; set; }
    }
}
