using Kucoin.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Transfer quotas
    /// </summary>
    public record KucoinUaTransferQuotas
    {
        /// <summary>
        /// ["<c>accountType</c>"] Account type
        /// </summary>
        [JsonPropertyName("accountType")]
        public UnifiedAccountType? AccountType { get; set; }
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>transferable</c>"] Transferable
        /// </summary>
        [JsonPropertyName("transferable")]
        public decimal Transferable { get; set; }
    }


}
