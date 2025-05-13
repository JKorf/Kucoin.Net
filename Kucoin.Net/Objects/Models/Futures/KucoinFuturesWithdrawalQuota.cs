using CryptoExchange.Net.Converters.SystemTextJson;


namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Withdrawal quota
    /// </summary>
    [SerializationModel]
    public record KucoinFuturesWithdrawalQuota
    {
        /// <summary>
        /// The asset the quota is for
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The remaining quantity which can be withdrawn
        /// </summary>
        [JsonPropertyName("remainAmount")]
        public decimal QuantityRemaining { get; set; }
        /// <summary>
        /// 24h withdrawal limit
        /// </summary>
        [JsonPropertyName("limitAmount")]
        public decimal LimitQuantity { get; set; }
        /// <summary>
        /// 24h withdrawal limit
        /// </summary>
        [JsonPropertyName("usedAmount")]
        public decimal UsedQuantity { get; set; }
        /// <summary>
        /// The current quantity available for withdrawal
        /// </summary>
        [JsonPropertyName("availableAmount")]
        public decimal QuantityAvailable { get; set; }
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
    }
}
