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
        /// ["<c>accountType</c>"] Account type
        /// </summary>
        [JsonPropertyName("accountType")]
        public UnifiedAccountType AccountType { get; set; }
        /// <summary>
        /// ["<c>ts</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>accounts</c>"] Accounts
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
        /// ["<c>currencies</c>"] Asset
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
        /// ["<c>currency</c>"] Asset name
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>equity</c>"] Equity
        /// </summary>
        [JsonPropertyName("equity")]
        public decimal Equity { get; set; }
        /// <summary>
        /// ["<c>hold</c>"] Hold
        /// </summary>
        [JsonPropertyName("hold")]
        public decimal Hold { get; set; }
        /// <summary>
        /// ["<c>balance</c>"] Balance
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
        /// <summary>
        /// ["<c>available</c>"] Available
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// ["<c>liability</c>"] Liability
        /// </summary>
        [JsonPropertyName("liability")]
        public decimal Liability { get; set; }
    }
}
