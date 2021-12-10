using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects.Margin
{
    /// <summary>
    /// Index price of specified symbol
    /// </summary>
    public class KucoinMarkPrice
    {
        /// <summary>
        /// Symbol (Pair)
        /// </summary>
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Data granularity (millisecond)
        /// </summary>
        public int Granularity { get; set; }

        /// <summary>
        /// Time (millisecond)
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime TimePoint { get; set; }

        /// <summary>
        /// Mark price
        /// </summary>
        [JsonProperty("value")]
        public decimal MarkPrice { get; set; }
    }
}
