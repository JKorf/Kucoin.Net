namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Cancel request
    /// </summary>
    [SerializationModel]
    public record KucoinCancelRequest
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>clientOid</c>"] Client order id
        /// </summary>
        [JsonPropertyName("clientOid")]
        public string ClientOrderId { get; set; } = string.Empty;
    }
}
