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
        /// ["<c>address</c>"] Address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>memo</c>"] Memo
        /// </summary>
        [JsonPropertyName("memo")]
        public string? Memo { get; set; }
        /// <summary>
        /// ["<c>remark</c>"] Remark
        /// </summary>
        [JsonPropertyName("remark")]
        public string? Remark { get; set; }
        /// <summary>
        /// ["<c>chainId</c>"] Network id
        /// </summary>
        [JsonPropertyName("chainId")]
        public string NetworkId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>to</c>"] To
        /// </summary>
        [JsonPropertyName("to")]
        public UnifiedAccountType To { get; set; }
        /// <summary>
        /// ["<c>expirationDate</c>"] Expiration date
        /// </summary>
        [JsonPropertyName("expirationDate")]
        public DateTime? ExpirationDate { get; set; }
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contractAddress</c>"] Contract address
        /// </summary>
        [JsonPropertyName("contractAddress")]
        public string ContractAddress { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>chainName</c>"] Network name
        /// </summary>
        [JsonPropertyName("chainName")]
        public string NetworkName { get; set; } = string.Empty;
    }


}
