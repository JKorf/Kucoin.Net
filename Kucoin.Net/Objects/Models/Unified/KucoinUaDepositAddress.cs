using Kucoin.Net.Enums;
using System;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Deposit address
    /// </summary>
    public record KucoinUaDepositAddress
    {
        /// <summary>
        /// Address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Memo
        /// </summary>
        [JsonPropertyName("memo")]
        public string? Memo { get; set; }
        /// <summary>
        /// Remark
        /// </summary>
        [JsonPropertyName("remark")]
        public string? Remark { get; set; }
        /// <summary>
        /// Network id
        /// </summary>
        [JsonPropertyName("chainId")]
        public string NetworkId { get; set; } = string.Empty;
        /// <summary>
        /// To
        /// </summary>
        [JsonPropertyName("to")]
        public UnifiedAccountType To { get; set; }
        /// <summary>
        /// Expiration date
        /// </summary>
        [JsonPropertyName("expirationDate")]
        public DateTime? ExpirationDate { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Contract address
        /// </summary>
        [JsonPropertyName("contractAddress")]
        public string ContractAddress { get; set; } = string.Empty;
        /// <summary>
        /// Network name
        /// </summary>
        [JsonPropertyName("chainName")]
        public string NetworkName { get; set; } = string.Empty;
    }


}
