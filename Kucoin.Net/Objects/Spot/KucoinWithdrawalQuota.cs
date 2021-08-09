using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Spot
{
    /// <summary>
    /// Withdrawal quota info
    /// </summary>
    public class KucoinWithdrawalQuota
    {
        /// <summary>
        /// The currency the quota is for
        /// </summary>
        public string Currency { get; set; } = string.Empty;
        /// <summary>
        /// The max BTC value that can be withdrawn
        /// </summary>
        [JsonProperty("limitBtcAmount")]
        public decimal LimitBTCQuantity { get; set; }
        /// <summary>
        /// The used BTC value
        /// </summary>
        [JsonProperty("usdBtcAmount")]
        public decimal UsedBTCQuantity { get; set; }
        /// <summary>
        /// The remaining amount which can be withdrawn
        /// </summary>
        [JsonProperty("remainAmount")]
        public decimal RemainingQuantity { get; set; }
        /// <summary>
        /// The current amount available for withdrawal
        /// </summary>
        [JsonProperty("availableAmount")]
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
        /// <summary>
        /// The chain
        /// </summary>
        public string Chain { get; set; } = string.Empty;
    }
}
