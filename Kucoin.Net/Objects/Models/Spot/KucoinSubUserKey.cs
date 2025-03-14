using CryptoExchange.Net.Converters.SystemTextJson;

using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Sub user api key info
    /// </summary>
    [SerializationModel]
    public record KucoinSubUserKey
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
        /// Remark
        /// </summary>
        [JsonPropertyName("remark")]
        public string? Remark { get; set; }
        /// <summary>
        /// Permissions
        /// </summary>
        [JsonPropertyName("permission")]
        public string Permissions { get; set; } = string.Empty;
        /// <summary>
        /// IP whitelist
        /// </summary>
        [JsonPropertyName("ipWhitelist")]
        public string IpWhitelist { get; set; } = string.Empty;
        /// <summary>
        /// Key creation time
        /// </summary>
        [JsonPropertyName("createdAt"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Version of the API key
        /// </summary>
        [JsonPropertyName("apiVersion")]
        public int ApiKeyVersion { get; set; }
    }
}
