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
        /// ["<c>withdrawHold</c>"] Current frozen quantity for withdrawal
        /// </summary>
        [JsonPropertyName("withdrawHold")]
        public decimal WithdrawHold { get; set; }
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
