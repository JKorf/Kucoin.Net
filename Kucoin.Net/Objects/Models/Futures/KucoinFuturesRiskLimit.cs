namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Risk limit info
    /// </summary>
    [SerializationModel]
    public record KucoinFuturesRiskLimit
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>level</c>"] Level
        /// </summary>
        [JsonPropertyName("level")]
        public int Level { get; set; }
        /// <summary>
        /// ["<c>maxRiskLimit</c>"] Max risk limit
        /// </summary>
        [JsonPropertyName("maxRiskLimit")]
        public int MaxRiskLimit { get; set; }
        /// <summary>
        /// ["<c>minRiskLimit</c>"] Min risk limit
        /// </summary>
        [JsonPropertyName("minRiskLimit")]
        public int MinRiskLimit { get; set; }
        /// <summary>
        /// ["<c>maxLeverage</c>"] Max leverage
        /// </summary>
        [JsonPropertyName("maxLeverage")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// ["<c>initialMargin</c>"] Initial margin
        /// </summary>
        [JsonPropertyName("initialMargin")]
        public decimal InitialMargin { get; set; }
        /// <summary>
        /// ["<c>maintainMargin</c>"] Maintenance margin
        /// </summary>
        [JsonPropertyName("maintainMargin")]
        public decimal MaintainMargin { get; set; }
    }
}
