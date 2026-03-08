namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Cross margin risk limit
    /// </summary>
    public record KucoinCrossMarginRiskLimit
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>maxOpenSize</c>"] Max open quantity
        /// </summary>
        [JsonPropertyName("maxOpenSize")]
        public decimal MaxOpenQuantity { get; set; }
        /// <summary>
        /// ["<c>maxOpenValue</c>"] Max open value
        /// </summary>
        [JsonPropertyName("maxOpenValue")]
        public decimal MaxOpenValue { get; set; }
        /// <summary>
        /// ["<c>totalMargin</c>"] Total margin
        /// </summary>
        [JsonPropertyName("totalMargin")]
        public decimal TotalMargin { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// ["<c>mmr</c>"] Maintenance margin rate
        /// </summary>
        [JsonPropertyName("mmr")]
        public decimal MaintenanceMarginRate { get; set; }
        /// <summary>
        /// ["<c>imr</c>"] Initial margin rate
        /// </summary>
        [JsonPropertyName("imr")]
        public decimal InitialMarginRate { get; set; }
        /// <summary>
        /// ["<c>currency</c>"] Margin asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
    }
}
