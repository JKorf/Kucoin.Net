using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Isolated position status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<IsolatedPositionStatus>))]
    public enum IsolatedPositionStatus
    {
        /// <summary>
        /// Debt
        /// </summary>
        [Map("DEBT")]
        Debt,
        /// <summary>
        /// Debt free
        /// </summary>
        [Map("CLEAR")]
        DebtFree,
        /// <summary>
        /// Borrowing
        /// </summary>
        [Map("IN_BORROW")]
        Borrowing,
        /// <summary>
        /// Repaying
        /// </summary>
        [Map("IN_REPAY")]
        Repaying,
        /// <summary>
        /// Liquidating
        /// </summary>
        [Map("IN_LIQUIDATION")]
        Liquidating,
        /// <summary>
        /// Auto renewing
        /// </summary>
        [Map("IN_AUTO_RENEW")]
        AutoRenewing
    }
}
