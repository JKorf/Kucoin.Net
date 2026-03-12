using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Direction
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AccountDirection>))]
    public enum AccountDirection
    {
        /// <summary>
        /// ["<c>in</c>"] In
        /// </summary>
        [Map("in")]
        In,
        /// <summary>
        /// ["<c>out</c>"] Out
        /// </summary>
        [Map("out")]
        Out
    }
}
