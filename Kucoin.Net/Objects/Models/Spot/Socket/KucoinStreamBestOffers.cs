using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Best offer info
    /// </summary>
    public record KucoinStreamBestOffers
    {
        /// <summary>
        /// Data timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// The current best ask
        /// </summary>
        [JsonProperty("asks")]
        public KucoinStreamOrderBookEntry BestAsk { get; set; } = null!;
        /// <summary>
        /// The current best bid
        /// </summary>
        [JsonProperty("bids")]
        public KucoinStreamOrderBookEntry BestBid { get; set; } = null!;
    }
}
