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
        /// ["<c>INTERNAL</c>"] Deduct the fee from the withdrawal amount
        /// </summary>
        [Map("INTERNAL")]
        Internal,
        /// <summary>
        /// ["<c>EXTERNAL</c>"] Deduct the fee from main account
        /// </summary>
        [Map("EXTERNAL")]
        External
    }
}
