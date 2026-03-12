using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Category of an earn product
    /// </summary>
    [JsonConverter(typeof(EnumConverter<EarnProductCategory>))]
    public enum EarnProductCategory
    {
        /// <summary>
        /// ["<c>DEMAND</c>"] Demand
        /// </summary>
        [Map("DEMAND")]
        Demand,

        /// <summary>
        /// ["<c>ACTIVITY</c>"] Activity
        /// </summary>
        [Map("ACTIVITY")]
        Activity,

        /// <summary>
        /// ["<c>STAKING</c>"] Staking
        /// </summary>
        [Map("STAKING")]
        Staking,

        /// <summary>
        /// ["<c>KCS_STAKING</c>"] KCS Staking
        /// </summary>
        [Map("KCS_STAKING")]
        KCSStaking,

        /// <summary>
        /// ["<c>ETH2</c>"] ETH2 Staking
        /// </summary>
        [Map("ETH2")]
        ETH2
    }
}
