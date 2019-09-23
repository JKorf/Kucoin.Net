﻿using CryptoExchange.Net.Converters;
using CryptoExchange.Net.OrderBook;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Sockets
{
    /// <summary>
    /// Order book info
    /// </summary>
    public class KucoinStreamOrderBook
    {
        /// <summary>
        /// The sequence id of the first event this order book update covers
        /// </summary>
        public long SequenceStart { get; set; }
        /// <summary>
        /// The sequence id of the last event this order book update covers
        /// </summary>
        public long SequenceEnd { get; set; }
        /// <summary>
        /// The symbol of the order book
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// The changes
        /// </summary>
        public KucoinStreamOrderBookChanged Changes { get; set; }
    }

    /// <summary>
    /// Order book changes
    /// </summary>
    public class KucoinStreamOrderBookChanged
    {
        /// <summary>
        /// The changes in bids
        /// </summary>
        public KucoinStreamOrderBookEntry[] Bids { get; set; }
        /// <summary>
        /// The changes in asks
        /// </summary>
        public KucoinStreamOrderBookEntry[] Asks { get; set; }
    }

    /// <summary>
    /// Order book entry
    /// </summary>
    [JsonConverter(typeof(ArrayConverter))]
    public class KucoinStreamOrderBookEntry: ISymbolOrderBookEntry
    {
        /// <summary>
        /// The price of the change
        /// </summary>
        [ArrayProperty(0)]
        public decimal Price { get; set; }
        /// <summary>
        /// The quantity of the change
        /// </summary>
        [ArrayProperty(1)]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The sequence of the change
        /// </summary>
        [ArrayProperty(2)]
        public long Sequence { get; set; }
    }
}
