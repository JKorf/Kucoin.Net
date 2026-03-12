using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Order type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderType>))]
    public enum OrderType
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
        Market,
        /// <summary>
        /// ["<c>limit_stop</c>"] Limit stop order
        /// </summary>
        [Map("limit_stop")]
        LimitStop,
        /// <summary>
        /// ["<c>market_stop</c>"] Market stop order
        /// </summary>
        [Map("market_stop")]
        MarketStop,
        /// <summary>
        /// ["<c>stop</c>"] Stop order
        /// </summary>
        [Map("stop")]
        Stop
    }
}
