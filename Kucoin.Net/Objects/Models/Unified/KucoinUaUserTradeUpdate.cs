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
        /// Order id
        /// </summary>
        [JsonPropertyName("oi")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("oT")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        [JsonPropertyName("S")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Liquidity role of the trade
        /// </summary>
        [JsonPropertyName("lR")]
        public LiquidityType? TradeRole { get; set; }
        /// <summary>
        /// Order price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal? Price { get; set; }
        /// <summary>
        /// Trade id
        /// </summary>
        [JsonPropertyName("ti")]
        public long? TradeId { get; set; }
        /// <summary>
        /// Order quantity
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Total settle fee
        /// </summary>
        [JsonPropertyName("f")]
        public decimal? TotalSettleFee { get; set; }
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonPropertyName("fC")]
        public string? FeeAsset { get; set; }
        /// <summary>
        /// Total tax
        /// </summary>
        [JsonPropertyName("t")]
        public decimal? TotalTax { get; set; }
        /// <summary>
        /// Trade time
        /// </summary>
        [JsonPropertyName("E")]
        public DateTime TradeTime { get; set; }
    }
}
