using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Sub user api key info
    /// </summary>
    [SerializationModel]
    public record KucoinSubUserKeyDetails
    {
        /// <summary>
        /// ["<c>subName</c>"] The sub user name
        /// </summary>
        [JsonPropertyName("subName")]
        public string SubName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>apiKey</c>"] The API key
        /// </summary>
        [JsonPropertyName("apiKey")]
        public string ApiKey { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>apiSecret</c>"] The secret value
        /// </summary>
        [JsonPropertyName("apiSecret")]
        public string Secret { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>passphrase</c>"] The passphrase/password
        /// </summary>
        [JsonPropertyName("passphrase")]
        public string Passphrase { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>remark</c>"] Remark
        /// </summary>
        [JsonPropertyName("remark")]
        public string? Remark { get; set; }
        /// <summary>
        /// ["<c>permission</c>"] Permissions
        /// </summary>
        [JsonPropertyName("permission")]
        public string Permissions { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>ipWhitelist</c>"] IP whitelist
        /// </summary>
        [JsonPropertyName("ipWhitelist")]
        public string IpWhitelist { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>createdAt</c>"] Key creation time
        /// </summary>
        [JsonPropertyName("createdAt"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
    }
}
