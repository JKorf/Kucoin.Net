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
        /// ["<c>predictedValue</c>"] Predicted value
        /// </summary>
        [JsonPropertyName("predictedValue")]
        public decimal PredictedValue { get; set; }
        /// <summary>
        /// ["<c>fundingRateCap</c>"] Funding rate cap
        /// </summary>
        [JsonPropertyName("fundingRateCap")]
        public decimal? FundingRateCap { get; set; }
        /// <summary>
        /// ["<c>fundingRateFloor</c>"] Funding rate floor
        /// </summary>
        [JsonPropertyName("fundingRateFloor")]
        public decimal? FundingRateFloor { get; set; }
        /// <summary>
        /// ["<c>period</c>"] Indicates whether the current funding fee is charged within this cycle
        /// </summary>
        [JsonPropertyName("period")]
        public bool Period { get; set; }
        /// <summary>
        /// ["<c>fundingTime</c>"] Next funding time
        /// </summary>
        [JsonPropertyName("fundingTime")]
        public DateTime NextFundingTime { get; set; }
    }
}
