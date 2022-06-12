using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Lend history info
    /// </summary>
    public class KucoinLendHistory
    {
        /// <summary>
        /// Asset lend
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Outstanding
        /// </summary>
        public decimal Outstanding { get; set; }
        /// <summary>
        /// Filled
        /// </summary>
        [JsonProperty("filledSize")]
        public decimal Filled { get; set; }
        /// <summary>
        /// Accrued interest
        /// </summary>
        public decimal AccruedInterest { get; set; }
        /// <summary>
        /// Realized profits
        /// </summary>
        public decimal RealizedProfit { get; set; }
        /// <summary>
        /// Auto lend enabled
        /// </summary>
        public bool IsAutoLend { get; set; }
    }
}
