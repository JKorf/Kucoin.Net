using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Sockets
{
    /// <summary>
    /// Stream tick
    /// </summary>
    public class KucoinStreamCandle
    {
        /// <summary>
        /// The symbol
        /// </summary>
        public string Symbol { get; set; } = "";

        /// <summary>
        /// Candles
        /// </summary>
        public KucoinKline Candles { get; set; }

        /// <summary>
        /// The timestamp of the data
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(TimestampNanoSecondsConverter))]
        public DateTime Timestamp { get; set; }
    }
}
