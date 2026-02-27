using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Order update type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<UnifiedOrderUpdateType>))]
    public enum UnifiedOrderUpdateType
    {
        /// <summary>
        /// Order created
        /// </summary>
        [Map("OPEN")]
        Open,
        /// <summary>
        /// Order updated
        /// </summary>
        [Map("UPDATE")]
        Update,
        /// <summary>
        /// Trade executed
        /// </summary>
        [Map("FILL")]
        Fill,
        /// <summary>
        /// Order canceled
        /// </summary>
        [Map("CANCEL")]
        Cancel,
        /// <summary>
        /// Conditional order triggered
        /// </summary>
        [Map("TRIGGER")]
        Trigger,
        /// <summary>
        /// Match
        /// </summary>
        [Map("MATCH")]
        Match
    }
}
