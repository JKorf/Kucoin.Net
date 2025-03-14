using CryptoExchange.Net.Converters.SystemTextJson;
namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Risk limit info
    /// </summary>
    [SerializationModel]
    public record KucoinFuturesRiskLimit
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Level
        /// </summary>
        [JsonPropertyName("level")]
        public int Level { get; set; }
        /// <summary>
        /// Max risk limit
        /// </summary>
        [JsonPropertyName("maxRiskLimit")]
        public int MaxRiskLimit { get; set; }
        /// <summary>
        /// Min risk limit
        /// </summary>
        [JsonPropertyName("minRiskLimit")]
        public int MinRiskLimit { get; set; }
        /// <summary>
        /// Max leverage
        /// </summary>
        [JsonPropertyName("maxLeverage")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// Initial margin
        /// </summary>
        [JsonPropertyName("initialMargin")]
        public decimal InitialMargin { get; set; }
        /// <summary>
        /// Maintenance margin
        /// </summary>
        [JsonPropertyName("maintainMargin")]
        public decimal MaintainMargin { get; set; }
    }
}
