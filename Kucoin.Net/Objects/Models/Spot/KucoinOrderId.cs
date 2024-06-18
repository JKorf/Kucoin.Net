using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Order id
    /// </summary>
    public record KucoinOrderId
    {
        /// <summary>
        /// The id of the order
        /// </summary>
        [JsonProperty("orderId")]
        public string Id { get; set; } = string.Empty;
    }
}
