using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Tick info
    /// </summary>
    public record KucoinTicks
    {
        /// <summary>
        /// The timestamp of the data
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// The ticker data
        /// </summary>
        [JsonProperty("ticker")]
        public IEnumerable<KucoinAllTick> Data { get; set; } = Array.Empty<KucoinAllTick>();
    }
}
