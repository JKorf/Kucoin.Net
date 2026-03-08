namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Sub user api key info
    /// </summary>
    [SerializationModel]
    public record KucoinSubUserKeyEdited
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
        /// ["<c>permission</c>"] Permissions
        /// </summary>
        [JsonPropertyName("permission")]
        public string? Permissions { get; set; }
        /// <summary>
        /// ["<c>ipWhitelist</c>"] IP whitelist
        /// </summary>
        [JsonPropertyName("ipWhitelist")]
        public string? IpWhitelist { get; set; }       
    }
}
