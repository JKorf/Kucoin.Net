using Kucoin.Net.Enums;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Service status
    /// </summary>
    public record KucoinUaServiceStatus
    {
        /// <summary>
        /// ["<c>tradeType</c>"] Product type
        /// </summary>
        [JsonPropertyName("tradeType")]
        public ProductType ProductType { get; set; }
        /// <summary>
        /// ["<c>serverStatus</c>"] Server status
        /// </summary>
        [JsonPropertyName("serverStatus")]
        public ServiceStatus Status { get; set; }
        /// <summary>
        /// ["<c>msg</c>"] Msg
        /// </summary>
        [JsonPropertyName("msg")]
        public string? Message { get; set; }
    }


}
