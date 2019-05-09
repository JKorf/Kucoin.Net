using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects
{
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
        public KucoinAllTick[] Data { get; set; }
    }
}
