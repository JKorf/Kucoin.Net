using System;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Funding rate info
    /// </summary>
    public record KucoinUaFundingRateEntry
    {
        /// <summary>
        /// Funding rate
        /// </summary>
        [JsonPropertyName("fundingRate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
    }


}
