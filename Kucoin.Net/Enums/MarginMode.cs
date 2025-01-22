using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Mode of Margin
    /// </summary>
    public enum MarginMode
    {
        /// <summary>
        /// Cross Mode
        /// </summary>
        [Map("CROSS")]
        CrossMode,
        /// <summary>
        /// Isolated Mode, This mode is not supported by platform yet.
        /// </summary>
        [Map("ISOLATED")]
        IsolatedMode,
    }
}
