namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Leverage update
    /// </summary>
    [SerializationModel]
    public record KucoinLeverageUpdate
    {
        /// <summary>
        /// ["<c>leverage</c>"] Leverage value
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
    }
}
