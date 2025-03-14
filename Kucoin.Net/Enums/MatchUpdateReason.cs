using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Reason for an update
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MatchUpdateReason>))]
    public enum MatchUpdateReason
    {
        /// <summary>
        /// Canceled
        /// </summary>
        [Map("canceled")]
        Canceled,
        /// <summary>
        /// Filled
        /// </summary>
        [Map("filled")]
        Filled
    }
}
