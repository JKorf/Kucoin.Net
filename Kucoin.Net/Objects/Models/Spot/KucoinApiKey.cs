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
        /// ["<c>remark</c>"] Remark
        /// </summary>
        [JsonPropertyName("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>apiKey</c>"] Api key
        /// </summary>
        [JsonPropertyName("apiKey")]
        public string Apikey { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>apiVersion</c>"] Version of the API key
        /// </summary>
        [JsonPropertyName("apiVersion")]
        public int ApiKeyVersion { get; set; }
        /// <summary>
        /// ["<c>permission</c>"] Permissions, comma seperated
        /// </summary>
        [JsonPropertyName("permission")]
        public string Permissions { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>createdAt</c>"] Creation time
        /// </summary>
        [JsonPropertyName("createdAt"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>uid</c>"] User id
        /// </summary>
        [JsonPropertyName("uid")]
        public long UserId { get; set; }
        /// <summary>
        /// ["<c>isMaster</c>"] Is master account
        /// </summary>
        [JsonPropertyName("isMaster")]
        public bool IsMaster { get; set; }
    }
}
