using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects
{
    public class KucoinFullOrderBook
    {
        /// <summary>
        /// The last sequnece number of this order book state
        /// </summary>
        public long Sequence { get; set; }
        /// <summary>
        /// The timestamp of the data
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(TimestampConverter))]        
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// The list of asks
        /// </summary>
        public KucoinFullOrderBookEntry[] Asks { get; set; }
        /// <summary>
        /// The list of bids
        /// </summary>
        public KucoinFullOrderBookEntry[] Bids { get; set; }
    }

    public class KucoinOrderBook
    {
        /// <summary>
        /// The last sequnece number of this order book state
        /// </summary>
        public long Sequence { get; set; }
        /// <summary>
        /// The timestamp of the data
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// The list of asks
        /// </summary>
        public KucoinOrderBookEntry[] Asks { get; set; }
        /// <summary>
        /// The list of bids
        /// </summary>
        public KucoinOrderBookEntry[] Bids { get; set; }
    }

    [JsonConverter(typeof(ArrayConverter))]
    public class KucoinFullOrderBookEntry
    {
        /// <summary>
        /// The order id of the entry
        /// </summary>
        [ArrayProperty(0)]
        public string OrderId { get; set; }
        /// <summary>
        /// The price of the entry
        /// </summary>
        [ArrayProperty(1)]
        public decimal Price { get; set; }
        /// <summary>
        /// The quantity of the entry
        /// </summary>
        [ArrayProperty(2)]
        public decimal Quantity { get; set; }
    }

    [JsonConverter(typeof(ArrayConverter))]
    public class KucoinOrderBookEntry
    {
        /// <summary>
        /// The price of the entry
        /// </summary>
        [ArrayProperty(0)]
        public decimal Price { get; set; }
        /// <summary>
        /// The quantity of the entry
        /// </summary>
        [ArrayProperty(1)]
        public decimal Quantity { get; set; }
    }
}
