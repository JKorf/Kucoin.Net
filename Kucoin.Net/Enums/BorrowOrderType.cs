using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
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
