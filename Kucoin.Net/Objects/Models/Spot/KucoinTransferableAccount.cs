namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Transferable Account info
    /// </summary>
    [SerializationModel]
    public record KucoinTransferableAccount
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
        /// ["<c>available</c>"] The available balance of the account
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// ["<c>holds</c>"] The quantity of balance that's in hold
        /// </summary>
        [JsonPropertyName("holds")]
        public decimal Holds { get; set; }
        /// <summary>
        /// ["<c>transferable</c>"] The quantity of transferable balance
        /// </summary>
        [JsonPropertyName("transferable")]
        public decimal Transferable { get; set; }
    }
}
