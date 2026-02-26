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
        /// Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Sub accounts
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
        /// Sub account id
        /// </summary>
        [JsonPropertyName("uid")]
        public long Id { get; set; }
        /// <summary>
        /// Account list
        /// </summary>
        [JsonPropertyName("accountList")]
        public KucoinUaSubAccount[] Accounts { get; set; } = [];
    }

    /// <summary>
    /// Sub account info
    /// </summary>
    public record KucoinUaSubAccount
    {
        /// <summary>
        /// Account type
        /// </summary>
        [JsonPropertyName("accountType")]
        public UnifiedAccountType AccountType { get; set; }
        /// <summary>
        /// Account sub type
        /// </summary>
        [JsonPropertyName("accountSubType")]
        public string? AccountSubType { get; set; }
        /// <summary>
        /// Asset balances
        /// </summary>
        [JsonPropertyName("currencyList")]
        public KucoinUaSubAccountBalance[] Assets { get; set; } = [];
    }

    /// <summary>
    /// Sub account balance
    /// </summary>
    public record KucoinUaSubAccountBalance
    {
        /// <summary>
        /// Liability principle
        /// </summary>
        [JsonPropertyName("liabilityPrinciple")]
        public decimal? LiabilityPrinciple { get; set; }
        /// <summary>
        /// Liability interest
        /// </summary>
        [JsonPropertyName("liabilityInterest")]
        public decimal? LiabilityInterest { get; set; }
        /// <summary>
        /// Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealisedPnl")]
        public decimal? UnrealisedPnl { get; set; }
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Equity
        /// </summary>
        [JsonPropertyName("equity")]
        public decimal? Equity { get; set; }
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
        public decimal? Liability { get; set; }
    }
}
