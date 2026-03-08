namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// The order model in bulk order creation response
    /// </summary>
    [SerializationModel]
    public record KucoinBulkMinimalResponseEntry
    {
        /// <summary>
        /// ["<c>orderId</c>"] The id of the order
        /// </summary>
        [JsonPropertyName("orderId")]
        public string? OrderId { get; set; }
        /// <summary>
        /// ["<c>failMsg</c>"] The cause of failure
        /// </summary>
        [JsonPropertyName("failMsg")]
        public string? Error { get; set; }
        /// <summary>
        /// ["<c>success</c>"] Whether the call is successful
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}
