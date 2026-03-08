namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Lending result
    /// </summary>
    [SerializationModel]
    public record KucoinLendingResult
    {
        /// <summary>
        /// ["<c>orderNo</c>"] Order number
        /// </summary>
        [JsonPropertyName("orderNo")]
        public string OrderId { get; set; } = string.Empty;
    }
}
