using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Isolated closed borrow record
    /// </summary>
    public class KucoinIsolatedClosedBorrowRecord
    {
        /// <summary>
        /// Loan id
        /// </summary>
        public string LoanId { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Principal total
        /// </summary>
        public decimal PrincipalTotal { get; set; }
        /// <summary>
        /// Accrued interest
        /// </summary>
        public decimal InterestBalance { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("createdAt")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Repay finish time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("repayFinishAt")]
        public DateTime RepayFinishTime { get; set; }
        /// <summary>
        /// Term
        /// </summary>
        public int Period { get; set; }
        /// <summary>
        /// Amount repaid
        /// </summary>
        public decimal RepaidSize { get; set; }
        /// <summary>
        /// Daily interest
        /// </summary>
        public decimal DailyInterestRate { get; set; }
    }
}
