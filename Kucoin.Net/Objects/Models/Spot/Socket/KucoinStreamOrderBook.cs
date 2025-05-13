using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Kucoin.Net.Converters;


namespace Kucoin.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Order book info
    /// </summary>
    [SerializationModel]
    public record KucoinStreamOrderBook
    {
        /// <summary>
        /// The sequence id of the first event this order book update covers
        /// </summary>
        [JsonPropertyName("sequenceStart")]
        public long SequenceStart { get; set; }
        /// <summary>
        /// The sequence id of the last event this order book update covers
        /// </summary>
        [JsonPropertyName("sequenceEnd")]
        public long SequenceEnd { get; set; }

        /// <summary>
        /// Data timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// The symbol of the order book
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// The changes
        /// </summary>
        [JsonPropertyName("changes")]
        public KucoinStreamOrderBookChanged Changes { get; set; } = default!;
    }

    /// <summary>
    /// Order book changes
    /// </summary>
    [SerializationModel]
    public record KucoinStreamOrderBookChanged
    {
        /// <summary>
        /// The changes in bids
        /// </summary>
        [JsonPropertyName("bids")]
        public KucoinStreamOrderBookEntry[] Bids { get; set; } = Array.Empty<KucoinStreamOrderBookEntry>();
        /// <summary>
        /// The changes in asks
        /// </summary>
        [JsonPropertyName("asks")]
        public KucoinStreamOrderBookEntry[] Asks { get; set; } = Array.Empty<KucoinStreamOrderBookEntry>();
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("timestamp"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Timestamp { get; set; }

        [JsonInclude, JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
        internal DateTime? Ts { set => Timestamp = value; get => Timestamp; }
    }

    /// <summary>
    /// Order book entry
    /// </summary>
    [JsonConverter(typeof(ArrayConverter<KucoinStreamOrderBookEntry>))]
    [SerializationModel]
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
