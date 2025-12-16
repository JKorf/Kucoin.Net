using Kucoin.Net.Enums;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Service status
    /// </summary>
    public record KucoinUaServiceStatus
    {
        /// <summary>
        /// Product type
        /// </summary>
        [JsonPropertyName("tradeType")]
        public ProductType ProductType { get; set; }
        /// <summary>
        /// Server status
        /// </summary>
        [JsonPropertyName("serverStatus")]
        public ServiceStatus Status { get; set; }
        /// <summary>
        /// Msg
        /// </summary>
        [JsonPropertyName("msg")]
        public string? Message { get; set; }
    }


}
