namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Withdrawal quota info
    /// </summary>
    [SerializationModel]
    public record KucoinWithdrawalQuota
    {
        /// <summary>
        /// ["<c>currency</c>"] The asset the quota is for
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>limitBTCAmount</c>"] The max BTC value that can be withdrawn
        /// </summary>
        [JsonPropertyName("limitBTCAmount")]
        public decimal LimitBTCQuantity { get; set; }
        /// <summary>
        /// ["<c>usedBTCAmount</c>"] The used BTC value
        /// </summary>
        [JsonPropertyName("usedBTCAmount")]
        public decimal UsedBTCQuantity { get; set; }
        /// <summary>
        /// ["<c>remainAmount</c>"] The remaining quantity which can be withdrawn
        /// </summary>
        [JsonPropertyName("remainAmount")]
        public decimal RemainingQuantity { get; set; }
        /// <summary>
        /// ["<c>availableAmount</c>"] The current quantity available for withdrawal
        /// </summary>
        [JsonPropertyName("availableAmount")]
        public decimal AvailableQuantity { get; set; }
        /// <summary>
        /// ["<c>withdrawMinFee</c>"] The minimum fee for withdrawing
        /// </summary>
        [JsonPropertyName("withdrawMinFee")]
        public decimal WithdrawMinFee { get; set; }
        /// <summary>
        /// ["<c>innerWithdrawMinFee</c>"] The minimum fee for an internal withdrawal
        /// </summary>
        [JsonPropertyName("innerWithdrawMinFee")]
        public decimal InnerWithdrawMinFee { get; set; }
        /// <summary>
        /// ["<c>withdrawMinSize</c>"] The min quantity of a withdrawal
        /// </summary>
        [JsonPropertyName("withdrawMinSize")]
        public decimal WithdrawMinQuantity { get; set; }
        /// <summary>
        /// ["<c>isWithdrawEnabled</c>"] Whether withdrawing is enabled
        /// </summary>
        [JsonPropertyName("isWithdrawEnabled")]
        public bool IsWithdrawEnabled { get; set; }
        /// <summary>
        /// ["<c>precision</c>"] The precision of a withdrawal
        /// </summary>
        [JsonPropertyName("precision")]
        public int WithdrawPrecision { get; set; }
        /// <summary>
        /// ["<c>chain</c>"] The network
        /// </summary>
        [JsonPropertyName("chain")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quotaCurrency</c>"] Withdrawal limit asset
        /// </summary>
        [JsonPropertyName("quotaCurrency")]
        public string QuotaAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>limitQuotaCurrencyAmount</c>"] The intraday available withdrawal amount
        /// </summary>
        [JsonPropertyName("limitQuotaCurrencyAmount")]
        public decimal LimitQuotaAssetQuantity { get; set; }
        /// <summary>
        /// ["<c>usedQuotaCurrencyAmount</c>"] The intraday used withdrawal amount
        /// </summary>
        [JsonPropertyName("usedQuotaCurrencyAmount")]
        public decimal UsedQuotaAssetQuantity { get; set; }
        /// <summary>
        /// ["<c>lockedAmount</c>"] Total locked amount
        /// </summary>
        [JsonPropertyName("lockedAmount")]
        public decimal LockedQuantity { get; set; }
        /// <summary>
        /// ["<c>reason</c>"] Reason
        /// </summary>
        [JsonPropertyName("reason")]
        public string? Reason { get; set; }
    }
}
