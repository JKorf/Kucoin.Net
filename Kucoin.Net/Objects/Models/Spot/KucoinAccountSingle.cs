using CryptoExchange.Net.Converters.SystemTextJson;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Account info
    /// </summary>
    [SerializationModel]
    public record KucoinAccountSingle
    {
        /// <summary>
        /// The asset of the account
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The total balance of the account
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal Total { get; set; }
        /// <summary>
        /// the available quantity in the account
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// The quantity in hold of the account
        /// </summary>
        [JsonPropertyName("holds")]
        public decimal Holds { get; set; }
    }
}
