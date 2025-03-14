using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Withdrawal fee deduction type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FeeDeductType>))]
    public enum FeeDeductType
    {
        /// <summary>
        /// Deduct the fee from the withdrawal amount
        /// </summary>
        [Map("INTERNAL")]
        Internal,
        /// <summary>
        /// Deduct the fee from main account
        /// </summary>
        [Map("EXTERNAL")]
        External
    }
}
