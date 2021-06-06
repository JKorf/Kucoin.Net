using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kucoin.Net.Objects.Sockets
{
    /// <summary>
    /// Index price update
    /// </summary>
    public class KucoinStreamIndicatorPrice
    {
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = "";
        /// <summary>
        /// Granularity
        /// </summary>
        public int Granularity { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Value
        /// </summary>
        public decimal Value { get; set; }
    }
}
