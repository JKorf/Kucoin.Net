
using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Symbols with open orders
    /// </summary>
    public record KucoinOpenOrderSymbols
    {
        /// <summary>
        /// Symbols with open orders
        /// </summary>
        [JsonPropertyName("symbols")]
        public IEnumerable<string> Symbols { get; set; } = Array.Empty<string>();
    }
}
