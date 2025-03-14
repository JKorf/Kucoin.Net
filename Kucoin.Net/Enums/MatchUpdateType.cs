using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Match update type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MatchUpdateType>))]
    public enum MatchUpdateType
    {
        /// <summary>
        /// Received
        /// </summary>
        [Map("received")]
        Received,
        /// <summary>
        /// Open
        /// </summary>
        [Map("open")]
        Open,
        /// <summary>
        /// Match
        /// </summary>
        [Map("match")]
        Match,
        /// <summary>
        /// Filled
        /// </summary>
        [Map("filled")]
        Filled,
        /// <summary>
        /// Canceled
        /// </summary>
        [Map("canceled")]
        Canceled,
        /// <summary>
        /// Update
        /// </summary>
        [Map("update")]
        Update
    }
}
