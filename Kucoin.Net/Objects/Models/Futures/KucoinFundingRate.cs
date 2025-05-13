using CryptoExchange.Net.Converters.SystemTextJson;


namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Funding rate info
    /// </summary>
    [SerializationModel]
    public record KucoinFundingRate: KucoinIndexBase
    {
        /// <summary>
        /// Predicted value
        /// </summary>
        [JsonPropertyName("predictedValue")]
        public decimal PredictedValue { get; set; }
        /// <summary>
        /// Funding rate cap
        /// </summary>
        [JsonPropertyName("fundingRateCap")]
        public decimal? FundingRateCap { get; set; }
        /// <summary>
        /// Funding rate floor
        /// </summary>
        [JsonPropertyName("fundingRateFloor")]
        public decimal? FundingRateFloor { get; set; }
    }
}
