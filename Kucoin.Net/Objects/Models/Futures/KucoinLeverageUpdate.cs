namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Leverage update
    /// </summary>
    public record KucoinLeverageUpdate
    {
        /// <summary>
        /// Leverage value
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
    }
}
