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
        /// Last id
        /// </summary>
        [JsonPropertyName("lastId")]
        public long? LastId { get; set; }
        /// <summary>
        /// Trade type
        /// </summary>
        [JsonPropertyName("tradeType")]
        public UnifiedAccountType TradeType { get; set; }
        /// <summary>
        /// Items
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
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
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
        /// Fill type
        /// </summary>
        [JsonPropertyName("fillType")]
        public UnifiedTradeType FillType { get; set; }
        /// <summary>
        /// Trade id
        /// </summary>
        [JsonPropertyName("tradeId")]
        public long TradeId { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Value
        /// </summary>
        [JsonPropertyName("value")]
        public decimal Value { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Execution time
        /// </summary>
        [JsonPropertyName("executionTime")]
        public DateTime ExecutionTime { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonPropertyName("feeCurrency")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Liquidity role
        /// </summary>
        [JsonPropertyName("liquidityRole")]
        public LiquidityType LiquidityRole { get; set; }
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonPropertyName("marginMode")]
        public MarginMode? MarginMode { get; set; }
        /// <summary>
        /// Tax
        /// </summary>
        [JsonPropertyName("tax")]
        public decimal Tax { get; set; }
    }


}
