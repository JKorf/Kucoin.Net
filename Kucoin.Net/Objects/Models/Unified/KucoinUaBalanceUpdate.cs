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
        /// ["<c>E</c>"] Sequence number
        /// </summary>
        [JsonPropertyName("E")]
        public long Sequence { get; set; }
        /// <summary>
        /// ["<c>U</c>"] Update time
        /// </summary>
        [JsonPropertyName("U")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// ["<c>c</c>"] Asset
        /// </summary>
        [JsonPropertyName("c")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>e</c>"] Equity
        /// </summary>
        [JsonPropertyName("e")]
        public decimal? Equity { get; set; }
        /// <summary>
        /// ["<c>b</c>"] Balance
        /// </summary>
        [JsonPropertyName("b")]
        public decimal Balance { get; set; }
        /// <summary>
        /// ["<c>a</c>"] Available
        /// </summary>
        [JsonPropertyName("a")]
        public decimal Available { get; set; }
        /// <summary>
        /// ["<c>h</c>"] Frozen
        /// </summary>
        [JsonPropertyName("h")]
        public decimal? Frozen { get; set; }
        /// <summary>
        /// ["<c>l</c>"] Liability
        /// </summary>
        [JsonPropertyName("l")]
        public decimal? Liability { get; set; }
        /// <summary>
        /// ["<c>tCM</c>"] Total cross margin
        /// </summary>
        [JsonPropertyName("tCM")]
        public decimal? TotalCrossMargin { get; set; }
        /// <summary>
        /// ["<c>cPM</c>"] Cross position margin
        /// </summary>
        [JsonPropertyName("cPM")]
        public decimal? CrossPositionMargin { get; set; }
        /// <summary>
        /// ["<c>cOM</c>"] Cross order margin
        /// </summary>
        [JsonPropertyName("cOM")]
        public decimal? CrossOrderMargin { get; set; }
        /// <summary>
        /// ["<c>cUP</c>"] Cross unrealized profit and loss
        /// </summary>
        [JsonPropertyName("cUP")]
        public decimal? CrossUnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>iPM</c>"] Isolated position margin
        /// </summary>
        [JsonPropertyName("iPM")]
        public decimal? IsolatedPosMargin { get; set; }
        /// <summary>
        /// ["<c>iOM</c>"] Isolated order margin
        /// </summary>
        [JsonPropertyName("iOM")]
        public decimal? IsolatedOrderMargin { get; set; }
        /// <summary>
        /// ["<c>iFF</c>"] Isolated funding fee margin
        /// </summary>
        [JsonPropertyName("iFF")]
        public decimal? IsolatedFundingFeeMargin { get; set; }
        /// <summary>
        /// ["<c>iUP</c>"] Isolated unrealized profit and loss
        /// </summary>
        [JsonPropertyName("iUP")]
        public decimal? IsolatedUnrealizedPnl { get; set; }
    }
}
