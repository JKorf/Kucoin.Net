using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Book depth
    /// </summary>
    [JsonConverter(typeof(JsonConverter<OrderBookDepth>))]
    public enum OrderBookDepth
    {
        /// <summary>
        /// Top 1 snapshots
        /// </summary>
        [Map("1")]
        BestBidOffer,
        /// <summary>
        /// Top 5 snapshots
        /// </summary>
        [Map("5")]
        Top5,
        /// <summary>
        /// Top 50 snapshots
        /// </summary>
        [Map("50")]
        Top50,
        /// <summary>
        /// Incremental full order book
        /// </summary>
        [Map("increment")]
        Incremental
    }
}
