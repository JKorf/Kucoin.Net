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
        /// Funding account
        /// </summary>
        [Map("FUNDING")]
        Funding,
        /// <summary>
        /// Spot account
        /// </summary>
        [Map("SPOT")]
        Spot,
        /// <summary>
        /// Futures account
        /// </summary>
        [Map("FUTURES")]
        Futures,
        /// <summary>
        /// Cross margin account
        /// </summary>
        [Map("CROSS")]
        Cross,
        /// <summary>
        /// Isolated margin account
        /// </summary>
        [Map("ISOLATED")]
        Isolated,

        /// <summary>
        /// Unified account
        /// </summary>
        [Map("UNIFIED")]
        Unified
    }
}
