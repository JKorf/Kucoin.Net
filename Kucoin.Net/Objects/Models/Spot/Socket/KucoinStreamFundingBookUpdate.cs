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
        /// ["<c>currency</c>"] The asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>sequence</c>"] Sequence number
        /// </summary>
        [JsonPropertyName("sequence")]
        public long Sequence { get; set; }

        /// <summary>
        /// ["<c>dailyIntRate</c>"] The daily interest rate
        /// </summary>
        [JsonPropertyName("dailyIntRate")]
        public decimal DailyInterestRate { get; set; }

        /// <summary>
        /// ["<c>annualIntRate</c>"] The anual interest rate
        /// </summary>
        [JsonPropertyName("annualIntRate")]
        public decimal AnnualInterestRate { get; set; }

        /// <summary>
        /// ["<c>term</c>"] Term (days)
        /// </summary>
        [JsonPropertyName("term")]
        public int Term { get; set; }
        /// <summary>
        /// ["<c>size</c>"] Current total size
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Size { get; set; }

        /// <summary>
        /// ["<c>side</c>"] Lend or borrow
        /// </summary>
        [JsonPropertyName("side")]
        public string Side { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>ts</c>"] The timestamp of the data
        /// </summary>
        [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
