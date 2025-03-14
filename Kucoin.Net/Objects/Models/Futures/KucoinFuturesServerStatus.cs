using CryptoExchange.Net.Converters.SystemTextJson;
using Kucoin.Net.Enums;


namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Service status
    /// </summary>
    [SerializationModel]
    public record KucoinFuturesServiceStatus
    {
        /// <summary>
        /// Service status
        /// </summary>
        [JsonPropertyName("status")]
        public ServiceStatus Status { get; set; }
        /// <summary>
        /// Info
        /// </summary>
        [JsonPropertyName("msg")]
        public string Message { get; set; } = string.Empty;
    }
}
