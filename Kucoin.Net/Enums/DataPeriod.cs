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
        /// Five minutes
        /// </summary>
        [Map("5min")]
        FiveMinutes,
        /// <summary>
        /// Fifteen minutes
        /// </summary>
        [Map("15min")]
        FifteenMinutes,
        /// <summary>
        /// Thirty minutes
        /// </summary>
        [Map("30min")]
        ThirtyMinutes,
        /// <summary>
        /// One hour
        /// </summary>
        [Map("1hour")]
        OneHour,
        /// <summary>
        /// Four hours
        /// </summary>
        [Map("4hour")]
        FourHours,
        /// <summary>
        /// One day
        /// </summary>
        [Map("1day")]
        OneDay
    }
}
