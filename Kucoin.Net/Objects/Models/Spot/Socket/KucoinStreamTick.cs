namespace Kucoin.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Stream tick
    /// </summary>
    [SerializationModel]
    public record KucoinStreamTick: KucoinTick
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
    }
}
