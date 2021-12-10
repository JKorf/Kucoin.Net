using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects.Margin
{
    /// <summary>
    /// The settled lend orders include orders repaid fully or partially before or at the maturity time 
    /// </summary>
    public class KucoinLendSettled
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
        /// Size executed
        /// </summary>
        [JsonProperty("size")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Accrued interest. This value will decrease when borrower repays the interest.
        /// </summary>
        public decimal TotalInterest { get; set; }

        /// <summary>
        /// Repaid size
        /// </summary>
        public decimal Repaid { get; set; }

        /// <summary>
        /// Daily interest rate
        /// </summary>
        [JsonProperty("dailyIntRate")]
        public decimal DailyInterestRate { get; set; }

        /// <summary>
        /// Term (Unit: Day)
        /// </summary>
        public int Term { get; set; }

        /// <summary>
        /// Settlement time (millisecond)
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime SettledAt { get; set; }

        /// <summary>
        /// Note. To note the account of the borrower reached a negative balance, and whether the insurance fund is repaid.
        /// </summary>
        public string? Note { get; set; } = string.Empty;
    }
}
