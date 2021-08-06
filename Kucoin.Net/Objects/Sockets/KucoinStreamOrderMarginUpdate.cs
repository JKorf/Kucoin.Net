using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kucoin.Net.Objects.Sockets
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
        /// Currency
        /// </summary>
        public string Currency { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
    }
}
