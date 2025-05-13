using CryptoExchange.Net.Converters.SystemTextJson;

using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Interest info
    /// </summary>
    [SerializationModel]
    public record KucoinMarginInterest
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Daily interest rate
        /// </summary>
        [JsonPropertyName("dayRatio")]
        public decimal DailyInterestRate { get; set; }
        /// <summary>
        /// Interest quantity
        /// </summary>
        [JsonPropertyName("interestAmount")]
        public decimal InterestQuantity { get; set; }
        /// <summary>
        /// Timesatmp
        /// </summary>
        [JsonPropertyName("createdTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
