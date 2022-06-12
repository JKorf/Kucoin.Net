using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Lending open market entry
    /// </summary>
    public class KucoinLendingMarketEntry
    {
        /// <summary>
        /// Daily interest rate
        /// </summary>
        [JsonProperty("dailyIntRate")]
        public decimal DailyInterestRate { get; set; }
        /// <summary>
        /// Term in days
        /// </summary>
        public int Term { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
    }
}
