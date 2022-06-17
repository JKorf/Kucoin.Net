namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Status of a transaction
    /// </summary>
    public enum TransactionStatus : byte
    {
        /// <summary>
        /// In progress
        /// </summary>
        Processing = 0,
        /// <summary>
        /// Withdrawal in progress
        /// </summary>
        WalletProcessing = 1,
        /// <summary>
        /// Successful
        /// </summary>
        Success = 2,
        /// <summary>
        /// Failed
        /// </summary>
        Failure = 3
    }
}
