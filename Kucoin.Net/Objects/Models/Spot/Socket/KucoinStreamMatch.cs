using System;


namespace Kucoin.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Match info
    /// </summary>
    [SerializationModel]
    public record KucoinStreamMatch: KucoinStreamMatchBase
    {
        /// <summary>
        /// ["<c>type</c>"] The type
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>time</c>"] Gets time of the trade match
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
