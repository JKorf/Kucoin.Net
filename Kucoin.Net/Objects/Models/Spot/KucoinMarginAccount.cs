using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Margin account info
    /// </summary>
    public record KucoinMarginAccount
    {
        /// <summary>
        /// Accounts
        /// </summary>
        public IEnumerable<KucoinMarginAccountDetails> Accounts { get; set; } = Array.Empty<KucoinMarginAccountDetails>();
        /// <summary>
        /// Debt ratio
        /// </summary>
        public decimal DebtRatio { get; set; }
    }

    /// <summary>
    /// Account details
    /// </summary>
    public record KucoinMarginAccountDetails
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Available balance
        /// </summary>
        public decimal AvailableBalance { get; set; }
        /// <summary>
        /// Hold balance
        /// </summary>
        public decimal HoldBalance { get; set; }
        /// <summary>
        /// Liability
        /// </summary>
        public decimal Liability { get; set; }
        /// <summary>
        /// Max borrow quantity
        /// </summary>
        [JsonProperty("maxBorrowSize")]
        public decimal MaxBorrowQuantity { get; set; }
        /// <summary>
        /// Total balance
        /// </summary>
        public decimal TotalBalance { get; set; }
    }
}
