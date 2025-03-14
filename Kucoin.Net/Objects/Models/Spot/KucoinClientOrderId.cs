using CryptoExchange.Net.Converters.SystemTextJson;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Client order id
    /// </summary>
    [SerializationModel]
    public record KucoinClientOrderId
    {
        /// <summary>
        /// The client id of the order
        /// </summary>
        [JsonPropertyName("clientOid")]
        public string ClientOrderId { get; set; } = string.Empty;
    }
}
