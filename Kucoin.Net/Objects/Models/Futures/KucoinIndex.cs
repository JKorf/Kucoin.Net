using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Index info
    /// </summary>
    [SerializationModel]
    public record KucoinIndex: KucoinIndexBase
    {
        /// <summary>
        /// Component list
        /// </summary>
        [JsonPropertyName("decomposionList")]
        public KucoinDecomposionItem[] DecomposionList { get; set; } = Array.Empty<KucoinDecomposionItem>();
    }

    /// <summary>
    /// Decomposion item
    /// </summary>
    [SerializationModel]
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
