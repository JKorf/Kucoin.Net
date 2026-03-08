namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Margin requirements
    /// </summary>
    public record KucoinCrossMarginRequirement
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>imr</c>"] Initial margin requirements
        /// </summary>
        [JsonPropertyName("imr")]
        public decimal InitialMarginRequirement { get; set; }
        /// <summary>
        /// ["<c>mmr</c>"] Maintenance margin requirement
        /// </summary>
        [JsonPropertyName("mmr")]
        public decimal MaintenanceMarginRequirement { get; set; }
        /// <summary>
        /// ["<c>size</c>"] Position size in contracts
        /// </summary>
        [JsonPropertyName("size")]
        public int PositionSize { get; set; }
        /// <summary>
        /// ["<c>positionValue</c>"] Position value
        /// </summary>
        [JsonPropertyName("positionValue")]
        public decimal PositionValue { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Trade price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }
}
