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
        /// ["<c>DEBT</c>"] Debt
        /// </summary>
        [Map("DEBT")]
        Debt,
        /// <summary>
        /// ["<c>CLEAR</c>"] Debt free
        /// </summary>
        [Map("CLEAR")]
        DebtFree,
        /// <summary>
        /// ["<c>IN_BORROW</c>"] Borrowing
        /// </summary>
        [Map("IN_BORROW")]
        Borrowing,
        /// <summary>
        /// ["<c>IN_REPAY</c>"] Repaying
        /// </summary>
        [Map("IN_REPAY")]
        Repaying,
        /// <summary>
        /// ["<c>IN_LIQUIDATION</c>"] Liquidating
        /// </summary>
        [Map("IN_LIQUIDATION")]
        Liquidating,
        /// <summary>
        /// ["<c>IN_AUTO_RENEW</c>"] Auto renewing
        /// </summary>
        [Map("IN_AUTO_RENEW")]
        AutoRenewing
    }
}
