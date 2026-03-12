using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Unified account type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<UnifiedAccountType>))]
    public enum UnifiedAccountType
    {
        /// <summary>
        /// ["<c>FUNDING</c>"] Funding account
        /// </summary>
        [Map("FUNDING")]
        Funding,
        /// <summary>
        /// ["<c>SPOT</c>"] Spot account
        /// </summary>
        [Map("SPOT")]
        Spot,
        /// <summary>
        /// ["<c>FUTURES</c>"] Futures account
        /// </summary>
        [Map("FUTURES")]
        Futures,
        /// <summary>
        /// ["<c>CROSS</c>"] Cross margin account
        /// </summary>
        [Map("CROSS")]
        Cross,
        /// <summary>
        /// ["<c>ISOLATED</c>"] Isolated margin account
        /// </summary>
        [Map("ISOLATED")]
        Isolated,

        /// <summary>
        /// ["<c>UNIFIED</c>"] Unified account
        /// </summary>
        [Map("UNIFIED")]
        Unified
    }
}
