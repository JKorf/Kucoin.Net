using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Client order id
    /// </summary>
    public record KucoinClientOrderId
    {
        /// <summary>
        /// The client id of the order
        /// </summary>
        [JsonProperty("clientOid")]
        public string ClientOrderId { get; set; } = string.Empty;
    }
}
