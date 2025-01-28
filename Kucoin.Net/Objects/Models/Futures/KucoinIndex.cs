using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Index info
    /// </summary>
    public record KucoinIndex: KucoinIndexBase
    {
        /// <summary>
        /// Component list
        /// </summary>
        [JsonPropertyName("decomposionList")]
        public IEnumerable<KucoinDecomposionItem> DecomposionList { get; set; } = Array.Empty<KucoinDecomposionItem>();
    }

    /// <summary>
    /// Decomposion item
    /// </summary>
    public record KucoinDecomposionItem
    {
        /// <summary>
        /// Exchange
        /// </summary>
        [JsonPropertyName("exchange")]
        public string Exchange { get; set; } = string.Empty;
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Weight
        /// </summary>
        [JsonPropertyName("weight")]
        public decimal Weight { get; set; }
    }
}
