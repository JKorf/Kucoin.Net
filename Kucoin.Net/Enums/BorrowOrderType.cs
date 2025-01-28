using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Borrow order type
    /// </summary>
    public enum BorrowOrderType
    {
        /// <summary>
        /// Fill or kill
        /// </summary>
        [Map("FOK")]
        FOK,
        /// <summary>
        /// Immediate or cancel
        /// </summary>
        [Map("IOC")]
        IOC
    }
}
