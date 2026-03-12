using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Withdrawal type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<WithdrawType>))]
    public enum WithdrawType
    {
        /// <summary>
        /// ["<c>ADDRESS</c>"] Withdraw to crypto address
        /// </summary>
        [Map("ADDRESS")]
        Address,
        /// <summary>
        /// ["<c>UID</c>"] Withdraw to user by user id
        /// </summary>
        [Map("UID")]
        Uid,
        /// <summary>
        /// ["<c>MAIL</c>"] Withdraw to user by email
        /// </summary>
        [Map("MAIL")]
        Mail,
        /// <summary>
        /// ["<c>Phone</c>"] Withdraw to user by phone
        /// </summary>
        [Map("Phone")]
        Phone
    }
}
