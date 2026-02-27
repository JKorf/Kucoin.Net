using Kucoin.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Account balances
    /// </summary>
    public record KucoinUaBalances
    {
        /// <summary>
        /// Account type
        /// </summary>
        [JsonPropertyName("accountType")]
        public UnifiedAccountType AccountType { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Accounts
        /// </summary>
        [JsonPropertyName("accounts")]
        public KucoinUaBalanceAccount[] Accounts { get; set; } = [];
    }

    /// <summary>
    /// Account
    /// </summary>
    public record KucoinUaBalanceAccount
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currencies")]
        public KucoinUaBalanceAsset[] Assets { get; set; } = [];
    }

    /// <summary>
    /// Asset balance
    /// </summary>
    public record KucoinUaBalanceAsset
    {
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Equity
        /// </summary>
        [JsonPropertyName("equity")]
        public decimal Equity { get; set; }
        /// <summary>
        /// Hold
        /// </summary>
        [JsonPropertyName("hold")]
        public decimal Hold { get; set; }
        /// <summary>
        /// Balance
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
        /// <summary>
        /// Available
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// Liability
        /// </summary>
        [JsonPropertyName("liability")]
        public decimal Liability { get; set; }
    }
}
