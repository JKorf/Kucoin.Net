using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects
{
    [JsonConverter(typeof(ArrayConverter))]
    public class KucoinKline
    {
        /// <summary>
        /// The start time of the kline
        /// </summary>
        [ArrayProperty(0), JsonConverter(typeof(TimestampSecondsConverter))]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// The open price
        /// </summary>
        [ArrayProperty(1)]
        public decimal Open { get; set; }
        /// <summary>
        /// The close price
        /// </summary>
        [ArrayProperty(2)]
        public decimal Close { get; set; }
        /// <summary>
        /// The highest price during this kline
        /// </summary>
        [ArrayProperty(3)]
        public decimal High { get; set; }
        /// <summary>
        /// The lowest price during this kline
        /// </summary>
        [ArrayProperty(4)]
        public decimal Low { get; set; }
        /// <summary>
        /// The amount of the kline
        /// </summary>
        [ArrayProperty(5)]
        public decimal Amount { get; set; }
        /// <summary>
        /// The volume of the kline
        /// </summary>
        [ArrayProperty(6)]
        public decimal Volume { get; set; }
    }
}
