using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Status of a Borrow Order
    /// </summary>
    [JsonConverter(typeof(EnumConverter<BorrowStatus>))]
    public enum BorrowStatus
    {
        /// <summary>
        /// ["<c>Processing</c>"] In progress
        /// </summary>
        [Map("Processing")]
        Processing,
        /// <summary>
        /// ["<c>Done</c>"] Done 
        /// </summary>
        [Map("Done")]
        Done
    }
}
