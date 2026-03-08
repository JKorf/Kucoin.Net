namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Max open size
    /// </summary>
    [SerializationModel]
    public record KucoinMaxOpenSize
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>maxBuyOpenSize</c>"] Max buy size
        /// </summary>
        [JsonPropertyName("maxBuyOpenSize")]
        public long MaxBuyOpenSize { get; set; }

        /// <summary>
        /// ["<c>maxSellOpenSize</c>"] Max sell size
        /// </summary>
        [JsonPropertyName("maxSellOpenSize")]
        public long MaxSellOpenSize { get; set; }
    }
}
