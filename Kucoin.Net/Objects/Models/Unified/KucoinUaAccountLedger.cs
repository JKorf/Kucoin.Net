using Kucoin.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Account ledger
    /// </summary>
    public record KucoinUaAccountLedger
    {
        /// <summary>
        /// Last id
        /// </summary>
        [JsonPropertyName("lastId")]
        public long? LastId { get; set; }
        /// <summary>
        /// Items
        /// </summary>
        [JsonPropertyName("items")]
        public KucoinUaAccountLedgerEntry[] Items { get; set; } = [];
    }

    /// <summary>
    /// Ledger entry
    /// </summary>
    public record KucoinUaAccountLedgerEntry
    {
        /// <summary>
        /// Account type
        /// </summary>
        [JsonPropertyName("accountType")]
        public UnifiedAccountType AccountType { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Direction
        /// </summary>
        [JsonPropertyName("direction")]
        public AccountDirection Direction { get; set; }
        /// <summary>
        /// Business type
        /// </summary>
        [JsonPropertyName("businessType")]
        public UnifiedBusinessType BusinessType { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Balance
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Tax
        /// </summary>
        [JsonPropertyName("tax")]
        public decimal Tax { get; set; }
        /// <summary>
        /// Remark
        /// </summary>
        [JsonPropertyName("remark")]
        public string? Remark { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
    }


}
