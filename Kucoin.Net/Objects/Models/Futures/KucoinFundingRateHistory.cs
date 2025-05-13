using CryptoExchange.Net.Converters.SystemTextJson;

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
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Funding rate
        /// </summary>
        [JsonPropertyName("fundingRate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("timepoint")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
