using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Symbols with open orders
    /// </summary>
    [SerializationModel]
    public record KucoinMarginOpenOrderSymbols
    {
        /// <summary>
        /// ["<c>symbolSize</c>"] Number or symbols with active orders
        /// </summary>
        [JsonPropertyName("symbolSize")]
        public int ActiveOrderSymbols { get; set; }
        /// <summary>
        /// ["<c>symbols</c>"] Symbols with open orders
        /// </summary>
        [JsonPropertyName("symbols")]
        public string[] Symbols { get; set; } = Array.Empty<string>();
    }
}
