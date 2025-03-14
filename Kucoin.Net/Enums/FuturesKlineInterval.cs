using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Futures kline interval
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FuturesKlineInterval>))]
    public enum FuturesKlineInterval
    {
        /// <summary>
        /// One minute
        /// </summary>
        [Map("1")]
        OneMinute = 60,
        /// <summary>
        /// Five minute
        /// </summary>
        [Map("5")]
        FiveMinutes = 60 * 5,
        /// <summary>
        /// Fifteen minutes
        /// </summary>
        [Map("15")]
        FifteenMinutes = 60 * 15,
        /// <summary>
        /// Thirty minutes
        /// </summary>
        [Map("30")]
        ThirtyMinutes = 60 * 30,
        /// <summary>
        /// One hour
        /// </summary>
        [Map("60")]
        OneHour = 60 * 60,
        /// <summary>
        /// Two hours
        /// </summary>
        [Map("120")]
        TwoHours = 60 * 60 * 2,
        /// <summary>
        /// Four hours
        /// </summary>
        [Map("240")]
        FourHours = 60 * 60 * 4,
        /// <summary>
        /// Eight hours
        /// </summary>
        [Map("480")]
        EightHours = 60 * 60 * 8,
        /// <summary>
        /// Twelve hours
        /// </summary>
        [Map("720")]
        TwelveHours = 60 * 60 * 12,
        /// <summary>
        /// One day
        /// </summary>
        [Map("1440")]
        OneDay = 60 * 60 * 24,
        /// <summary>
        /// One week
        /// </summary>
        [Map("10080")]
        OneWeek = 60 * 60 * 24 * 7,
    }
}
