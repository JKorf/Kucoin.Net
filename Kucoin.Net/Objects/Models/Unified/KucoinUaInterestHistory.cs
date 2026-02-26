using System;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Interest history
    /// </summary>
    public record KucoinUaInterestHistory
    {
        /// <summary>
        /// Items
        /// </summary>
        [JsonPropertyName("items")]
        public KucoinUaInterestHistoryEntry[] Items { get; set; } = [];
        /// <summary>
        /// Last id
        /// </summary>
        [JsonPropertyName("lastId")]
        public long? LastId { get; set; }
    }

    /// <summary>
    /// History entry
    /// </summary>
    public record KucoinUaInterestHistoryEntry
    {
        /// <summary>
        /// Liability
        /// </summary>
        [JsonPropertyName("liability")]
        public decimal Liability { get; set; }
        /// <summary>
        /// Interest
        /// </summary>
        [JsonPropertyName("interest")]
        public decimal Interest { get; set; }
        /// <summary>
        /// Hourly interest rate
        /// </summary>
        [JsonPropertyName("hourlyInterestRate")]
        public decimal HourlyInterestRate { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Interest free liability
        /// </summary>
        [JsonPropertyName("interestFreeLiability")]
        public decimal InterestFreeLiability { get; set; }
    }


}
