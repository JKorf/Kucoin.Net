using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Converts;
using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects
{
    /// <summary>
    /// Sub transfer info
    /// </summary>
    public class KucoinInnerTransfer
    {
        /// <summary>
        /// The id of the new sub transfer
        /// </summary>
        public string OrderId { get; set; } = "";
    }
}
