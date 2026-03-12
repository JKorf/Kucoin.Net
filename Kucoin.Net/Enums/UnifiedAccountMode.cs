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
        /// ["<c>CLASSIC</c>"] Classic
        /// </summary>
        [Map("CLASSIC")]
        Classic,
        /// <summary>
        /// ["<c>UNIFIED</c>"] Unified
        /// </summary>
        [Map("UNIFIED")]
        Unified
    }
}
