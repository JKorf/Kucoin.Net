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
        /// ["<c>status</c>"] Service status
        /// </summary>
        [JsonPropertyName("status")]
        public ServiceStatus Status { get; set; }
        /// <summary>
        /// ["<c>msg</c>"] Info
        /// </summary>
        [JsonPropertyName("msg")]
        public string Message { get; set; } = string.Empty;
    }
}
