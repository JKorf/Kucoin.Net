using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Order info
    /// </summary>
    public record KucoinOrderHighFrequency : KucoinOrder
    {
        /// <summary>
        /// Whether the order is active
        /// </summary>
        [JsonProperty("active")]
        public override bool? IsActive { get; set; }
    }
}
