using CryptoExchange.Net.Converters.SystemTextJson;
using System;


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
        /// <summary>
        /// Indicates whether the current funding fee is charged within this cycle
        /// </summary>
        [JsonPropertyName("period")]
        public bool Period { get; set; }
        /// <summary>
        /// Next funding time
        /// </summary>
        [JsonPropertyName("fundingTime")]
        public DateTime NextFundingTime { get; set; }
    }
}
