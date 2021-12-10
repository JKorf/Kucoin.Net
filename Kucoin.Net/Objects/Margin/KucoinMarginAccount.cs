using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Margin
{
    /// <summary>
    /// Margin account
    /// </summary>
    public class KucoinMarginAccount
    {
        /// <summary>
        /// Accounts details list
        /// </summary>
        public IEnumerable<KucoinMarginAccountDetail>? Accounts { get; set; } = Array.Empty<KucoinMarginAccountDetail>();

        /// <summary>
        /// Debt ratio
        /// </summary>
        public decimal DebtRatio { get; set; }
    }

    /// <summary>
    /// Margin account detail
    /// </summary>
    public class KucoinMarginAccountDetail
    {
        /// <summary>
        /// Available balance
        /// </summary>
        public decimal AvailableBalance { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// Hold balance
        /// </summary>
        public decimal HoldBalance { get; set; }

        /// <summary>
        /// Liabilities
        /// </summary>
        public decimal Liability { get; set; }

        /// <summary>
        /// Max borrow size
        /// </summary>
        public decimal MaxBorrowSize { get; set; }

        /// <summary>
        /// Total balance
        /// </summary>
        public decimal TotalBalance { get; set; }
    }
}
