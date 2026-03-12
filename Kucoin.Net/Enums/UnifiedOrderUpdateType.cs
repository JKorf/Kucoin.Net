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
        /// ["<c>OPEN</c>"] Order created
        /// </summary>
        [Map("OPEN")]
        Open,
        /// <summary>
        /// ["<c>UPDATE</c>"] Order updated
        /// </summary>
        [Map("UPDATE")]
        Update,
        /// <summary>
        /// ["<c>FILL</c>"] Trade executed
        /// </summary>
        [Map("FILL")]
        Fill,
        /// <summary>
        /// ["<c>CANCEL</c>"] Order canceled
        /// </summary>
        [Map("CANCEL")]
        Cancel,
        /// <summary>
        /// ["<c>TRIGGER</c>"] Conditional order triggered
        /// </summary>
        [Map("TRIGGER")]
        Trigger,
        /// <summary>
        /// ["<c>MATCH</c>"] Match
        /// </summary>
        [Map("MATCH")]
        Match
    }
}
