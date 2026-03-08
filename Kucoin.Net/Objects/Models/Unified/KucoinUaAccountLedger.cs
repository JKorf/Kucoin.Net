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
        /// ["<c>lastId</c>"] Last id
        /// </summary>
        [JsonPropertyName("lastId")]
        public long? LastId { get; set; }
        /// <summary>
        /// ["<c>items</c>"] Items
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
        /// ["<c>accountType</c>"] Account type
        /// </summary>
        [JsonPropertyName("accountType")]
        public UnifiedAccountType AccountType { get; set; }
        /// <summary>
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>direction</c>"] Direction
        /// </summary>
        [JsonPropertyName("direction")]
        public AccountDirection Direction { get; set; }
        /// <summary>
        /// ["<c>businessType</c>"] Business type
        /// </summary>
        [JsonPropertyName("businessType")]
        public UnifiedBusinessType BusinessType { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>balance</c>"] Balance
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>tax</c>"] Tax
        /// </summary>
        [JsonPropertyName("tax")]
        public decimal Tax { get; set; }
        /// <summary>
        /// ["<c>remark</c>"] Remark
        /// </summary>
        [JsonPropertyName("remark")]
        public string? Remark { get; set; }
        /// <summary>
        /// ["<c>ts</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
    }


}
