using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Canceled order
    /// </summary>
    public record KucoinCanceledOrder
    {
        /// <summary>
        /// Order id of the canceled order
        /// </summary>
        
        [JsonProperty("cancelledOrderId")]
        public string CanceledOrderId { get; set; } = string.Empty;
        /// <summary>
        /// Client order id of the canceled order
        /// </summary>
        [JsonProperty("clientOid")]
        public string ClientOrderId { get; set; } = string.Empty;
    }
}
