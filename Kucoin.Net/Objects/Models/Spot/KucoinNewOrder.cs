using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// New order id
    /// </summary>
    public class KucoinNewOrder
    {
        /// <summary>
        /// The id of the new order
        /// </summary>
        [JsonProperty("orderId")]
        public string Id { get; set; } = string.Empty;
    }
}
