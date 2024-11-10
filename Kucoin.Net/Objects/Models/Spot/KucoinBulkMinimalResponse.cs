using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// The order model in bulk order creation response
    /// </summary>
    public record KucoinBulkMinimalResponseEntry
    {
        /// <summary>
        /// The id of the order
        /// </summary>
        [JsonProperty("orderId")]
        public string? OrderId { get; set; }
        /// <summary>
        /// The cause of failure
        /// </summary>
        [JsonProperty("failMsg")]
        public string? Error { get; set; }
        /// <summary>
        /// Whether the call is successful
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
