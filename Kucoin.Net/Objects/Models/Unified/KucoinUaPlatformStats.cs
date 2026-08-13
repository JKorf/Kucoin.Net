namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Platform stats
    /// </summary>
    public record KucoinUaPlatformStats
    {
        /// <summary>
        /// ["<c>spot</c>"] Spot stats
        /// </summary>
        [JsonPropertyName("spot")]
        public KucoinUaPlatformStatsTopic Spot { get; set; } = null!;
        /// <summary>
        /// ["<c>futures</c>"] Futures stats
        /// </summary>
        [JsonPropertyName("futures")]
        public KucoinUaPlatformStatsTopic Futures { get; set; } = null!;
    }

    /// <summary>
    /// Platform stats
    /// </summary>
    public record KucoinUaPlatformStatsTopic
    {
        /// <summary>
        /// ["<c>turnoverOf24h</c>"] 24H turnover
        /// </summary>
        [JsonPropertyName("turnoverOf24h")]
        public decimal Turnover24h { get; set; }
    }
}
