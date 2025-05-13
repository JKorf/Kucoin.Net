using CryptoExchange.Net.Converters.SystemTextJson;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Withdrawal quota info
    /// </summary>
    [SerializationModel]
    public record KucoinWithdrawalQuota
    {
        /// <summary>
        /// The asset the quota is for
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The max BTC value that can be withdrawn
        /// </summary>
        [JsonPropertyName("limitBTCAmount")]
        public decimal LimitBTCQuantity { get; set; }
        /// <summary>
        /// The used BTC value
        /// </summary>
        [JsonPropertyName("usedBTCAmount")]
        public decimal UsedBTCQuantity { get; set; }
        /// <summary>
        /// The remaining quantity which can be withdrawn
        /// </summary>
        [JsonPropertyName("remainAmount")]
        public decimal RemainingQuantity { get; set; }
        /// <summary>
        /// The current quantity available for withdrawal
        /// </summary>
        [JsonPropertyName("availableAmount")]
        public decimal AvailableQuantity { get; set; }
        /// <summary>
        /// The minimum fee for withdrawing
        /// </summary>
        [JsonPropertyName("withdrawMinFee")]
        public decimal WithdrawMinFee { get; set; }
        /// <summary>
        /// The minimum fee for an internal withdrawal
        /// </summary>
        [JsonPropertyName("innerWithdrawMinFee")]
        public decimal InnerWithdrawMinFee { get; set; }
        /// <summary>
        /// The min quantity of a withdrawal
        /// </summary>
        [JsonPropertyName("withdrawMinSize")]
        public decimal WithdrawMinQuantity { get; set; }
        /// <summary>
        /// Whether withdrawing is enabled
        /// </summary>
        [JsonPropertyName("isWithdrawEnabled")]
        public bool IsWithdrawEnabled { get; set; }
        /// <summary>
        /// The precision of a withdrawal
        /// </summary>
        [JsonPropertyName("precision")]
        public int WithdrawPrecision { get; set; }
        /// <summary>
        /// The network
        /// </summary>
        [JsonPropertyName("chain")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// Withdrawal limit asset
        /// </summary>
        [JsonPropertyName("quotaCurrency")]
        public string QuotaAsset { get; set; } = string.Empty;
        /// <summary>
        /// The intraday available withdrawal amount
        /// </summary>
        [JsonPropertyName("limitQuotaCurrencyAmount")]
        public decimal LimitQuotaAssetQuantity { get; set; }
        /// <summary>
        /// The intraday used withdrawal amount
        /// </summary>
        [JsonPropertyName("usedQuotaCurrencyAmount")]
        public decimal UsedQuotaAssetQuantity { get; set; }
        /// <summary>
        /// Total locked amount
        /// </summary>
        [JsonPropertyName("lockedAmount")]
        public decimal LockedQuantity { get; set; }
        /// <summary>
        /// Reason
        /// </summary>
        [JsonPropertyName("reason")]
        public string? Reason { get; set; }
    }
}
