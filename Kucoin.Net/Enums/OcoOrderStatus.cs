using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// OCO order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OcoOrderStatus>))]
    public enum OcoOrderStatus
    {
        /// <summary>
        /// New
        /// </summary>
        [Map("NEW")]
        New,
        /// <summary>
        /// Done
        /// </summary>
        [Map("DONE")]
        Done,
        /// <summary>
        /// Triggered
        /// </summary>
        [Map("TRIGGERED")]
        Triggered,
        /// <summary>
        /// Cancelled
        /// </summary>
        [Map("CANCELLED")]
        Canceled
    }
}
