using Kucoin.Net.Enums;
using System;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Sub Account balances
    /// </summary>
    public record KucoinUaSubAccountBalances
    {
        /// <summary>
        /// ["<c>ts</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>userList</c>"] Sub accounts
        /// </summary>
        [JsonPropertyName("userList")]
        public KucoinUaSubAccounts[] SubAccounts { get; set; } = [];
    }

    /// <summary>
    /// Sub account
    /// </summary>
    public record KucoinUaSubAccounts
    {
        /// <summary>
        /// ["<c>uid</c>"] Sub account id
        /// </summary>
        [JsonPropertyName("uid")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>accounts</c>"] Account list
        /// </summary>
        [JsonPropertyName("accounts")]
        public KucoinUaSubAccount[] Accounts { get; set; } = [];
    }

    /// <summary>
    /// Sub account info
    /// </summary>
    public record KucoinUaSubAccount
    {
        /// <summary>
        /// ["<c>accountType</c>"] Account type
        /// </summary>
        [JsonPropertyName("accountType")]
        public UnifiedAccountType AccountType { get; set; }
        /// <summary>
        /// ["<c>subAccountType</c>"] Account sub type
        /// </summary>
        [JsonPropertyName("subAccountType")]
        public string? AccountSubType { get; set; }
        /// <summary>
        /// ["<c>currencies</c>"] Asset balances
        /// </summary>
        [JsonPropertyName("currencies")]
        public KucoinUaSubAccountBalance[] Assets { get; set; } = [];
    }

    /// <summary>
    /// Sub account balance
    /// </summary>
    public record KucoinUaSubAccountBalance
    {
        /// <summary>
        /// ["<c>liabilityPrincipal</c>"] Liability principal
        /// </summary>
        [JsonPropertyName("liabilityPrincipal")]
        public decimal? LiabilityPrincipal { get; set; }
        /// <summary>
        /// ["<c>liabilityInterest</c>"] Liability interest
        /// </summary>
        [JsonPropertyName("liabilityInterest")]
        public decimal? LiabilityInterest { get; set; }
        /// <summary>
        /// ["<c>unrealisedPnl</c>"] Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealisedPnl")]
        public decimal? UnrealisedPnl { get; set; }
        /// <summary>
        /// ["<c>currency</c>"] Asset name
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>equity</c>"] Equity
        /// </summary>
        [JsonPropertyName("equity")]
        public decimal? Equity { get; set; }
        /// <summary>
        /// ["<c>locked</c>"] Locked
        /// </summary>
        [JsonPropertyName("locked")]
        public decimal Locked { get; set; }
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
        public decimal? Liability { get; set; }
    }
}
