using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using CryptoExchange.Net.Interfaces;

namespace Kucoin.Net.Objects
{
    /// <summary>
    /// Order book
    /// </summary>
    public class KucoinFullOrderBook
    {
        /// <summary>
        /// The last sequence number of this order book state
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
        public IEnumerable<KucoinFullOrderBookEntry> Asks { get; set; } = new List<KucoinFullOrderBookEntry>();
        /// <summary>
        /// The list of bids
        /// </summary>
        public IEnumerable<KucoinFullOrderBookEntry> Bids { get; set; } = new List<KucoinFullOrderBookEntry>();
    }

    /// <summary>
    /// Order book
    /// </summary>
    public class KucoinOrderBook
    {
        /// <summary>
        /// The last sequence number of this order book state
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
        public IEnumerable<KucoinOrderBookEntry> Asks { get; set; } = new List<KucoinOrderBookEntry>();
        /// <summary>
        /// The list of bids
        /// </summary>
        public IEnumerable<KucoinOrderBookEntry> Bids { get; set; } = new List<KucoinOrderBookEntry>();
    }

    /// <summary>
    /// Order book entry
    /// </summary>
    [JsonConverter(typeof(ArrayConverter))]
    public class KucoinFullOrderBookEntry: ISymbolOrderBookEntry
    {
        /// <summary>
        /// The order id of the entry
        /// </summary>
        [ArrayProperty(0)]
        public string OrderId { get; set; } = "";
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

    /// <summary>
    /// Order book entry
    /// </summary>
    [JsonConverter(typeof(ArrayConverter))]
    public class KucoinOrderBookEntry : ISymbolOrderBookEntry
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
