using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Repayment record
    /// </summary>
    public class KucoinRepayRecord
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
        /// Accrued interest
        /// </summary>
        public decimal AccruedInterest { get; set; }
        /// <summary>
        /// Daily interest rate
        /// </summary>
        [JsonProperty("dailyIntRate")]
        public decimal DailyInterestRate { get; set; }
        /// <summary>
        /// Liability
        /// </summary>
        public decimal Liability { get; set; }
        /// <summary>
        /// Maturity time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime MaturityTime { get; set; }
        /// <summary>
        /// Principal
        /// </summary>
        public decimal Principal { get; set; }
        /// <summary>
        /// Repaid size
        /// </summary>
        public decimal RepaidSize { get; set; }
        /// <summary>
        /// Term
        /// </summary>
        public int Term { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        [JsonProperty("createdAt")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
    }
}
