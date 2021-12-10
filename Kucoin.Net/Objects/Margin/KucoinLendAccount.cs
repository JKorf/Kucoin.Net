using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kucoin.Net.Objects.Margin
{
    /// <summary>
    /// Lend account
    /// </summary>
    public class KucoinLendAccount
    {
        /// <summary>
        /// Currency
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Outstanding size
        /// </summary>
        public decimal Outstanding { get; set; }
        /// <summary>
        /// Size executed
        /// </summary>
        public decimal FilledSize { get; set; }
        /// <summary>
        /// Accrued interest
        /// </summary>
        public decimal AccruedInterest { get; set; }
        /// <summary>
        /// Realized profit
        /// </summary>
        public decimal RealizedProfit { get; set; }
        /// <summary>
        /// Auto-lend enabled
        /// </summary>
        public bool IsAutoLend { get; set; }
    }
}
