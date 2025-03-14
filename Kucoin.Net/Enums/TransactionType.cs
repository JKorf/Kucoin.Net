using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Realized profit and loss
        /// </summary>
        [Map("RealisedPNL")]
        RealizedPnl,
        /// <summary>
        /// Deposit
        /// </summary>
        [Map("Deposit")]
        Deposit,
        /// <summary>
        /// Withdrawal
        /// </summary>
        [Map("Withdrawal")]
        Withdrawal,
        /// <summary>
        /// Transfer in
        /// </summary>
        [Map("Transferin")]
        TransferIn,
        /// <summary>
        /// Transfer out
        /// </summary>
        [Map("Transferout")]
        TransferOut
    }
}
