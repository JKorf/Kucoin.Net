using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Symbols with open orders
    /// </summary>
    [SerializationModel]
    public record KucoinOpenOrderSymbols
    {
        /// <summary>
        /// Symbols with open orders
        /// </summary>
        [JsonPropertyName("symbols")]
        public string[] Symbols { get; set; } = Array.Empty<string>();
    }
}
