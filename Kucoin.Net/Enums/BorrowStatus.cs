using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Status of a Borrow Order
    /// </summary>
    public enum BorrowStatus
    {
        /// <summary>
        /// In progress
        /// </summary>
        [Map("Processing")]
        Processing,
        /// <summary>
        /// Done 
        /// </summary>
        [Map("Done")]
        Done
    }
}
