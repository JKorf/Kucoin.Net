using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Tick info
    /// </summary>
    [SerializationModel]
    public record KucoinTicks
    {
        /// <summary>
        /// The timestamp of the data
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// The ticker data
        /// </summary>
        [JsonPropertyName("ticker")]
        public KucoinAllTick[] Data { get; set; } = Array.Empty<KucoinAllTick>();
    }
}
