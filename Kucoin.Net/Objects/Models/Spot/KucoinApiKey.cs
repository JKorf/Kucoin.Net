using CryptoExchange.Net.Converters.SystemTextJson;

using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// API key info
    /// </summary>
    [SerializationModel]
    public record KucoinApiKey
    {
        /// <summary>
        /// Remark
        /// </summary>
        [JsonPropertyName("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// Api key
        /// </summary>
        [JsonPropertyName("apiKey")]
        public string Apikey { get; set; } = string.Empty;
        /// <summary>
        /// Version of the API key
        /// </summary>
        [JsonPropertyName("apiVersion")]
        public int ApiKeyVersion { get; set; }
        /// <summary>
        /// Permissions, comma seperated
        /// </summary>
        [JsonPropertyName("permission")]
        public string Permissions { get; set; } = string.Empty;
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonPropertyName("createdAt"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// User id
        /// </summary>
        [JsonPropertyName("uid")]
        public long UserId { get; set; }
        /// <summary>
        /// Is master account
        /// </summary>
        [JsonPropertyName("isMaster")]
        public bool IsMaster { get; set; }
    }
}
