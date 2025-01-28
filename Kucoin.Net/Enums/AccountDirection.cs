using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Direction
    /// </summary>
    public enum AccountDirection
    {
        /// <summary>
        /// In
        /// </summary>
        [Map("in")]
        In,
        /// <summary>
        /// Out
        /// </summary>
        [Map("out")]
        Out
    }
}
