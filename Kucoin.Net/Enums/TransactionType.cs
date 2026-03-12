using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Type of transaction
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TransactionType>))]
    public enum TransactionType
    {
        /// <summary>
        /// ["<c>RealisedPNL</c>"] Realized profit and loss
        /// </summary>
        [Map("RealisedPNL")]
        RealizedPnl,
        /// <summary>
        /// ["<c>Deposit</c>"] Deposit
        /// </summary>
        [Map("Deposit")]
        Deposit,
        /// <summary>
        /// ["<c>Withdrawal</c>"] Withdrawal
        /// </summary>
        [Map("Withdrawal")]
        Withdrawal,
        /// <summary>
        /// ["<c>Transferin</c>"] Transfer in
        /// </summary>
        [Map("Transferin")]
        TransferIn,
        /// <summary>
        /// ["<c>Transferout</c>"] Transfer out
        /// </summary>
        [Map("Transferout")]
        TransferOut
    }
}
