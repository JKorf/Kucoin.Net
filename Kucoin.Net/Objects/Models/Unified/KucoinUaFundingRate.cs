using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Funding rate
    /// </summary>
    public record KucoinUaFundingRate
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Next funding rate
        /// </summary>
        [JsonPropertyName("nextFundingRate")]
        public decimal NextFundingRate { get; set; }
        /// <summary>
        /// Funding time
        /// </summary>
        [JsonPropertyName("fundingTime")]
        public DateTime? FundingTime { get; set; }
        /// <summary>
        /// Funding rate cap
        /// </summary>
        [JsonPropertyName("fundingRateCap")]
        public decimal FundingRateCap { get; set; }
        /// <summary>
        /// Funding rate floor
        /// </summary>
        [JsonPropertyName("fundingRateFloor")]
        public decimal FundingRateFloor { get; set; }
    }
}
