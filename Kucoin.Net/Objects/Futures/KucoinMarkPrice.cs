using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kucoin.Net.Objects.Futures
{
    /// <summary>
    /// Mark price
    /// </summary>
    public class KucoinMarkPrice: KucoinIndexBase
    {        
        /// <summary>
        /// Index price
        /// </summary>
        public decimal IndexPrice { get; set; }
    }
}
