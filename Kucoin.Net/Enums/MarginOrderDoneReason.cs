using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Margin order done reason
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarginOrderDoneReason>))]
    public enum MarginOrderDoneReason
    {
        /// <summary>
        /// Filled
        /// </summary>
        [Map("filled")]
        Filled,
        /// <summary>
        /// Cancelled
        /// </summary>
        [Map("canceled")]
        Canceled
    }
}
