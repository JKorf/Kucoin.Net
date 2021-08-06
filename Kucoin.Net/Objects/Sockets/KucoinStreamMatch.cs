using CryptoExchange.Net.Converters;
using Kucoin.Net.Converts;
using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects.Sockets
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
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// The type
        /// </summary>
        public string Type { get; set; } = string.Empty;
    }
}
