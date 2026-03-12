using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Mode of Margin
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarginMode>))]
    public enum MarginMode
    {
        /// <summary>
        /// ["<c>CROSS</c>"] Cross Mode
        /// </summary>
        [Map("CROSS", "cross")]
        CrossMode,
        /// <summary>
        /// ["<c>ISOLATED</c>"] Isolated Mode, This mode is not supported by platform yet.
        /// </summary>
        [Map("ISOLATED", "isolated")]
        IsolatedMode,
    }
}
