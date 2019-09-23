using Newtonsoft.Json;

namespace Kucoin.Net.Objects
{
    /// <summary>
    /// Withdrawal quota info
    /// </summary>
    public class KucoinWithdrawalQuota
    {
        /// <summary>
        /// The currency the quota is for
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// The max BTC value that can be withdrawn
        /// </summary>
        public decimal LimitBTCAmount { get; set; }
        /// <summary>
        /// The used BTC value
        /// </summary>
        public decimal UsedBTCAmount { get; set; }
        /// <summary>
        /// The remaining amount which can be withdrawn
        /// </summary>
        [JsonProperty("remainAmount")]
        public decimal RemainingAmount { get; set; }
        /// <summary>
        /// The current amount available for withdrawal
        /// </summary>
        public decimal AvailableAmount { get; set; }
        /// <summary>
        /// The minimum fee for withdrawing
        /// </summary>
        public decimal WithdrawMinFee { get; set; }
        /// <summary>
        /// The minimum fee for an internal withdrawal
        /// </summary>
        public decimal InnerWithdrawMinFee { get; set; }
        /// <summary>
        /// The min size of a withdrawal
        /// </summary>
        public decimal WithdrawMinSize { get; set; }
        /// <summary>
        /// Whether withdrawing is enabled
        /// </summary>
        public bool IsWithdrawEnabled { get; set; }
        /// <summary>
        /// The precision of a withdrawal
        /// </summary>
        [JsonProperty("precision")]
        public int WithdrawPrecision { get; set; }
        /// <summary>
        /// The chain
        /// </summary>
        public string Chain { get; set; }
    }
}
