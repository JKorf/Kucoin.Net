using CryptoExchange.Net.Converters.SystemTextJson;
namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Leverage update
    /// </summary>
    [SerializationModel]
    public record KucoinLeverageUpdate
    {
        /// <summary>
        /// Leverage value
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
    }
}
