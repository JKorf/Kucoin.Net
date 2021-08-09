using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Futures
{
    /// <summary>
    /// WIthdrawal quota
    /// </summary>
    public class KucoinFuturesWithdrawalQuota
    {
        /// <summary>
        /// The currency the quota is for
        /// </summary>
        public string Currency { get; set; } = string.Empty;
        /// <summary>
        /// The remaining amount which can be withdrawn
        /// </summary>
        [JsonProperty("remainAmount")]
        public decimal RemainingQuantity { get; set; }
        /// <summary>
        /// The current amount available for withdrawal
        /// </summary>
        public decimal AvailableQuantity { get; set; }
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
    }
}
