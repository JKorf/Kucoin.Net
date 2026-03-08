using Kucoin.Net.Enums;
using System;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Ticker update
    /// </summary>
    public record KucoinUaTickerUpdate
    {
        /// <summary>
        /// ["<c>s</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>E</c>"] Sequence number
        /// </summary>
        [JsonPropertyName("E")]
        public long Sequence { get; set; }
        /// <summary>
        /// ["<c>b</c>"] Best bid price
        /// </summary>
        [JsonPropertyName("b")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>B</c>"] Best bid quantity
        /// </summary>
        [JsonPropertyName("B")]
        public decimal BestBidQuantity { get; set; }
        /// <summary>
        /// ["<c>a</c>"] Best ask price
        /// </summary>
        [JsonPropertyName("a")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>A</c>"] Best ask quantity
        /// </summary>
        [JsonPropertyName("A")]
        public decimal BestAskQuantity { get; set; }
        /// <summary>
        /// ["<c>l</c>"] Last trade price
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LastTradePrice { get; set; }
        /// <summary>
        /// ["<c>q</c>"] Last trade quantity
        /// </summary>
        [JsonPropertyName("q")]
        public decimal LastTradeQuantity { get; set; }
        /// <summary>
        /// ["<c>S</c>"] Last trade taker side
        /// </summary>
        [JsonPropertyName("S")]
        public OrderSide LastTradeSide { get; set; }
        /// <summary>
        /// ["<c>M</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("M")]
        public DateTime UpdateTime { get; set; }
    }
}
