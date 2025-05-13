using CryptoExchange.Net.Converters.SystemTextJson;
using System;


namespace Kucoin.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// 24 Hour statistics update
    /// </summary>
    [SerializationModel]
    public record KucoinStreamTransactionStatisticsUpdate
    {
        /// <summary>
        /// Volume
        /// </summary>
        [JsonPropertyName("volume")]
        public decimal Volume { get; set; }
        /// <summary>
        /// Turnover
        /// </summary>
        [JsonPropertyName("turnover")]
        public decimal Turnover { get; set; }
        /// <summary>
        /// Last price
        /// </summary>
        [JsonPropertyName("lastPrice")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// Price change percentage
        /// </summary>
        [JsonPropertyName("priceChgPct")]
        public decimal PriceChangePercentage { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
