using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Lending result
    /// </summary>
    public record KucoinLendingResult
    {
        /// <summary>
        /// Order number
        /// </summary>
        [JsonProperty("orderNo")]
        public string OrderId { get; set; } = string.Empty;
    }
}
