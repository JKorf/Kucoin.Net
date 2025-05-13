using CryptoExchange.Net.Converters.SystemTextJson;
using System;


namespace Kucoin.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Match info
    /// </summary>
    [SerializationModel]
    public record KucoinStreamFuturesMatch: KucoinStreamMatchBase
    {
        /// <summary>
        /// Gets time of the trade match
        /// </summary>
        [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Marer user id
        /// </summary>
        [JsonPropertyName("makerUserId")]
        public string MakerUserId { get; set; } = string.Empty;
        /// <summary>
        /// Taker user id
        /// </summary>
        [JsonPropertyName("takerUserId")]
        public string TakerUserId { get; set; } = string.Empty;
    }
}
