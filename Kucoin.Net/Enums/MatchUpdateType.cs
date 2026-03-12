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
        /// ["<c>received</c>"] Received
        /// </summary>
        [Map("received")]
        Received,
        /// <summary>
        /// ["<c>open</c>"] Open
        /// </summary>
        [Map("open")]
        Open,
        /// <summary>
        /// ["<c>match</c>"] Match
        /// </summary>
        [Map("match")]
        Match,
        /// <summary>
        /// ["<c>filled</c>"] Filled
        /// </summary>
        [Map("filled")]
        Filled,
        /// <summary>
        /// ["<c>canceled</c>"] Canceled
        /// </summary>
        [Map("canceled")]
        Canceled,
        /// <summary>
        /// ["<c>update</c>"] Update
        /// </summary>
        [Map("update")]
        Update
    }
}
