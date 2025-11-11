using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Demand
        /// </summary>
        [Map("DEMAND")]
        Demand,

        /// <summary>
        /// Activity
        /// </summary>
        [Map("ACTIVITY")]
        Activity,

        /// <summary>
        /// Staking
        /// </summary>
        [Map("STAKING")]
        Staking,

        /// <summary>
        /// KCS Staking
        /// </summary>
        [Map("KCS_STAKING")]
        KCSStaking,

        /// <summary>
        /// ETH2 Staking
        /// </summary>
        [Map("ETH2")]
        ETH2
    }
}