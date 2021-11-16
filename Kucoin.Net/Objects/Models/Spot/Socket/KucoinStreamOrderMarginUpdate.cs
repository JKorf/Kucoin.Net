using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Spot.Socket
{
    /// <summary>
    /// Margin update
    /// </summary>
    public class KucoinStreamOrderMarginUpdate
    {
        /// <summary>
        /// Order margin
        /// </summary>
        public decimal OrderMargin { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
    }
}
