

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Deposit address
    /// </summary>
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
        [JsonPropertyName("chain")]
        public string Network { get; set; } = string.Empty;

        /// <summary>
        /// The token contract address
        /// </summary>
        [JsonPropertyName("contractAddress")]
        public string ContractAddress { get; set; } = string.Empty;
    }
}
