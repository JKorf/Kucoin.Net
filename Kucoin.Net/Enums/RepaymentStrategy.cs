using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Repayment strategy
    /// </summary>
    [JsonConverter(typeof(EnumConverter<RepaymentStrategy>))]
    public enum RepaymentStrategy
    {
        /// <summary>
        /// Time priority, repay nearest maturity first
        /// </summary>
        [Map("RECENTLY_EXPIRE_FIRST")]
        RecentlyExpireFirst,
        /// <summary>
        /// Rate priority, repay highest interest rate first
        /// </summary>
        [Map("HIGHEST_RATE_FIRST")]
        HighestRateFirst
    }
}
