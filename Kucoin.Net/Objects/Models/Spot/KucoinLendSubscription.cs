using CryptoExchange.Net.Converters.SystemTextJson;

using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Lend subscription info
    /// </summary>
    [SerializationModel]
    public record KucoinLendSubscription
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Purchase order id
        /// </summary>
        [JsonPropertyName("purchaseOrderNo")]
        public string PurchaseOrderId { get; set; } = string.Empty;
        /// <summary>
        /// Purchase quantity
        /// </summary>
        [JsonPropertyName("purchaseSize")]
        public decimal PurchaseQuantity { get; set; }
        /// <summary>
        /// Executed amount
        /// </summary>
        [JsonPropertyName("matchSize")]
        public decimal QuantityExecuted { get; set; }
        /// <summary>
        /// Redeemed amount
        /// </summary>
        [JsonPropertyName("redeemSize")]
        public decimal QuantityRedeemed { get; set; }
        /// <summary>
        /// Target annualized interest rate
        /// </summary>
        [JsonPropertyName("interestRate")]
        public decimal InterestRate { get; set; }
        /// <summary>
        /// Total earnings
        /// </summary>
        [JsonPropertyName("incomeSize")]
        public decimal TotalEarnings { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Apply time
        /// </summary>
        [JsonPropertyName("applyTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime ApplyTime { get; set; }

    }
}
