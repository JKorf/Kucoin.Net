using Kucoin.Net.Enums;
using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Deposit address
    /// </summary>
    [SerializationModel]
    public record KucoinDepositAddress
    {
        /// <summary>
        /// ["<c>address</c>"] The address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>memo</c>"] A memo for the address
        /// </summary>
        [JsonPropertyName("memo")]
        public string Memo { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>chainName</c>"] The chain of the address
        /// </summary>
        [JsonPropertyName("chainName")]
        public string Network { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>contractAddress</c>"] The token contract address
        /// </summary>
        [JsonPropertyName("contractAddress")]
        public string ContractAddress { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>to</c>"] The deposit account type
        /// </summary>
        [JsonPropertyName("to")]
        public AccountType ToAccount { get; set; }

        /// <summary>
        /// ["<c>currency</c>"] The asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>chainId</c>"] The id of the network
        /// </summary>
        [JsonPropertyName("chainId")]
        public string NetworkId { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>expirationDate</c>"] Expiration time
        /// </summary>
        [JsonPropertyName("expirationDate")]
        public DateTime? ExpirationTime { get; set; }
    }
}
