namespace Kucoin.Net.Objects
{
    /// <summary>
    /// Currency info
    /// </summary>
    public class KucoinCurrency
    {
        /// <summary>
        /// The currency identifier
        /// </summary>
        public string Currency { get; set; } = string.Empty;
        /// <summary>
        /// The name of the currency
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The full name of the currency
        /// </summary>
        public string FullName { get; set; } = string.Empty;
        /// <summary>
        /// The precision of the currency
        /// </summary>
        public int Precision { get; set; }
        /// <summary>
        /// The minimum size of a withdrawal
        /// </summary>
        public decimal WithdrawalMinSize { get; set; }
        /// <summary>
        /// The minimum fee of a withdrawal
        /// </summary>
        public decimal WithdrawalMinFee { get; set; }
        /// <summary>
        /// Is withdrawing enabled for this currency
        /// </summary>
        public bool IsWithdrawEnabled { get; set; }
        /// <summary>
        /// Is depositing enabled for this currency
        /// </summary>
        public bool IsDepositEnabled { get; set; }
    }
}
