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
        /// Margin margin account
        /// </summary>
        [Map("MARGIN")]
        Margin,
    }
}
