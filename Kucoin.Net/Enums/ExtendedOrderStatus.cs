using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ExtendedOrderStatus>))]
    public enum ExtendedOrderStatus
    {
        /// <summary>
        /// New
        /// </summary>
        [Map("new")]
        New,
        /// <summary>
        /// Match
        /// </summary>
        [Map("match")]
        Match,
        /// <summary>
        /// Open
        /// </summary>
        [Map("open")]
        Open,
        /// <summary>
        /// Done
        /// </summary>
        [Map("done")]
        Done
    }
}
