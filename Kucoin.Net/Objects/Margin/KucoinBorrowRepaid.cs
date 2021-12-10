using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects.Margin
{
    /// <summary>
    /// Borrow repaid record
    /// </summary>
    public class KucoinBorrowRepaid
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
        /// Interests
        /// </summary>
        [JsonProperty("interest")]
        public decimal Interests { get; set; }

        /// <summary>
        /// Principal
        /// </summary>
        public decimal Principal { get; set; }

        /// <summary>
        /// Repayment time 
        /// </summary>
        [JsonProperty("repayTime"), JsonConverter(typeof(TimestampConverter))]
        public DateTime RepayedAt { get; set; }

        /// <summary>
        /// Term
        /// </summary>
        public int Term { get; set; }

        /// <summary>
        /// Repaid size
        /// </summary>
        [JsonProperty("repaidSize")]
        public decimal Repaid { get; set; }

        /// <summary>
        /// Daily interest rate
        /// </summary>
        [JsonProperty("dailyIntRate")]
        public decimal DailyInterestRate { get; set; }
    }
}
