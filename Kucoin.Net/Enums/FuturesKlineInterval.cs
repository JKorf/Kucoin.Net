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
        /// ["<c>1</c>"] One minute
        /// </summary>
        [Map("1")]
        OneMinute = 60,
        /// <summary>
        /// ["<c>5</c>"] Five minute
        /// </summary>
        [Map("5")]
        FiveMinutes = 60 * 5,
        /// <summary>
        /// ["<c>15</c>"] Fifteen minutes
        /// </summary>
        [Map("15")]
        FifteenMinutes = 60 * 15,
        /// <summary>
        /// ["<c>30</c>"] Thirty minutes
        /// </summary>
        [Map("30")]
        ThirtyMinutes = 60 * 30,
        /// <summary>
        /// ["<c>60</c>"] One hour
        /// </summary>
        [Map("60")]
        OneHour = 60 * 60,
        /// <summary>
        /// ["<c>120</c>"] Two hours
        /// </summary>
        [Map("120")]
        TwoHours = 60 * 60 * 2,
        /// <summary>
        /// ["<c>240</c>"] Four hours
        /// </summary>
        [Map("240")]
        FourHours = 60 * 60 * 4,
        /// <summary>
        /// ["<c>480</c>"] Eight hours
        /// </summary>
        [Map("480")]
        EightHours = 60 * 60 * 8,
        /// <summary>
        /// ["<c>720</c>"] Twelve hours
        /// </summary>
        [Map("720")]
        TwelveHours = 60 * 60 * 12,
        /// <summary>
        /// ["<c>1440</c>"] One day
        /// </summary>
        [Map("1440")]
        OneDay = 60 * 60 * 24,
        /// <summary>
        /// ["<c>10080</c>"] One week
        /// </summary>
        [Map("10080")]
        OneWeek = 60 * 60 * 24 * 7,
    }
}
