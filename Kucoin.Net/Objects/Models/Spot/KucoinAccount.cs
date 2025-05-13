using CryptoExchange.Net.Converters.SystemTextJson;
using Kucoin.Net.Enums;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Account info
    /// </summary>
    [SerializationModel]
    public record KucoinAccount
    {
        /// <summary>
        /// The id of the account
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// The asset of the account
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The type of the account
        /// </summary>
        [JsonPropertyName("type")]
        public AccountType Type { get; set; }
        /// <summary>
        /// The total balance of the account
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal Total { get; set; }
        /// <summary>
        /// The available balance of the account
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// The quantity of balance that's in hold
        /// </summary>
        [JsonPropertyName("holds")]
        public decimal Holds { get; set; }
    }
}
