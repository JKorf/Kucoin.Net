using Newtonsoft.Json;

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
        [JsonProperty("orderId")]
        public string OrderId { get; set; } = string.Empty;
    }
}
