using CryptoExchange.Net.Converters.SystemTextJson;

using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Announcement
    /// </summary>
    [SerializationModel]
    public record KucoinAnnouncement
    {
        /// <summary>
        /// Announcement id
        /// </summary>
        [JsonPropertyName("annId")]
        public long AnnouncementId { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        [JsonPropertyName("annTitle")]
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Description
        /// </summary>
        [JsonPropertyName("annDesc")]
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Create timestamp
        /// </summary>
        [JsonPropertyName("cTime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Language
        /// </summary>
        [JsonPropertyName("language")]
        public string Language { get; set; } = string.Empty;
        /// <summary>
        /// Url
        /// </summary>
        [JsonPropertyName("annUrl")]
        public string Url { get; set; } = string.Empty;
        /// <summary>
        /// Announcement types
        /// </summary>
        [JsonPropertyName("annType")]
        public string[] Types { get; set; } = Array.Empty<string>();
    }


}
