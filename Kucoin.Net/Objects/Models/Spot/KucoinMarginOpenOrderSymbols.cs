using CryptoExchange.Net.Converters.SystemTextJson;

using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Symbols with open orders
    /// </summary>
    [SerializationModel]
    public record KucoinMarginOpenOrderSymbols
    {
        /// <summary>
        /// Number or symbols with active orders
        /// </summary>
        [JsonPropertyName("symbolSize")]
        public int ActiveOrderSymbols { get; set; }
        /// <summary>
        /// Symbols with open orders
        /// </summary>
        [JsonPropertyName("symbols")]
        public string[] Symbols { get; set; } = Array.Empty<string>();
    }
}
