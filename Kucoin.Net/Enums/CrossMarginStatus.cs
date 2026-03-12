using CryptoExchange.Net.Attributes;


namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Cross margin status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<CrossMarginStatus>))]
    public enum CrossMarginStatus
    {
        /// <summary>
        /// ["<c>EFFECTIVE</c>"] Effective
        /// </summary>
        [Map("EFFECTIVE")]
        Effective,
        /// <summary>
        /// ["<c>BANKRUPTCY</c>"] Bankruptcy liquidation
        /// </summary>
        [Map("BANKRUPTCY")]
        BankruptcyLiquidation,
        /// <summary>
        /// ["<c>LIQUIDATION</c>"] Closing
        /// </summary>
        [Map("LIQUIDATION")]
        Closing,
        /// <summary>
        /// ["<c>REPAY</c>"] Repaying
        /// </summary>
        [Map("REPAY")]
        Repaying,
        /// <summary>
        /// ["<c>BORROW</c>"] Borrowing
        /// </summary>
        [Map("BORROW")]
        Borrowing
    }
}
