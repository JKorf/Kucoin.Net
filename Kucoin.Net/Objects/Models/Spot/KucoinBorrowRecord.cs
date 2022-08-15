using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Borrow record
    /// </summary>
    public class KucoinBorrowRecord
    {
        /// <summary>
        /// Trade id
        /// </summary>
        public string TradeId { get; set; } = string.Empty;
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Daily interest rate
        /// </summary>
        [JsonProperty("dailyIntRate")]
        public decimal DailyInterestRate { get; set; }
        /// <summary>
        /// Interest
        /// </summary>
        public decimal Interest { get; set; }
        /// <summary>
        /// Principal
        /// </summary>
        public decimal Principal { get; set; }
        /// <summary>
        /// Repaid size
        /// </summary>
        public decimal RepaidSize { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        [JsonProperty("createdAt")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime RepayTime { get; set; }

        [JsonProperty("repayTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        private DateTime RepayTimeInternal
        {
            get => RepayTime;
            set => RepayTime = value;
        }

        /// <summary>
        /// Term
        /// </summary>
        public int Term { get; set; }
    }
}
