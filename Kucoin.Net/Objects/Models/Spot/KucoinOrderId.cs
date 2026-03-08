namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Order id
    /// </summary>
    [SerializationModel]
    public record KucoinOrderId
    {
        /// <summary>
        /// ["<c>orderId</c>"] The id of the order
        /// </summary>
        [JsonPropertyName("orderId")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>clientOid</c>"] The client order id
        /// </summary>
        [JsonPropertyName("clientOid")]
        public string? ClientOrderId { get; set; }
    }
}
