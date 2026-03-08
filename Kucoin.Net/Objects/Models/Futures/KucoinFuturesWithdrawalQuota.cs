namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Withdrawal quota
    /// </summary>
    [SerializationModel]
    public record KucoinFuturesWithdrawalQuota
    {
        /// <summary>
        /// ["<c>currency</c>"] The asset the quota is for
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>remainAmount</c>"] The remaining quantity which can be withdrawn
        /// </summary>
        [JsonPropertyName("remainAmount")]
        public decimal QuantityRemaining { get; set; }
        /// <summary>
        /// ["<c>limitAmount</c>"] 24h withdrawal limit
        /// </summary>
        [JsonPropertyName("limitAmount")]
        public decimal LimitQuantity { get; set; }
        /// <summary>
        /// ["<c>usedAmount</c>"] 24h withdrawal limit
        /// </summary>
        [JsonPropertyName("usedAmount")]
        public decimal UsedQuantity { get; set; }
        /// <summary>
        /// ["<c>availableAmount</c>"] The current quantity available for withdrawal
        /// </summary>
        [JsonPropertyName("availableAmount")]
        public decimal QuantityAvailable { get; set; }
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
    }
}
