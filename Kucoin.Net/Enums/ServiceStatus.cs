using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Service status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ServiceStatus>))]
    public enum ServiceStatus
    {
        /// <summary>
        /// Open
        /// </summary>
        [Map("open")]
        Open,
        /// <summary>
        /// Closed
        /// </summary>
        [Map("close")]
        Close,
        /// <summary>
        /// Only cancelation available
        /// </summary>
        [Map("cancelOnly")]
        CancelOnly
    }
}
