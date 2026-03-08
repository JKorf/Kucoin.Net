using Kucoin.Net.Enums;
using System;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Trades
    /// </summary>
    public record KucoinUaUserTrades
    {
        /// <summary>
        /// ["<c>lastId</c>"] Last id
        /// </summary>
        [JsonPropertyName("lastId")]
        public long? LastId { get; set; }
        /// <summary>
        /// ["<c>tradeType</c>"] Trade type
        /// </summary>
        [JsonPropertyName("tradeType")]
        public UnifiedAccountType TradeType { get; set; }
        /// <summary>
        /// ["<c>items</c>"] Items
        /// </summary>
        [JsonPropertyName("items")]
        public KucoinUaUserTrade[] Items { get; set; } = [];
    }

    /// <summary>
    /// Trade info
    /// </summary>
    public record KucoinUaUserTrade
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
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
        /// ["<c>fillType</c>"] Fill type
        /// </summary>
        [JsonPropertyName("fillType")]
        public UnifiedTradeType FillType { get; set; }
        /// <summary>
        /// ["<c>tradeId</c>"] Trade id
        /// </summary>
        [JsonPropertyName("tradeId")]
        public long TradeId { get; set; }
        /// <summary>
        /// ["<c>size</c>"] Quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>value</c>"] Value
        /// </summary>
        [JsonPropertyName("value")]
        public decimal Value { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>executionTime</c>"] Execution time
        /// </summary>
        [JsonPropertyName("executionTime")]
        public DateTime ExecutionTime { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>feeCurrency</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("feeCurrency")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>liquidityRole</c>"] Liquidity role
        /// </summary>
        [JsonPropertyName("liquidityRole")]
        public LiquidityType LiquidityRole { get; set; }
        /// <summary>
        /// ["<c>marginMode</c>"] Margin mode
        /// </summary>
        [JsonPropertyName("marginMode")]
        public MarginMode? MarginMode { get; set; }
        /// <summary>
        /// ["<c>tax</c>"] Tax
        /// </summary>
        [JsonPropertyName("tax")]
        public decimal Tax { get; set; }
    }


}
