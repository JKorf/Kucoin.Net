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
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>purchaseOrderNo</c>"] Purchase order id
        /// </summary>
        [JsonPropertyName("purchaseOrderNo")]
        public string PurchaseOrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>purchaseSize</c>"] Purchase quantity
        /// </summary>
        [JsonPropertyName("purchaseSize")]
        public decimal PurchaseQuantity { get; set; }
        /// <summary>
        /// ["<c>matchSize</c>"] Executed amount
        /// </summary>
        [JsonPropertyName("matchSize")]
        public decimal QuantityExecuted { get; set; }
        /// <summary>
        /// ["<c>redeemSize</c>"] Redeemed amount
        /// </summary>
        [JsonPropertyName("redeemSize")]
        public decimal QuantityRedeemed { get; set; }
        /// <summary>
        /// ["<c>interestRate</c>"] Target annualized interest rate
        /// </summary>
        [JsonPropertyName("interestRate")]
        public decimal InterestRate { get; set; }
        /// <summary>
        /// ["<c>incomeSize</c>"] Total earnings
        /// </summary>
        [JsonPropertyName("incomeSize")]
        public decimal TotalEarnings { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>applyTime</c>"] Apply time
        /// </summary>
        [JsonPropertyName("applyTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime ApplyTime { get; set; }

    }
}
