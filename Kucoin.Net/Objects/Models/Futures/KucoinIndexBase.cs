using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Base record for index data
    /// </summary>
    public record KucoinIndexBase
    {
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Granularity in milliseconds
        /// </summary>
        public int? Granularity { get; set; }
        /// <summary>
        /// Time point
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime TimePoint { get; set; }
        /// <summary>
        /// Value
        /// </summary>
        public decimal Value { get; set; }
    }
}
