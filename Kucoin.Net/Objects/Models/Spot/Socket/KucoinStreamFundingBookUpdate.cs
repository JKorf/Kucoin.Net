using CryptoExchange.Net.Converters.SystemTextJson;
using System;


namespace Kucoin.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Stream funding book update
    /// </summary>
    [SerializationModel]
    public record KucoinStreamFundingBookUpdate
    {
        /// <summary>
        /// The asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// Sequence number
        /// </summary>
        [JsonPropertyName("sequence")]
        public long Sequence { get; set; }

        /// <summary>
        /// The daily interest rate
        /// </summary>
        [JsonPropertyName("dailyIntRate")]
        public decimal DailyInterestRate { get; set; }

        /// <summary>
        /// The anual interest rate
        /// </summary>
        [JsonPropertyName("annualIntRate")]
        public decimal AnnualInterestRate { get; set; }

        /// <summary>
        /// Term (days)
        /// </summary>
        [JsonPropertyName("term")]
        public int Term { get; set; }
        /// <summary>
        /// Current total size
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Size { get; set; }

        /// <summary>
        /// Lend or borrow
        /// </summary>
        [JsonPropertyName("side")]
        public string Side { get; set; } = string.Empty;

        /// <summary>
        /// The timestamp of the data
        /// </summary>
        [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
