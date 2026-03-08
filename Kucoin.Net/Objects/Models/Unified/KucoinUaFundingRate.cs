using System;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Funding rate
    /// </summary>
    public record KucoinUaFundingRate
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>nextFundingRate</c>"] Next funding rate
        /// </summary>
        [JsonPropertyName("nextFundingRate")]
        public decimal NextFundingRate { get; set; }
        /// <summary>
        /// ["<c>fundingTime</c>"] Funding time
        /// </summary>
        [JsonPropertyName("fundingTime")]
        public DateTime? FundingTime { get; set; }
        /// <summary>
        /// ["<c>fundingRateCap</c>"] Funding rate cap
        /// </summary>
        [JsonPropertyName("fundingRateCap")]
        public decimal FundingRateCap { get; set; }
        /// <summary>
        /// ["<c>fundingRateFloor</c>"] Funding rate floor
        /// </summary>
        [JsonPropertyName("fundingRateFloor")]
        public decimal FundingRateFloor { get; set; }
    }
}
