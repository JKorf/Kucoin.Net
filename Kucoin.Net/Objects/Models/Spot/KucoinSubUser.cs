using CryptoExchange.Net.Converters.SystemTextJson;

using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Sub user info
    /// </summary>
    [SerializationModel]
    public record KucoinSubUser
    {
        /// <summary>
        /// The sub user id
        /// </summary>
        [JsonPropertyName("userId")]
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// The uid
        /// </summary>
        [JsonPropertyName("uid"), JsonConverter(typeof(NumberStringConverter))]
        public string Uid { get; set; } = string.Empty;
        /// <summary>
        /// The sub user name
        /// </summary>
        [JsonPropertyName("subName")]
        public string SubName { get; set; } = string.Empty;
        /// <summary>
        /// Status, 2: enabled, 3: frozen
        /// </summary>
        [JsonPropertyName("status")]
        public int Status { get; set; }
        /// <summary>
        /// Account type
        /// </summary>
        [JsonPropertyName("type")]
        public int Type { get; set; }
        /// <summary>
        /// Remarks for this sub user
        /// </summary>
        [JsonPropertyName("remarks")]
        public string? Remarks { get; set; }
        /// <summary>
        /// Permissions
        /// </summary>
        [JsonPropertyName("access")]
        public string Access { get; set; } = string.Empty;
        /// <summary>
        /// Key creation time
        /// </summary>
        [JsonPropertyName("createdAt"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
    }
}
