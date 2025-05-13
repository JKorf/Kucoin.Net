using CryptoExchange.Net.Converters.SystemTextJson;
namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Leverage info
    /// </summary>
    [SerializationModel]
    public record KucoinLeverage
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
    }


}
