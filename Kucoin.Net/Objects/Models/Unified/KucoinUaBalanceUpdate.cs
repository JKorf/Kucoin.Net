using Kucoin.Net.Enums;
using Kucoin.Net.Objects.Models.Spot;
using System;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Balance update
    /// </summary>
    public record KucoinUaBalanceUpdate
    {
        /// <summary>
        /// Sequence number
        /// </summary>
        [JsonPropertyName("E")]
        public long Sequence { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("U")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("c")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Equity
        /// </summary>
        [JsonPropertyName("e")]
        public decimal? Equity { get; set; }
        /// <summary>
        /// Balance
        /// </summary>
        [JsonPropertyName("b")]
        public decimal Balance { get; set; }
        /// <summary>
        /// Available
        /// </summary>
        [JsonPropertyName("a")]
        public decimal Available { get; set; }
        /// <summary>
        /// Frozen
        /// </summary>
        [JsonPropertyName("h")]
        public decimal? Frozen { get; set; }
        /// <summary>
        /// Liability
        /// </summary>
        [JsonPropertyName("l")]
        public decimal? Liability { get; set; }
        /// <summary>
        /// Total cross margin
        /// </summary>
        [JsonPropertyName("tCM")]
        public decimal? TotalCrossMargin { get; set; }
        /// <summary>
        /// Cross position margin
        /// </summary>
        [JsonPropertyName("cPM")]
        public decimal? CrossPositionMargin { get; set; }
        /// <summary>
        /// Cross order margin
        /// </summary>
        [JsonPropertyName("cOM")]
        public decimal? CrossOrderMargin { get; set; }
        /// <summary>
        /// Cross unrealized profit and loss
        /// </summary>
        [JsonPropertyName("cUP")]
        public decimal? CrossUnrealizedPnl { get; set; }
        /// <summary>
        /// Isolated position margin
        /// </summary>
        [JsonPropertyName("iPM")]
        public decimal? IsolatedPosMargin { get; set; }
        /// <summary>
        /// Isolated order margin
        /// </summary>
        [JsonPropertyName("iOM")]
        public decimal? IsolatedOrderMargin { get; set; }
        /// <summary>
        /// Isolated funding fee margin
        /// </summary>
        [JsonPropertyName("iFF")]
        public decimal? IsolatedFundingFeeMargin { get; set; }
        /// <summary>
        /// Isolated unrealized profit and loss
        /// </summary>
        [JsonPropertyName("iUP")]
        public decimal? IsolatedUnrealizedPnl { get; set; }
    }
}
