namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Leverage info
    /// </summary>
    [SerializationModel]
    public record KucoinLeverage
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
    }


}
