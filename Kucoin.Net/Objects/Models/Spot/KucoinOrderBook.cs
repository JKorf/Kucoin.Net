using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Order book
    /// </summary>
    public record KucoinFullOrderBook
    {
        /// <summary>
        /// The last sequence number of this order book state
        /// </summary>
        public long Sequence { get; set; }
        /// <summary>
        /// The timestamp of the data
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(DateTimeConverter))]        
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// The list of asks
        /// </summary>
        public IEnumerable<KucoinFullOrderBookEntry> Asks { get; set; } = Array.Empty<KucoinFullOrderBookEntry>();
        /// <summary>
        /// The list of bids
        /// </summary>
        public IEnumerable<KucoinFullOrderBookEntry> Bids { get; set; } = Array.Empty<KucoinFullOrderBookEntry>();
    }

    /// <summary>
    /// Order book
    /// </summary>
    public record KucoinOrderBook
    {
        /// <summary>
        /// The last sequence number of this order book state
        /// </summary>
        public long? Sequence { get; set; }
        /// <summary>
        /// The timestamp of the data
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
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
    public record KucoinFullOrderBookEntry: ISymbolOrderBookEntry
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
    public record KucoinOrderBookEntry : ISymbolOrderBookEntry
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
