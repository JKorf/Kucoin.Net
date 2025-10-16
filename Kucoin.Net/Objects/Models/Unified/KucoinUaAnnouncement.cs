using Kucoin.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Announcement
    /// </summary>
    public record KucoinUaAnnouncement
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Types
        /// </summary>
        [JsonPropertyName("type")]
        public AnnouncementType[] Types { get; set; } = [];
        /// <summary>
        /// Description
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Release time
        /// </summary>
        [JsonPropertyName("releaseTime")]
        public DateTime ReleaseTime { get; set; }
        /// <summary>
        /// Language
        /// </summary>
        [JsonPropertyName("language")]
        public string Language { get; set; } = string.Empty;
        /// <summary>
        /// Url
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
    }


}
