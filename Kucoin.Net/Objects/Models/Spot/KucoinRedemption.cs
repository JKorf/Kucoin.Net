using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Redemption record
    /// </summary>
    [SerializationModel]
    public record KucoinRedemption
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
        /// ["<c>redeemOrderNo</c>"] Redeem order id
        /// </summary>
        [JsonPropertyName("redeemOrderNo")]
        public string RedeemOrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>redeemSize</c>"] Redeem quantity
        /// </summary>
        [JsonPropertyName("redeemSize")]
        public decimal RedeemQuantity { get; set; }
        /// <summary>
        /// ["<c>receiptSize</c>"] Redeemed quantity
        /// </summary>
        [JsonPropertyName("receiptSize")]
        public decimal ReceiptQuantity { get; set; }
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
