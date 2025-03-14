using CryptoExchange.Net.Converters.SystemTextJson;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Leveraged token info
    /// </summary>
    [SerializationModel]
    public record KucoinLeveragedToken
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Net worth
        /// </summary>
        [JsonPropertyName("netAsset")]
        public decimal NetWorth { get; set; }
        /// <summary>
        /// Target leverage
        /// </summary>
        [JsonPropertyName("targetLeverage")]
        public string TargetLeverage { get; set; } = string.Empty;
        /// <summary>
        /// Actual leverage
        /// </summary>
        [JsonPropertyName("actualLeverage")]
        public decimal ActualLeverage { get; set; }
        /// <summary>
        /// Assets under management
        /// </summary>
        [JsonPropertyName("assetsUnderManagement")]
        public string AssetsUnderManagement { get; set; } = string.Empty;
        /// <summary>
        /// Basket info
        /// </summary>
        [JsonPropertyName("basket")]
        public string Basket { get; set; } = string.Empty;
    }
}
