using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects
{
    /// <summary>
    /// Tick info
    /// </summary>
    public class KucoinTicks
    {
        /// <summary>
        /// The timestamp of the data
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// The ticker data
        /// </summary>
        [JsonProperty("ticker")]
        public IEnumerable<KucoinAllTick> Data { get; set; } = Array.Empty<KucoinAllTick>();
    }
}
