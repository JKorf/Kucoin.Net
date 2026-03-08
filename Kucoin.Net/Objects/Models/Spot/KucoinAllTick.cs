namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Tick info
    /// </summary>
    [SerializationModel]
    public record KucoinAllTick
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol of the tick
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbolName</c>"] Name of trading pairs, it would change after renaming
        /// </summary>
        [JsonPropertyName("symbolName")]
        public string SymbolName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>sell</c>"] The best ask price
        /// </summary>
        [JsonPropertyName("sell")]
        public decimal? BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>bestAskSize</c>"] The quantity of the best ask
        /// </summary>
        [JsonPropertyName("bestAskSize")]
        public decimal? BestAskQuantity { get; set; }
        /// <summary>
        /// ["<c>buy</c>"] The best bid price
        /// </summary>
        [JsonPropertyName("buy")]
        public decimal? BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>bestBidSize</c>"] The quantity of the best bid
        /// </summary>
        [JsonPropertyName("bestBidSize")]
        public decimal? BestBidQuantity { get; set; }
        /// <summary>
        /// ["<c>changeRate</c>"] The percentage change
        /// </summary>
        [JsonPropertyName("changeRate")]
        public decimal? ChangePercentage { get; set; }
        /// <summary>
        /// ["<c>changePrice</c>"] The price change
        /// </summary>
        [JsonPropertyName("changePrice")]
        public decimal? ChangePrice { get; set; }
        /// <summary>
        /// ["<c>high</c>"] The highest price
        /// </summary>
        [JsonPropertyName("high")]
        public decimal? HighPrice { get; set; }
        /// <summary>
        /// ["<c>low</c>"] The lowest price
        /// </summary>
        [JsonPropertyName("low")]
        public decimal? LowPrice { get; set; }
        /// <summary>
        /// ["<c>vol</c>"] The volume in this tick
        /// </summary>
        [JsonPropertyName("vol")]
        public decimal? Volume { get; set; }
        /// <summary>
        /// ["<c>volValue</c>"] The value of the volume in this tick
        /// </summary>
        [JsonPropertyName("volValue")]
        public decimal? QuoteVolume { get; set; }
        /// <summary>
        /// ["<c>last</c>"] The last trade price
        /// </summary>
        [JsonPropertyName("last")]
        public decimal? LastPrice { get; set; }
        /// <summary>
        /// ["<c>averagePrice</c>"] The average trade price in the last 24 hours
        /// </summary>
        [JsonPropertyName("averagePrice")]
        public decimal? AveragePrice { get; set; }

        /// <summary>
        /// ["<c>takerFeeRate</c>"] Basic Taker Fee
        /// </summary>
        [JsonPropertyName("takerFeeRate")]
        public decimal? TakerFeeRate { get; set; }
        /// <summary>
        /// ["<c>makerFeeRate</c>"] Basic Maker Fee
        /// </summary>
        [JsonPropertyName("makerFeeRate")]
        public decimal? MakerFeeRate { get; set; }
        /// <summary>
        /// ["<c>takerCoefficient</c>"] Taker Fee Coefficient
        /// </summary>
        [JsonPropertyName("takerCoefficient")]
        public decimal? TakerCoefficient { get; set; }
        /// <summary>
        /// ["<c>makerCoefficient</c>"] Maker Fee Coefficient
        /// </summary>
        [JsonPropertyName("makerCoefficient")]
        public decimal? MakerCoefficient { get; set; }
    }
}
