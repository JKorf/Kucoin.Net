using System;


namespace Kucoin.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Best offer info
    /// </summary>
    [SerializationModel]
    public record KucoinStreamBestOffers
    {
        /// <summary>
        /// ["<c>timestamp</c>"] Data timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// ["<c>asks</c>"] The current best ask
        /// </summary>
        [JsonPropertyName("asks")]
        public KucoinStreamOrderBookEntry BestAsk { get; set; } = null!;
        /// <summary>
        /// ["<c>bids</c>"] The current best bid
        /// </summary>
        [JsonPropertyName("bids")]
        public KucoinStreamOrderBookEntry BestBid { get; set; } = null!;
    }
}
