using System;


namespace Kucoin.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Stream tick
    /// </summary>
    [SerializationModel]
    public record KucoinStreamCandle
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>candles</c>"] Candles
        /// </summary>
        [JsonPropertyName("candles")]
        public KucoinKline Candles { get; set; } = default!;

        /// <summary>
        /// ["<c>time</c>"] The timestamp of the data
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
