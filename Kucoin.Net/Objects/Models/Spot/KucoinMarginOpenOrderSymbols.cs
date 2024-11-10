using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Symbols with open orders
    /// </summary>
    public record KucoinMarginOpenOrderSymbols
    {
        /// <summary>
        /// Number or symbols with active orders
        /// </summary>
        [JsonProperty("symbolSize")]
        public int ActiveOrderSymbols { get; set; }
        /// <summary>
        /// Symbols with open orders
        /// </summary>
        [JsonProperty("symbols")]
        public IEnumerable<string> Symbols { get; set; } = Array.Empty<string>();
    }
}
