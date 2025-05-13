using CryptoExchange.Net.Converters.SystemTextJson;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Lending asset
    /// </summary>
    [SerializationModel]
    public record KucoinLendingAsset
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Is purchasing enabled
        /// </summary>
        [JsonPropertyName("purchaseEnable")]
        public bool PurchaseEnabled { get; set; }
        /// <summary>
        /// Is redeeming enabled
        /// </summary>
        [JsonPropertyName("redeemEnable")]
        public bool RedeemEnabled { get; set; }
        /// <summary>
        /// Increment precision for subscription and redemption
        /// </summary>
        [JsonPropertyName("increment")]
        public decimal Increment { get; set; }
        /// <summary>
        /// Minimal purchase quantity
        /// </summary>
        [JsonPropertyName("minPurchaseSize")]
        public decimal MinPurchaseQuantity { get; set; }
        /// <summary>
        /// Minimal interest rate
        /// </summary>
        [JsonPropertyName("minInterestRate")]
        public decimal MinInterestRate { get; set; }
        /// <summary>
        /// Max interest rate
        /// </summary>
        [JsonPropertyName("maxInterestRate")]
        public decimal MaxInterestRate { get; set; }
        /// <summary>
        /// Interest precision
        /// </summary>
        [JsonPropertyName("interestIncrement")]
        public decimal InterestIncrement { get; set; }
        /// <summary>
        /// Max purchase quantity
        /// </summary>
        [JsonPropertyName("maxPurchaseSize")]
        public decimal MaxPurchaseQuantity { get; set; }
        /// <summary>
        /// Latest market annualized interest rate
        /// </summary>
        [JsonPropertyName("marketInterestRate")]
        public decimal MarketInterestRate { get; set; }
        /// <summary>
        /// Is Auto-Subscribe enabled
        /// </summary>
        [JsonPropertyName("autoPurchaseEnable")]
        public bool AutoSubscribeEnabled { get; set; }
    }
}
