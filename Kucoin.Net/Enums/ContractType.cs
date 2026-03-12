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
        /// ["<c>0</c>"] Perpetual contract
        /// </summary>
        [Map("0")]
        Perpetual,
        /// <summary>
        /// ["<c>1</c>"] Delivery futures contract
        /// </summary>
        [Map("1")]
        Delivery
    }
}
