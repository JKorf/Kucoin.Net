using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Margin
{
    /// <summary>
    /// Lending market data
    /// </summary>
    public class KucoinLendingMarketData
    {
        /// <summary>
        /// Daily interest rate. e.g. 0.002 is 0.2%
        /// </summary>
        [JsonProperty("dailyIntRate")]
        public decimal DailyInterestRate { get; set; }

        /// <summary>
        /// Term (Unit: day)
        /// </summary>
        public int Term { get; set; }

        /// <summary>
        /// Total size
        /// </summary>
        public decimal TotalSize { get; set; }
    }
}