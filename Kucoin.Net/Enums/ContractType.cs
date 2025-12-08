using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Contract type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ContractType>))]
    public enum ContractType
    {
        /// <summary>
        /// Perpetual contract
        /// </summary>
        [Map("0")]
        Perpetual,
        /// <summary>
        /// Delivery futures contract
        /// </summary>
        [Map("1")]
        Delivery
    }
}
