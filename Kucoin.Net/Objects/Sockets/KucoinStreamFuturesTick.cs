using CryptoExchange.Net.Converters;
using Kucoin.Net.Converts;
using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects.Futures
{
    /// <summary>
    /// Tick info
    /// </summary>
    public class KucoinStreamFuturesTick
    {
        /// <summary>
        /// Sequence number
        /// </summary>
        public long Sequence { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;        
        /// <summary>
        /// Best bid quantity
        /// </summary>
        [JsonProperty("bestBidSize")]
        public decimal BestBidQuantity { get; set; }
        /// <summary>
        /// Best bid price
        /// </summary>
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// Best ask quantity
        /// </summary>
        [JsonProperty("bestAskSize")]
        public decimal BestAskQuantity { get; set; }
        /// <summary>
        /// Best ask price
        /// </summary>
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// Filled time
        /// </summary>
        [JsonProperty("ts"), JsonConverter(typeof(TimestampNanoSecondsConverter))]
        public DateTime Timestamp { get; set; }
    }
}
