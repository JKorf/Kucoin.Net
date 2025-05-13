using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Limit order
        /// </summary>
        [Map("limit")]
        Limit,
        /// <summary>
        /// Market order
        /// </summary>
        [Map("market")]
        Market,
        /// <summary>
        /// Limit stop order
        /// </summary>
        [Map("limit_stop")]
        LimitStop,
        /// <summary>
        /// Market stop order
        /// </summary>
        [Map("market_stop")]
        MarketStop,
        /// <summary>
        /// Stop order
        /// </summary>
        [Map("stop")]
        Stop
    }
}
