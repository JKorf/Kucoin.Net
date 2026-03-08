using Kucoin.Net.Enums;
using System;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Announcement
    /// </summary>
    public record KucoinUaAnnouncement
    {
        /// <summary>
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>title</c>"] Title
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>type</c>"] Types
        /// </summary>
        [JsonPropertyName("type")]
        public AnnouncementType[] Types { get; set; } = [];
        /// <summary>
        /// ["<c>description</c>"] Description
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>releaseTime</c>"] Release time
        /// </summary>
        [JsonPropertyName("releaseTime")]
        public DateTime ReleaseTime { get; set; }
        /// <summary>
        /// ["<c>language</c>"] Language
        /// </summary>
        [JsonPropertyName("language")]
        public string Language { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>url</c>"] Url
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
    }


}
