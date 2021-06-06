using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Sockets
{
    /// <summary>
    /// Stream funding book update
    /// </summary>
    public class KucoinStreamFundingBookUpdate
    {
        /// <summary>
        /// The currency
        /// </summary>
        public string Currency { get; set; } = "";

        /// <summary>
        /// Sequence number
        /// </summary>
        public long Sequence { get; set; }

        /// <summary>
        /// The daily interest rate
        /// </summary>
        [JsonProperty("dailyIntRate")]
        public decimal DailyInterestRate { get; set; }

        /// <summary>
        /// The anual interest rate
        /// </summary>
        [JsonProperty("annualIntRate")]
        public decimal AnnualInterestRate { get; set; }

        /// <summary>
        /// Term (days)
        /// </summary>
        public int Term { get; set; }
        /// <summary>
        /// Current total size
        /// </summary>
        public decimal Size { get; set; }

        /// <summary>
        /// Lend or borrow
        /// </summary>
        public string Side { get; set; }

        /// <summary>
        /// The timestamp of the data
        /// </summary>
        [JsonProperty("ts"), JsonConverter(typeof(TimestampNanoSecondsConverter))]
        public DateTime Timestamp { get; set; }
    }
}
