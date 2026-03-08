using System;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Tick info
    /// </summary>
    [SerializationModel]
    public record KucoinTick
    {
        /// <summary>
        /// ["<c>sequence</c>"] The sequence of the tick
        /// </summary>
        [JsonPropertyName("sequence")]
        public long Sequence { get; set; }
        /// <summary>
        /// ["<c>price</c>"] The price of the last trade
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? LastPrice { get; set; }
        /// <summary>
        /// ["<c>size</c>"] The quantity of the last trade
        /// </summary>
        [JsonPropertyName("size")]
        public decimal? LastQuantity { get; set; }
        /// <summary>
        /// ["<c>bestAsk</c>"] The best ask price
        /// </summary>
        [JsonPropertyName("bestAsk")]
        public decimal? BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>bestAskSize</c>"] The quantity of the best ask price
        /// </summary>
        [JsonPropertyName("bestAskSize")]
        public decimal? BestAskQuantity { get; set; }
        /// <summary>
        /// ["<c>bestBid</c>"] The best bid price
        /// </summary>
        [JsonPropertyName("bestBid")]
        public decimal? BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>bestBidSize</c>"] The quantity of the best bid
        /// </summary>
        [JsonPropertyName("bestBidSize")]
        public decimal? BestBidQuantity { get; set; }
        /// <summary>
        /// ["<c>time</c>"] The timestamp of the data
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
