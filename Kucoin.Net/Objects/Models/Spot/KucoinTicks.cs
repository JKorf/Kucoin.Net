using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Spot
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
