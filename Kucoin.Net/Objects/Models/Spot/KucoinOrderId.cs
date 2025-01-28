

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Order id
    /// </summary>
    public record KucoinOrderId
    {
        /// <summary>
        /// The id of the order
        /// </summary>
        [JsonPropertyName("orderId")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// The client order id
        /// </summary>
        [JsonPropertyName("clientOid")]
        public string? ClientOrderId { get; set; }
    }
}
