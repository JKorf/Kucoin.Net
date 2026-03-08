using System;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Funding rate info
    /// </summary>
    public record KucoinUaFundingRateEntry
    {
        /// <summary>
        /// ["<c>fundingRate</c>"] Funding rate
        /// </summary>
        [JsonPropertyName("fundingRate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// ["<c>ts</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
    }


}
