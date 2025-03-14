using CryptoExchange.Net.Converters.SystemTextJson;
using System;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Tick info
    /// </summary>
    [SerializationModel]
    public record KucoinTick
    {
        /// <summary>
        /// The sequence of the tick
        /// </summary>
        [JsonPropertyName("sequence")]
        public long Sequence { get; set; }
        /// <summary>
        /// The price of the last trade
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? LastPrice { get; set; }
        /// <summary>
        /// The quantity of the last trade
        /// </summary>
        [JsonPropertyName("size")]
        public decimal? LastQuantity { get; set; }
        /// <summary>
        /// The best ask price
        /// </summary>
        [JsonPropertyName("bestAsk")]
        public decimal? BestAskPrice { get; set; }
        /// <summary>
        /// The quantity of the best ask price
        /// </summary>
        [JsonPropertyName("bestAskSize")]
        public decimal? BestAskQuantity { get; set; }
        /// <summary>
        /// The best bid price
        /// </summary>
        [JsonPropertyName("bestBid")]
        public decimal? BestBidPrice { get; set; }
        /// <summary>
        /// The quantity of the best bid
        /// </summary>
        [JsonPropertyName("bestBidSize")]
        public decimal? BestBidQuantity { get; set; }
        /// <summary>
        /// The timestamp of the data
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
