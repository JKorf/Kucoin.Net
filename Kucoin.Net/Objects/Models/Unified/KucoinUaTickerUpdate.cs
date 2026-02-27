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
        /// Symbol name
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Sequence number
        /// </summary>
        [JsonPropertyName("E")]
        public long Sequence { get; set; }
        /// <summary>
        /// Best bid price
        /// </summary>
        [JsonPropertyName("b")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// Best bid quantity
        /// </summary>
        [JsonPropertyName("B")]
        public decimal BestBidQuantity { get; set; }
        /// <summary>
        /// Best ask price
        /// </summary>
        [JsonPropertyName("a")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// Best ask quantity
        /// </summary>
        [JsonPropertyName("A")]
        public decimal BestAskQuantity { get; set; }
        /// <summary>
        /// Last trade price
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LastTradePrice { get; set; }
        /// <summary>
        /// Last trade quantity
        /// </summary>
        [JsonPropertyName("q")]
        public decimal LastTradeQuantity { get; set; }
        /// <summary>
        /// Last trade taker side
        /// </summary>
        [JsonPropertyName("S")]
        public OrderSide LastTradeSide { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("M")]
        public DateTime UpdateTime { get; set; }
    }
}
