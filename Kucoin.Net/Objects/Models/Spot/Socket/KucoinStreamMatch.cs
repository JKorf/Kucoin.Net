using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Match info
    /// </summary>
    public class KucoinStreamMatch: KucoinStreamMatchBase
    {       
        /// <summary>
        /// The timestamp of the match
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(TimestampNanoSecondsConverter))]
        public new DateTime Timestamp { get; set; }
        /// <summary>
        /// The type
        /// </summary>
        public string Type { get; set; } = string.Empty;
    }
}
