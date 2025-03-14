using CryptoExchange.Net.Converters.SystemTextJson;
namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Sub transfer info
    /// </summary>
    [SerializationModel]
    public record KucoinInnerTransfer
    {
        /// <summary>
        /// The id of the new sub transfer
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
    }
}
