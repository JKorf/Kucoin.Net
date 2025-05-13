using CryptoExchange.Net.Converters.SystemTextJson;
namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Max open size
    /// </summary>
    [SerializationModel]
    public record KucoinMaxOpenSize
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Max buy size
        /// </summary>
        [JsonPropertyName("maxBuyOpenSize")]
        public long MaxBuyOpenSize { get; set; }

        /// <summary>
        /// Max sell size
        /// </summary>
        [JsonPropertyName("maxSellOpenSize")]
        public long MaxSellOpenSize { get; set; }
    }
}
