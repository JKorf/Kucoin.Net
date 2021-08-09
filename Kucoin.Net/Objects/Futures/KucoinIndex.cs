using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Futures
{
    /// <summary>
    /// Index info
    /// </summary>
    public class KucoinIndex: KucoinIndexBase
    {
        /// <summary>
        /// Component list
        /// </summary>
        public IEnumerable<KucoinDecomposionItem> DecomposionList { get; set; } = Array.Empty<KucoinDecomposionItem>();
    }

    /// <summary>
    /// Decomposion item
    /// </summary>
    public class KucoinDecomposionItem
    {
        /// <summary>
        /// Exchange
        /// </summary>
        public string Exchange { get; set; } = string.Empty;
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Weight
        /// </summary>
        public decimal Weight { get; set; }
    }
}
