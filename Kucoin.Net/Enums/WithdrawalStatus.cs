using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// In progress
        /// </summary>
        [Map("PROCESSING")]
        Processing,
        /// <summary>
        /// In progress
        /// </summary>
        [Map("WALLET_PROCESSING")]
        WalletProcessing,
        /// <summary>
        /// Successful
        /// </summary>
        [Map("SUCCESS")]
        Success,
        /// <summary>
        /// Failed
        /// </summary>
        [Map("FAILURE")]
        Failure
    }
}
