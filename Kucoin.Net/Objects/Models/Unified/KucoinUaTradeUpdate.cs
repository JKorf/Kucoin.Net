using Kucoin.Net.Enums;
using Kucoin.Net.Objects.Models.Spot;
using System;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Trade update
    /// </summary>
    public record KucoinUaTradeUpdate
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
        /// ["<c>ti</c>"] Trade id
        /// </summary>
        [JsonPropertyName("ti")]
        public long TradeId { get; set; }
        /// <summary>
        /// ["<c>p</c>"] Trade price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>q</c>"] Trade quantity
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>S</c>"] Trade taker side
        /// </summary>
        [JsonPropertyName("S")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>rpi</c>"] Is RPI order
        /// </summary>
        [JsonPropertyName("rpi")]
        public bool Rpi { get; set; }
        /// <summary>
        /// ["<c>M</c>"] Update time
        /// </summary>
        [JsonPropertyName("M")]
        public DateTime UpdateTime { get; set; }
    }
}
