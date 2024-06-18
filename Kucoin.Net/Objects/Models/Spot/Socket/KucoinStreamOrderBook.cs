using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Order book info
    /// </summary>
    public record KucoinStreamOrderBook
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
        /// Data timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("time")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// The symbol of the order book
        /// </summary>
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// The changes
        /// </summary>
        public KucoinStreamOrderBookChanged Changes { get; set; } = default!;
    }

    /// <summary>
    /// Order book changes
    /// </summary>
    public record KucoinStreamOrderBookChanged
    {
        /// <summary>
        /// The changes in bids
        /// </summary>
        public IEnumerable<KucoinStreamOrderBookEntry> Bids { get; set; } = Array.Empty<KucoinStreamOrderBookEntry>();
        /// <summary>
        /// The changes in asks
        /// </summary>
        public IEnumerable<KucoinStreamOrderBookEntry> Asks { get; set; } = Array.Empty<KucoinStreamOrderBookEntry>();
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("timestamp"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Timestamp { get; set; }

        [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
        internal DateTime? Ts { set => Timestamp = value; }
    }

    /// <summary>
    /// Order book entry
    /// </summary>
    [JsonConverter(typeof(ArrayConverter))]
    public record KucoinStreamOrderBookEntry: ISymbolOrderSequencedBookEntry
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
