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
        /// Trade id
        /// </summary>
        [JsonPropertyName("ti")]
        public long TradeId { get; set; }
        /// <summary>
        /// Trade price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// Trade quantity
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Trade taker side
        /// </summary>
        [JsonPropertyName("S")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Is RPI order
        /// </summary>
        [JsonPropertyName("rpi")]
        public bool Rpi { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("M")]
        public DateTime UpdateTime { get; set; }
    }
}
