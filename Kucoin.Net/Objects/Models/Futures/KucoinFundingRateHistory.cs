using System;

namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Funding rate history
    /// </summary>
    [SerializationModel]
    public record KucoinFundingRateHistory
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fundingRate</c>"] Funding rate
        /// </summary>
        [JsonPropertyName("fundingRate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// ["<c>timepoint</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("timepoint")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
