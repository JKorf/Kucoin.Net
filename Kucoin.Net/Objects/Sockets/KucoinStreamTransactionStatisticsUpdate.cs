using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kucoin.Net.Objects.Sockets
{
    /// <summary>
    /// 24 Hour statistics update
    /// </summary>
    public class KucoinStreamTransactionStatisticsUpdate
    {
        /// <summary>
        /// Volume
        /// </summary>
        public decimal Volume { get; set; }
        /// <summary>
        /// Turnover
        /// </summary>
        public decimal Turnover { get; set; }
        /// <summary>
        /// Last price
        /// </summary>
        public decimal LastPrice { get; set; }
        /// <summary>
        /// Price change percentage
        /// </summary>
        [JsonProperty("priceChgPct")]
        public decimal PriceChangePercentage { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("ts"), JsonConverter(typeof(TimestampNanoSecondsConverter))]
        public DateTime Timestamp { get; set; }
    }
}
