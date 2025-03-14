using CryptoExchange.Net.Converters.SystemTextJson;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Canceled order
    /// </summary>
    [SerializationModel]
    public record KucoinCanceledOrder
    {
        /// <summary>
        /// Order id of the canceled order
        /// </summary>
        
        [JsonPropertyName("cancelledOrderId")]
        public string CanceledOrderId { get; set; } = string.Empty;
        /// <summary>
        /// Client order id of the canceled order
        /// </summary>
        [JsonPropertyName("clientOid")]
        public string ClientOrderId { get; set; } = string.Empty;
    }
}
