using CryptoExchange.Net.Converters.SystemTextJson;


namespace Kucoin.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Result of updating risk limit level
    /// </summary>
    [SerializationModel]
    public record KucoinPositionRiskAdjustResultUpdate
    {
        /// <summary>
        /// Successfull or not
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        /// <summary>
        /// Current risk limit level
        /// </summary>
        [JsonPropertyName("riskLimitLevel")]
        public bool RiskLimitLevel { get; set; }
        /// <summary>
        /// Failure reason
        /// </summary>
        [JsonPropertyName("msg")]
        public string Message { get; set; } = string.Empty;
    }
}
