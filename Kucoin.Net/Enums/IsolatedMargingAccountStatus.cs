using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Isolated margin account status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<IsolatedMargingAccountStatus>))]
    public enum IsolatedMargingAccountStatus
    {
        /// <summary>
        /// ["<c>DEBT</c>"] Existing liabilities
        /// </summary>
        [Map("DEBT")]
        Debt,
        /// <summary>
        /// ["<c>CLEAR</c>"] No liabilities
        /// </summary>
        [Map("CLEAR")]
        Clear,
        /// <summary>
        /// ["<c>BANKRUPTCY</c>"] Bankruptcy (after position enters a negative balance
        /// </summary>
        [Map("BANKRUPTCY")]
        Bankruptcy,
        /// <summary>
        /// ["<c>IN_BORROW</c>"] Existing borrowings
        /// </summary>
        [Map("IN_BORROW")]
        InBorrow,
        /// <summary>
        /// ["<c>IN_REPAY</c>"] Existing repayments
        /// </summary>
        [Map("IN_REPAY")]
        InRepay,
        /// <summary>
        /// ["<c>IN_LIQUIDATION</c>"] Under liquidation
        /// </summary>
        [Map("IN_LIQUIDATION")]
        InLiquidation,
        /// <summary>
        /// ["<c>IN_AUTO_RENEW </c>"] Under auto-renewal assets
        /// </summary>
        [Map("IN_AUTO_RENEW ")]
        InAutoRenew
    }
}
