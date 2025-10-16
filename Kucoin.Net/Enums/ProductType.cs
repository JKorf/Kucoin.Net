using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Product type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ProductType>))]
    public enum ProductType
    {
        /// <summary>
        /// Spot
        /// </summary>
        [Map("SPOT")]
        Spot,
        /// <summary>
        /// Futures
        /// </summary>
        [Map("FUTURES")]
        Futures,
        /// <summary>
        /// Cross margin
        /// </summary>
        [Map("CROSS")]
        CrossMargin,
        /// <summary>
        /// Isolated margin
        /// </summary>
        [Map("ISOLATED")]
        IsolatedMargin
    }
}
