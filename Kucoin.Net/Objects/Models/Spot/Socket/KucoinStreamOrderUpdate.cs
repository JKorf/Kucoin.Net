using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// The symbol of the update
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The timestamp of the event
        /// </summary>
        [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// The type of the update
        /// </summary>
        [JsonPropertyName("type")]
        public MatchUpdateType? UpdateType { get; set; }
        /// <summary>
        /// The side of the order
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// The order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonPropertyName("orderType")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// The price of the order
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// The client order id
        /// </summary>
        [JsonPropertyName("clientOid")]
        public string? ClientOrderid { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonPropertyName("status")]
        public ExtendedOrderStatus? Status { get; set; }
        /// <summary>
        /// Order time
        /// </summary>
        [JsonPropertyName("orderTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? OrderTime { get; set; }
        /// <summary>
        /// Origin quantity
        /// </summary>
        [JsonPropertyName("originSize")]
        public decimal OriginalQuantity { get; set; }
        /// <summary>
        /// Origin value
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
        /// The quantity of the order
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Quantity before the update
        /// </summary>
        [JsonPropertyName("oldSize")]
        public decimal? OldQuantity { get; set; }
        /// <summary>
        /// Quantity filled
        /// </summary>
        [JsonPropertyName("filledSize")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// Quantity remaining
        /// </summary>
        [JsonPropertyName("remainSize")]
        public decimal QuantityRemaining { get; set; }
        /// <summary>
        /// Quantity remaining
        /// </summary>
        [JsonPropertyName("remainFunds")]
        public decimal? QuoteQuantityRemaining { get; set; }
        /// <summary>
        /// Quantity canceled
        /// </summary>
        [JsonPropertyName("canceledSize")]
        public decimal QuantityCanceled { get; set; }
        /// <summary>
        /// Value canceled
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
        /// The trade id
        /// </summary>
        [JsonPropertyName("tradeId")]
        public string TradeId { get; set; } = string.Empty;
        /// <summary>
        /// The price of the match
        /// </summary>
        [JsonPropertyName("matchPrice")]
        public decimal MatchPrice { get; set; }
        /// <summary>
        /// The quantity of the match
        /// </summary>
        [JsonPropertyName("matchSize")]
        public decimal MatchQuantity { get; set; }
        /// <summary>
        /// The liquidity
        /// </summary>
        [JsonPropertyName("liquidity")]
        public LiquidityType Liquidity { get; set; }
        /// <summary>
        /// Type of fee paid
        /// </summary>
        [JsonPropertyName("feeType")]
        public FeeType FeeType { get; set; }
    }
}
