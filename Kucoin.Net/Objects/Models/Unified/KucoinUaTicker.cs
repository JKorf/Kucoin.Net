namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Price ticker info
    /// </summary>
    public record KucoinUaTicker
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>name</c>"] Name
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        /// <summary>
        /// ["<c>bestBidSize</c>"] Best bid quantity
        /// </summary>
        [JsonPropertyName("bestBidSize")]
        public decimal? BestBidQuantity { get; set; }
        /// <summary>
        /// ["<c>bestBidPrice</c>"] Best bid price
        /// </summary>
        [JsonPropertyName("bestBidPrice")]
        public decimal? BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>bestAskSize</c>"] Best ask quantity
        /// </summary>
        [JsonPropertyName("bestAskSize")]
        public decimal? BestAskQuantity { get; set; }
        /// <summary>
        /// ["<c>bestAskPrice</c>"] Best ask price
        /// </summary>
        [JsonPropertyName("bestAskPrice")]
        public decimal? BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>lastPrice</c>"] Last trade price
        /// </summary>
        [JsonPropertyName("lastPrice")]
        public decimal? LastPrice { get; set; }
        /// <summary>
        /// ["<c>size</c>"] Last trade quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal? LastQuantity { get; set; }
        /// <summary>
        /// ["<c>open</c>"] Open price last 24h ago
        /// </summary>
        [JsonPropertyName("open")]
        public decimal? OpenPrice { get; set; }
        /// <summary>
        /// ["<c>high</c>"] High price last 24h
        /// </summary>
        [JsonPropertyName("high")]
        public decimal? HighPrice { get; set; }
        /// <summary>
        /// ["<c>low</c>"] Low price last 24h
        /// </summary>
        [JsonPropertyName("low")]
        public decimal? LowPrice { get; set; }
        /// <summary>
        /// ["<c>baseVolume</c>"] Base volume last 24h
        /// </summary>
        [JsonPropertyName("baseVolume")]
        public decimal BaseVolume { get; set; }
        /// <summary>
        /// ["<c>quoteVolume</c>"] Quote volume last 24h
        /// </summary>
        [JsonPropertyName("quoteVolume")]
        public decimal QuoteVolume { get; set; }
    }


}
