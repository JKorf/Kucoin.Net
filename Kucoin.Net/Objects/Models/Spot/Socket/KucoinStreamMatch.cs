using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Match info
    /// </summary>
    public record KucoinStreamMatch: KucoinStreamMatchBase
    {
        /// <summary>
        /// The type
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets time of the trade match
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
