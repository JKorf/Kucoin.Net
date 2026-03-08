using Kucoin.Net.Enums;
using Kucoin.Net.Objects.Models.Spot;
using System;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// User trade update
    /// </summary>
    public record KucoinUaUserTradeUpdate
    {
        /// <summary>
        /// ["<c>oi</c>"] Order id
        /// </summary>
        [JsonPropertyName("oi")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>s</c>"] Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>oT</c>"] Order type
        /// </summary>
        [JsonPropertyName("oT")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// ["<c>S</c>"] Side
        /// </summary>
        [JsonPropertyName("S")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>lR</c>"] Liquidity role of the trade
        /// </summary>
        [JsonPropertyName("lR")]
        public LiquidityType? TradeRole { get; set; }
        /// <summary>
        /// ["<c>p</c>"] Order price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>ti</c>"] Trade id
        /// </summary>
        [JsonPropertyName("ti")]
        public long? TradeId { get; set; }
        /// <summary>
        /// ["<c>q</c>"] Order quantity
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>f</c>"] Total settle fee
        /// </summary>
        [JsonPropertyName("f")]
        public decimal? TotalSettleFee { get; set; }
        /// <summary>
        /// ["<c>fC</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("fC")]
        public string? FeeAsset { get; set; }
        /// <summary>
        /// ["<c>t</c>"] Total tax
        /// </summary>
        [JsonPropertyName("t")]
        public decimal? TotalTax { get; set; }
        /// <summary>
        /// ["<c>E</c>"] Trade time
        /// </summary>
        [JsonPropertyName("E")]
        public DateTime TradeTime { get; set; }
    }
}
