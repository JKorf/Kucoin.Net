namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Account info
    /// </summary>
    [SerializationModel]
    public record KucoinAccountSingle
    {
        /// <summary>
        /// ["<c>currency</c>"] The asset of the account
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>balance</c>"] The total balance of the account
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal Total { get; set; }
        /// <summary>
        /// ["<c>available</c>"] the available quantity in the account
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// ["<c>holds</c>"] The quantity in hold of the account
        /// </summary>
        [JsonPropertyName("holds")]
        public decimal Holds { get; set; }
    }
}
