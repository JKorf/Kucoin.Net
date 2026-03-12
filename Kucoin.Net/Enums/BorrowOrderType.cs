using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Borrow order type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<BorrowOrderType>))]
    public enum BorrowOrderType
    {
        /// <summary>
        /// ["<c>FOK</c>"] Fill or kill
        /// </summary>
        [Map("FOK")]
        FOK,
        /// <summary>
        /// ["<c>IOC</c>"] Immediate or cancel
        /// </summary>
        [Map("IOC")]
        IOC
    }
}
