using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
