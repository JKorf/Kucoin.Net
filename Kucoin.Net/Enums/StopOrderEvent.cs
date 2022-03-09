using System;
using System.Collections.Generic;
using System.Text;
using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Stop order event
    /// </summary>
    [JsonConverter(typeof(EnumConverter))]
    public enum StopOrderEvent
    {
        /// <summary>
        /// Stop order opened
        /// </summary>
        [Map("open")]
        Open,
        /// <summary>
        /// Stop order triggered by price
        /// </summary>
        [Map("triggered")]
        Triggered,
        /// <summary>
        /// Stop order canceled
        /// </summary>
        [Map("cancel")]
        Canceled
    }
}
