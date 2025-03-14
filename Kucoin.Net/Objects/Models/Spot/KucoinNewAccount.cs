using CryptoExchange.Net.Converters.SystemTextJson;
namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// New account id
    /// </summary>
    [SerializationModel]
    public record KucoinNewAccount
    {
        /// <summary>
        /// The id of the new account
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
    }
}
