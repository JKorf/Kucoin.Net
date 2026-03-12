using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Status of a withdrawal
    /// </summary>
    [JsonConverter(typeof(EnumConverter<WithdrawalStatus>))]
    public enum WithdrawalStatus
    {
        /// <summary>
        /// ["<c>PROCESSING</c>"] In progress
        /// </summary>
        [Map("PROCESSING")]
        Processing,
        /// <summary>
        /// ["<c>WALLET_PROCESSING</c>"] In progress
        /// </summary>
        [Map("WALLET_PROCESSING")]
        WalletProcessing,
        /// <summary>
        /// ["<c>SUCCESS</c>"] Successful
        /// </summary>
        [Map("SUCCESS")]
        Success,
        /// <summary>
        /// ["<c>FAILURE</c>"] Failed
        /// </summary>
        [Map("FAILURE")]
        Failure
    }
}
