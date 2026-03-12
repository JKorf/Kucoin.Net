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
        /// ["<c>RECENTLY_EXPIRE_FIRST</c>"] Time priority, repay nearest maturity first
        /// </summary>
        [Map("RECENTLY_EXPIRE_FIRST")]
        RecentlyExpireFirst,
        /// <summary>
        /// ["<c>HIGHEST_RATE_FIRST</c>"] Rate priority, repay highest interest rate first
        /// </summary>
        [Map("HIGHEST_RATE_FIRST")]
        HighestRateFirst
    }
}
