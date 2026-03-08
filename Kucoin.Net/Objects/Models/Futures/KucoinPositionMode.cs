using Kucoin.Net.Enums;

namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Position mode
    /// </summary>
    public record KucoinPositionMode
    {
        /// <summary>
        /// ["<c>positionMode</c>"] Position mode
        /// </summary>
        [JsonPropertyName("positionMode")]
        public PositionMode PositionMode { get; set; }
    }
}
