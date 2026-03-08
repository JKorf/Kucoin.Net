using System;

namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Index info
    /// </summary>
    [SerializationModel]
    public record KucoinIndex: KucoinIndexBase
    {
        /// <summary>
        /// ["<c>decomposionList</c>"] Component list
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
        /// ["<c>exchange</c>"] Exchange
        /// </summary>
        [JsonPropertyName("exchange")]
        public string Exchange { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>weight</c>"] Weight
        /// </summary>
        [JsonPropertyName("weight")]
        public decimal Weight { get; set; }
    }
}
