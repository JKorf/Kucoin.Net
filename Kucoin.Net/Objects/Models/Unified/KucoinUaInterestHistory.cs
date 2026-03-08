using System;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Interest history
    /// </summary>
    public record KucoinUaInterestHistory
    {
        /// <summary>
        /// ["<c>items</c>"] Items
        /// </summary>
        [JsonPropertyName("items")]
        public KucoinUaInterestHistoryEntry[] Items { get; set; } = [];
        /// <summary>
        /// ["<c>lastId</c>"] Last id
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
        /// ["<c>liability</c>"] Liability
        /// </summary>
        [JsonPropertyName("liability")]
        public decimal Liability { get; set; }
        /// <summary>
        /// ["<c>interest</c>"] Interest
        /// </summary>
        [JsonPropertyName("interest")]
        public decimal Interest { get; set; }
        /// <summary>
        /// ["<c>hourlyInterestRate</c>"] Hourly interest rate
        /// </summary>
        [JsonPropertyName("hourlyInterestRate")]
        public decimal HourlyInterestRate { get; set; }
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>ts</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>interestFreeLiability</c>"] Interest free liability
        /// </summary>
        [JsonPropertyName("interestFreeLiability")]
        public decimal InterestFreeLiability { get; set; }
    }


}
