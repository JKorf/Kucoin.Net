using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ExtendedOrderStatus>))]
    public enum ExtendedOrderStatus
    {
        /// <summary>
        /// ["<c>new</c>"] New
        /// </summary>
        [Map("new")]
        New,
        /// <summary>
        /// ["<c>match</c>"] Match
        /// </summary>
        [Map("match")]
        Match,
        /// <summary>
        /// ["<c>open</c>"] Open
        /// </summary>
        [Map("open")]
        Open,
        /// <summary>
        /// ["<c>done</c>"] Done
        /// </summary>
        [Map("done")]
        Done
    }
}
