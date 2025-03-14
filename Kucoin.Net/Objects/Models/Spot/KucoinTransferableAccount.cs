using CryptoExchange.Net.Converters.SystemTextJson;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Transferable Account info
    /// </summary>
    [SerializationModel]
    public record KucoinTransferableAccount
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
        /// The available balance of the account
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// The quantity of balance that's in hold
        /// </summary>
        [JsonPropertyName("holds")]
        public decimal Holds { get; set; }
        /// <summary>
        /// The quantity of transferable balance
        /// </summary>
        [JsonPropertyName("transferable")]
        public decimal Transferable { get; set; }
    }
}
