using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderStatus>))]
    public enum OrderStatus
    {
        /// <summary>
        /// ["<c>active</c>"] Order is active
        /// </summary>
        [Map("active", "open")]
        Active,
        /// <summary>
        /// ["<c>done</c>"] Order is done
        /// </summary>
        [Map("done")]
        Done
    }
}
