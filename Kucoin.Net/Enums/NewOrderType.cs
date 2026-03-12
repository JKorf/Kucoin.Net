using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// New order type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<NewOrderType>))]
    public enum NewOrderType
    {
        /// <summary>
        /// ["<c>limit</c>"] Limit order
        /// </summary>
        [Map("limit")]
        Limit,
        /// <summary>
        /// ["<c>market</c>"] Market order
        /// </summary>
        [Map("market")]
        Market
    }
}
