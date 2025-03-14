using CryptoExchange.Net.Converters.SystemTextJson;
using System;


namespace Kucoin.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Update to funds wich are withdrawable
    /// </summary>
    [SerializationModel]
    public record KucoinStreamFuturesWithdrawableUpdate
    {
        /// <summary>
        /// Current frozen quantity for withdrawal
        /// </summary>
        [JsonPropertyName("withdrawHold")]
        public decimal WithdrawHold { get; set; }
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
