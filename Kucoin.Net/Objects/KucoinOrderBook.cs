using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using CryptoExchange.Net.OrderBook;

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
        public KucoinFullOrderBookEntry[] Asks { get; set; }
        /// <summary>
        /// The list of bids
        /// </summary>
        public KucoinFullOrderBookEntry[] Bids { get; set; }
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
        public KucoinOrderBookEntry[] Asks { get; set; }
        /// <summary>
        /// The list of bids
        /// </summary>
        public KucoinOrderBookEntry[] Bids { get; set; }
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
