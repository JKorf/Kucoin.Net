using CryptoExchange.Net.Converters.SystemTextJson;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Universal transfer
    /// </summary>
    [SerializationModel]
    public record KucoinUniversalTransfer
    {
        /// <summary>
        /// Orrder id
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
    }
}
