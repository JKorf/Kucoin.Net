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
