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
        /// ["<c>canceled</c>"] Canceled
        /// </summary>
        [Map("canceled")]
        Canceled,
        /// <summary>
        /// ["<c>filled</c>"] Filled
        /// </summary>
        [Map("filled")]
        Filled
    }
}
