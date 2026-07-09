using Kucoin.Net.Enums;
using Kucoin.Net.Objects.Models.Spot;
using System;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Funding fee update
    /// </summary>
    public record KucoinUaFundingFeeUpdate
    {
        /// <summary>
        /// ["<c>s</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fr</c>"] Funding fee rate
        /// </summary>
        [JsonPropertyName("fr")]
        public decimal FundingFeeRate { get; set; }
        /// <summary>
        /// ["<c>ft</c>"] Last funding time
        /// </summary>
        [JsonPropertyName("ft")]
        public DateTime LastFundingTime { get; set; }
        /// <summary>
        /// ["<c>nt</c>"] Next funding time
        /// </summary>
        [JsonPropertyName("nt")]
        public DateTime NextFundingTime { get; set; }
        /// <summary>
        /// ["<c>gl</c>"] Funding fee interval
        /// </summary>
        [JsonPropertyName("gl")]
        public long FundingInterval { get; set; }
        /// <summary>
        /// ["<c>fc</c>"] Max funding rate
        /// </summary>
        [JsonPropertyName("fc")]
        public decimal? MaxFundingRate { get; set; }
        /// <summary>
        /// ["<c>ff</c>"] Min funding rate
        /// </summary>
        [JsonPropertyName("ff")]
        public decimal? MinFundingRate { get; set; }

    }
}
