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
        /// Account type
        /// </summary>
        [JsonPropertyName("accountType")]
        public UnifiedAccountType? AccountType { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Transferable
        /// </summary>
        [JsonPropertyName("transferable")]
        public decimal Transferable { get; set; }
    }


}
