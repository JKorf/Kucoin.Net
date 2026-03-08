namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Leveraged token info
    /// </summary>
    [SerializationModel]
    public record KucoinLeveragedToken
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>netAsset</c>"] Net worth
        /// </summary>
        [JsonPropertyName("netAsset")]
        public decimal NetWorth { get; set; }
        /// <summary>
        /// ["<c>targetLeverage</c>"] Target leverage
        /// </summary>
        [JsonPropertyName("targetLeverage")]
        public string TargetLeverage { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>actualLeverage</c>"] Actual leverage
        /// </summary>
        [JsonPropertyName("actualLeverage")]
        public decimal ActualLeverage { get; set; }
        /// <summary>
        /// ["<c>assetsUnderManagement</c>"] Assets under management
        /// </summary>
        [JsonPropertyName("assetsUnderManagement")]
        public string AssetsUnderManagement { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>basket</c>"] Basket info
        /// </summary>
        [JsonPropertyName("basket")]
        public string Basket { get; set; } = string.Empty;
    }
}
