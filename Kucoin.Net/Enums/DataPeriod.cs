using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Data period
    /// </summary>
    [JsonConverter(typeof(EnumConverter<DataPeriod>))]
    public enum DataPeriod
    {
        /// <summary>
        /// ["<c>5min</c>"] Five minutes
        /// </summary>
        [Map("5min")]
        FiveMinutes,
        /// <summary>
        /// ["<c>15min</c>"] Fifteen minutes
        /// </summary>
        [Map("15min")]
        FifteenMinutes,
        /// <summary>
        /// ["<c>30min</c>"] Thirty minutes
        /// </summary>
        [Map("30min")]
        ThirtyMinutes,
        /// <summary>
        /// ["<c>1hour</c>"] One hour
        /// </summary>
        [Map("1hour")]
        OneHour,
        /// <summary>
        /// ["<c>4hour</c>"] Four hours
        /// </summary>
        [Map("4hour")]
        FourHours,
        /// <summary>
        /// ["<c>1day</c>"] One day
        /// </summary>
        [Map("1day")]
        OneDay
    }
}
