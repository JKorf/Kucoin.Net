using CryptoExchange.Net.Converters;
using Kucoin.Net.Converts;
using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects.Sockets
{
    /// <summary>
    /// Match info
    /// </summary>
    public class KucoinStreamFuturesMatch: KucoinStreamMatchBase
    {
        /// <summary>
        /// Marer user id
        /// </summary>
        public string MakerUserId { get; set; } = string.Empty;
        /// <summary>
        /// Taker user id
        /// </summary>
        public string TakerUserId { get; set; } = string.Empty;
    }
}
