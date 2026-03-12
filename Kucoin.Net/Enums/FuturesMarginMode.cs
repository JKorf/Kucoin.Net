using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FuturesMarginMode>))]
    public enum FuturesMarginMode
    {
        /// <summary>
        /// ["<c>CROSS</c>"] Cross margin
        /// </summary>
        [Map("CROSS")]
        Cross,
        /// <summary>
        /// ["<c>ISOLATED</c>"] Isolated margin
        /// </summary>
        [Map("ISOLATED")]
        Isolated,
    }
}
