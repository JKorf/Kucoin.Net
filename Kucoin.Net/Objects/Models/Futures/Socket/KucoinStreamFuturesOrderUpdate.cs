using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The type of the update
        /// </summary>
        [JsonPropertyName("type")]
        public MatchUpdateType UpdateType { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonPropertyName("status")]
        public ExtendedOrderStatus Status { get; set; }
        /// <summary>
        /// Match quantity (for match update types)
        /// </summary>
        [JsonPropertyName("matchSize")]
        public decimal? MatchQuantity { get; set; }
        /// <summary>
        /// Match price (for match update types)
        /// </summary>
        [JsonPropertyName("matchPrice")]
        public decimal? MatchPrice { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("orderType")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Remaining quantity
        /// </summary>
        [JsonPropertyName("remainSize")]
        public decimal QuantityRemaining { get; set; }
        /// <summary>
        /// Filled quantity
        /// </summary>
        [JsonPropertyName("filledSize")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// Canceled quantity
        /// </summary>
        [JsonPropertyName("canceledSize")]
        public decimal QuantityCanceled { get; set; }
        /// <summary>
        /// Trade id
        /// </summary>
        [JsonPropertyName("tradeId")]
        public string? TradeId { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("clientOid")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Order timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("orderTime")]
        public DateTime OrderTime { get; set; }
        /// <summary>
        /// Quantity before the update
        /// </summary>
        [JsonPropertyName("oldSize")]
        public decimal? OldQuantity { get; set; }
        /// <summary>
        /// Trade direction
        /// </summary>
        [JsonPropertyName("liquidity")]
        public LiquidityType? Liquidity { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Trade direction
        /// </summary>

        [JsonPropertyName("feeType")]
        public FeeType? FeeType { get; set; }

        /// <summary>
        /// Margin mode
        /// </summary>

        [JsonPropertyName("marginMode")]
        public FuturesMarginMode? MarginMode { get; set; }

        /// <summary>
        /// Trade type
        /// </summary>

        [JsonPropertyName("tradeType")]
        public FuturesTradeType? TradeType { get; set; }
    }
}
