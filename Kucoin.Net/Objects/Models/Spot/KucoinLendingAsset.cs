namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Lending asset
    /// </summary>
    [SerializationModel]
    public record KucoinLendingAsset
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>purchaseEnable</c>"] Is purchasing enabled
        /// </summary>
        [JsonPropertyName("purchaseEnable")]
        public bool PurchaseEnabled { get; set; }
        /// <summary>
        /// ["<c>redeemEnable</c>"] Is redeeming enabled
        /// </summary>
        [JsonPropertyName("redeemEnable")]
        public bool RedeemEnabled { get; set; }
        /// <summary>
        /// ["<c>increment</c>"] Increment precision for subscription and redemption
        /// </summary>
        [JsonPropertyName("increment")]
        public decimal Increment { get; set; }
        /// <summary>
        /// ["<c>minPurchaseSize</c>"] Minimal purchase quantity
        /// </summary>
        [JsonPropertyName("minPurchaseSize")]
        public decimal MinPurchaseQuantity { get; set; }
        /// <summary>
        /// ["<c>minInterestRate</c>"] Minimal interest rate
        /// </summary>
        [JsonPropertyName("minInterestRate")]
        public decimal MinInterestRate { get; set; }
        /// <summary>
        /// ["<c>maxInterestRate</c>"] Max interest rate
        /// </summary>
        [JsonPropertyName("maxInterestRate")]
        public decimal MaxInterestRate { get; set; }
        /// <summary>
        /// ["<c>interestIncrement</c>"] Interest precision
        /// </summary>
        [JsonPropertyName("interestIncrement")]
        public decimal InterestIncrement { get; set; }
        /// <summary>
        /// ["<c>maxPurchaseSize</c>"] Max purchase quantity
        /// </summary>
        [JsonPropertyName("maxPurchaseSize")]
        public decimal MaxPurchaseQuantity { get; set; }
        /// <summary>
        /// ["<c>marketInterestRate</c>"] Latest market annualized interest rate
        /// </summary>
        [JsonPropertyName("marketInterestRate")]
        public decimal MarketInterestRate { get; set; }
        /// <summary>
        /// ["<c>autoPurchaseEnable</c>"] Is Auto-Subscribe enabled
        /// </summary>
        [JsonPropertyName("autoPurchaseEnable")]
        public bool AutoSubscribeEnabled { get; set; }
    }
}
