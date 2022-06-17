using Kucoin.Net.Converters;
using Kucoin.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Margin account summary
    /// </summary>
    public class KucoinMarginAccount
    {
        /// <summary>
        /// The id of the account
        /// </summary>
        [JsonProperty("debtRatio")]
        public decimal DebtRatio { get; set; }

        /// <summary>
        /// The list of margin accounts
        /// </summary>
        [JsonProperty("accounts")]
        public IEnumerable<KucoinMarginAccountInfo> Accounts { get; set; } = Array.Empty<KucoinMarginAccountInfo>();
    }
    /// <summary>
    /// Margin account info
    /// </summary>
    public class KucoinMarginAccountInfo
    {
        /// <summary>
        /// The asset of the account
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; } = string.Empty;
        /// <summary>
        /// The available balance of the account
        /// </summary>
        [JsonProperty("availableBalance")]
        public decimal Available { get; set; }
        /// <summary>
        /// The quantity of balance that's in hold
        /// </summary>
        [JsonProperty("holdBalance")]
        public decimal Holds { get; set; }
        /// <summary>
        /// The total balance of the account
        /// </summary>
        [JsonProperty("liability")]
        public decimal Liability { get; set; }
        /// <summary>
        /// The total balance of the account
        /// </summary>
        [JsonProperty("maxBorrowSize")]
        public decimal MaxBorrowSize { get; set; }
        /// <summary>
        /// The total balance of the account
        /// </summary>
        [JsonProperty("totalBalance")]
        public decimal TotalBalance { get; set; }
    }
}
