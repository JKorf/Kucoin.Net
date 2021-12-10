using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects.Margin
{
    /// <summary>
    /// Margin trade data
    /// </summary>
    public class KucoinMarginTradeData
    {
        /// <summary>
        /// Trade ID
        /// </summary>
        public string TradeId { get; set; } = string.Empty;

        /// <summary>
        /// Currency
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// Executed size
        /// </summary>
        public decimal Size { get; set; }

        /// <summary>
        /// Daily interest rate. e.g. 0.002 is 0.2%
        /// </summary>
        [JsonProperty("dailyIntRate")]
        public decimal DailyInterestRate { get; set; }

        /// <summary>
        /// Term (Unit: Day)
        /// </summary>
        public int Term { get; set; }

        /// <summary>
        /// Time of execution in nanosecond
        /// </summary>
        [JsonConverter(typeof(TimestampNanoSecondsConverter))]
        public DateTime Timestamp { get; set; }
    }
}