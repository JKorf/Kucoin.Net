using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderStatus>))]
    public enum OrderStatus
    {
        /// <summary>
        /// Order is active
        /// </summary>
        [Map("active", "open")]
        Active,
        /// <summary>
        /// Order is done
        /// </summary>
        [Map("done")]
        Done
    }
}
