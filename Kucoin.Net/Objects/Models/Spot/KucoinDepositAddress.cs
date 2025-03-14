using CryptoExchange.Net.Converters.SystemTextJson;


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
        /// The address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// A memo for the address
        /// </summary>
        [JsonPropertyName("memo")]
        public string Memo { get; set; } = string.Empty;

        /// <summary>
        /// The chain of the address
        /// </summary>
        [JsonPropertyName("chainName")]
        public string Network { get; set; } = string.Empty;

        /// <summary>
        /// The token contract address
        /// </summary>
        [JsonPropertyName("contractAddress")]
        public string ContractAddress { get; set; } = string.Empty;

        /// <summary>
        /// The deposit account type
        /// </summary>
        [JsonPropertyName("to")]
        public AccountType ToAccount { get; set; }

        /// <summary>
        /// The asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// The id of the network
        /// </summary>
        [JsonPropertyName("chainId")]
        public string NetworkId { get; set; } = string.Empty;

        /// <summary>
        /// Expiration time
        /// </summary>
        [JsonPropertyName("expirationDate")]
        public DateTime? ExpirationTime { get; set; }
    }
}
