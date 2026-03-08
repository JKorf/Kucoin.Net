using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Announcement
    /// </summary>
    [SerializationModel]
    public record KucoinAnnouncement
    {
        /// <summary>
        /// ["<c>annId</c>"] Announcement id
        /// </summary>
        [JsonPropertyName("annId")]
        public long AnnouncementId { get; set; }
        /// <summary>
        /// ["<c>annTitle</c>"] Title
        /// </summary>
        [JsonPropertyName("annTitle")]
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>annDesc</c>"] Description
        /// </summary>
        [JsonPropertyName("annDesc")]
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>cTime</c>"] Create timestamp
        /// </summary>
        [JsonPropertyName("cTime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>language</c>"] Language
        /// </summary>
        [JsonPropertyName("language")]
        public string Language { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>annUrl</c>"] Url
        /// </summary>
        [JsonPropertyName("annUrl")]
        public string Url { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>annType</c>"] Announcement types
        /// </summary>
        [JsonPropertyName("annType")]
        public string[] Types { get; set; } = Array.Empty<string>();
    }


}
