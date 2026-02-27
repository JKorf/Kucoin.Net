using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Account mode
    /// </summary>
    [JsonConverter(typeof(EnumConverter<UnifiedAccountMode>))]
    public enum UnifiedAccountMode
    {
        /// <summary>
        /// Classic
        /// </summary>
        [Map("CLASSIC")]
        Classic,
        /// <summary>
        /// Unified
        /// </summary>
        [Map("UNIFIED")]
        Unified
    }
}
