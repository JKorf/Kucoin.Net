using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Match info
    /// </summary>
    public record KucoinStreamFuturesMatch: KucoinStreamMatchBase
    {
        /// <summary>
        /// Gets time of the trade match
        /// </summary>
        [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Marer user id
        /// </summary>
        public string MakerUserId { get; set; } = string.Empty;
        /// <summary>
        /// Taker user id
        /// </summary>
        public string TakerUserId { get; set; } = string.Empty;
    }
}
