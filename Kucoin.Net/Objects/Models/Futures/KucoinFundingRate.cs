using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Funding rate info
    /// </summary>
    public record KucoinFundingRate: KucoinIndexBase
    {
        /// <summary>
        /// Predicted value
        /// </summary>
        public decimal PredictedValue { get; set; }
        /// <summary>
        /// Funding rate cap
        /// </summary>
        [JsonProperty("fundingRateCap")]
        public decimal? FundingRateCap { get; set; }
        /// <summary>
        /// Funding rate floor
        /// </summary>
        [JsonProperty("fundingRateFloor")]
        public decimal? FundingRateFloor { get; set; }
    }
}
