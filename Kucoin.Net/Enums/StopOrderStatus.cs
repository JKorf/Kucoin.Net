using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Stop order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<StopOrderStatus>))]
    public enum StopOrderStatus
    {
        /// <summary>
        /// New
        /// </summary>
        [Map("NEW")]
        New,
        /// <summary>
        /// Triggered
        /// </summary>
        [Map("TRIGGERED")]
        Triggered
    }
}
