using System;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Tick info
    /// </summary>
    [SerializationModel]
    public record KucoinTicks
    {
        /// <summary>
        /// ["<c>time</c>"] The timestamp of the data
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// ["<c>ticker</c>"] The ticker data
        /// </summary>
        [JsonPropertyName("ticker")]
        public KucoinAllTick[] Data { get; set; } = Array.Empty<KucoinAllTick>();
    }
}
