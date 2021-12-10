using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects.Margin
{
    /// <summary>
    /// When a lending order is executed, the system will generate the lending history.<br/>
    /// The outstanding lend orders includes orders unrepaid and partially repaid 
    /// </summary>
    public class KucoinLendUnsettled
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
        public decimal Quantity { get; set; }

        /// <summary>
        /// Accrued interest. This value will decrease when borrower repays the interest.
        /// </summary>
        public decimal AccruedInterest { get; set; }

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
        /// Maturity time (millisecond)
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime MaturityTime { get; set; }
    }
}
