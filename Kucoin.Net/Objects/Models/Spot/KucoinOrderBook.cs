using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Interfaces;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Spot
{
    /// <summary>
    /// Order book
    /// </summary>
    public class KucoinFullOrderBook: ICommonOrderBook
    {
        /// <summary>
        /// The last sequence number of this order book state
        /// </summary>
        public long Sequence { get; set; }
        /// <summary>
        /// The timestamp of the data
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(TimestampNanoSecondsConverter))]        
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// The list of asks
        /// </summary>
        public IEnumerable<KucoinFullOrderBookEntry> Asks { get; set; } = Array.Empty<KucoinFullOrderBookEntry>();
        /// <summary>
        /// The list of bids
        /// </summary>
        public IEnumerable<KucoinFullOrderBookEntry> Bids { get; set; } = Array.Empty<KucoinFullOrderBookEntry>();

        IEnumerable<ISymbolOrderBookEntry> ICommonOrderBook.CommonBids => Bids;
        IEnumerable<ISymbolOrderBookEntry> ICommonOrderBook.CommonAsks => Asks;
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
        [JsonProperty("ts"), JsonConverter(typeof(TimestampNanoSecondsConverter))]
        private DateTime TimestampNS
        {
            set => Timestamp = value;
        }

        /// <summary>
        /// Symbol
        /// </summary>
        public string? Symbol { get; set; }

        /// <summary>
        /// The list of asks
        /// </summary>
        public IEnumerable<KucoinOrderBookEntry> Asks { get; set; } = Array.Empty<KucoinOrderBookEntry>();
        /// <summary>
        /// The list of bids
        /// </summary>
        public IEnumerable<KucoinOrderBookEntry> Bids { get; set; } = Array.Empty<KucoinOrderBookEntry>();
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
        public string OrderId { get; set; } = string.Empty;
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
