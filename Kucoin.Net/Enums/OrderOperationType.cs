using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Order operation type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderOperationType>))]
    public enum OrderOperationType
    {
        /// <summary>
        /// Matched
        /// </summary>
        [Map("DEAL")]
        Deal,
        /// <summary>
        /// Canceled
        /// </summary>
        [Map("CANCEL")]
        Cancel
    }
}
