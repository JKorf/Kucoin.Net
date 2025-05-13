using CryptoExchange.Net.Converters.SystemTextJson;
namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// The order model in bulk order creation response
    /// </summary>
    [SerializationModel]
    public record KucoinBulkMinimalResponseEntry
    {
        /// <summary>
        /// The id of the order
        /// </summary>
        [JsonPropertyName("orderId")]
        public string? OrderId { get; set; }
        /// <summary>
        /// The cause of failure
        /// </summary>
        [JsonPropertyName("failMsg")]
        public string? Error { get; set; }
        /// <summary>
        /// Whether the call is successful
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}
