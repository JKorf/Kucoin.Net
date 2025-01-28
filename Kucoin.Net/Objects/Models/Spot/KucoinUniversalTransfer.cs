

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Universal transfer
    /// </summary>
    public record KucoinUniversalTransfer
    {
        /// <summary>
        /// Orrder id
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
    }
}
