using System;
using Kucoin.Net.Enums;


namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Tick info
    /// </summary>
    [SerializationModel]
    public record KucoinFuturesTick
    {
        /// <summary>
        /// ["<c>sequence</c>"] Sequence number
        /// </summary>
        [JsonPropertyName("sequence")]
        public long Sequence { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>side</c>"] Side of liquidity taker
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>size</c>"] Filled quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Filled price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>bestBidSize</c>"] Best bid quantity
        /// </summary>
        [JsonPropertyName("bestBidSize")]
        public decimal BestBidQuantity { get; set; }
        /// <summary>
        /// ["<c>bestBidPrice</c>"] Best bid price
        /// </summary>
        [JsonPropertyName("bestBidPrice")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>bestAskSize</c>"] Best ask quantity
        /// </summary>
        [JsonPropertyName("bestAskSize")]
        public decimal BestAskQuantity { get; set; }
        /// <summary>
        /// ["<c>bestAskPrice</c>"] Best ask price
        /// </summary>
        [JsonPropertyName("bestAskPrice")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>tradeId</c>"] Transaction id
        /// </summary>
        [JsonPropertyName("tradeId")]
        public string TradeId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>ts</c>"] Filled time
        /// </summary>
        [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
