using CryptoExchange.Net.Converters.SystemTextJson;
namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Sub user api key info
    /// </summary>
    [SerializationModel]
    public record KucoinSubUserKeyEdited
    {
        /// <summary>
        /// The sub user name
        /// </summary>
        [JsonPropertyName("subName")]
        public string SubName { get; set; } = string.Empty;
        /// <summary>
        /// The API key
        /// </summary>
        [JsonPropertyName("apiKey")]
        public string ApiKey { get; set; } = string.Empty;
        /// <summary>
        /// Permissions
        /// </summary>
        [JsonPropertyName("permission")]
        public string? Permissions { get; set; }
        /// <summary>
        /// IP whitelist
        /// </summary>
        [JsonPropertyName("ipWhitelist")]
        public string? IpWhitelist { get; set; }       
    }
}
