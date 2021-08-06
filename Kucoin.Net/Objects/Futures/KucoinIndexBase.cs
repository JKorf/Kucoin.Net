using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kucoin.Net.Objects.Futures
{
    /// <summary>
    /// Base class for index data
    /// </summary>
    public class KucoinIndexBase
    {
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Granularity in milliseconds
        /// </summary>
        public int Granularity { get; set; }
        /// <summary>
        /// Time point
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime TimePoint { get; set; }
        /// <summary>
        /// Value
        /// </summary>
        public decimal Value { get; set; }
    }
}
