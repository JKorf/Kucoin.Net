using CryptoExchange.Net.Converters.SystemTextJson;
using System;


namespace Kucoin.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Balance update
    /// </summary>
    [SerializationModel]
    public record KucoinStreamFuturesBalanceUpdate
    {
        /// <summary>
        /// Available balance
        /// </summary>
        [JsonPropertyName("availableBalance")]
        public decimal AvailableBalance { get; set; }
        /// <summary>
        /// Frozen balance
        /// </summary>
        [JsonPropertyName("holdBalance")]
        public decimal HoldBalance { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
