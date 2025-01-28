

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
        [JsonPropertyName("orderNo")]
        public string OrderId { get; set; } = string.Empty;
    }
}
