using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Unified account type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<UnifiedSimpleAccountType>))]
    public enum UnifiedSimpleAccountType
    {
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
        /// ["<c>MARGIN</c>"] Margin margin account
        /// </summary>
        [Map("MARGIN")]
        Margin,
    }
}
