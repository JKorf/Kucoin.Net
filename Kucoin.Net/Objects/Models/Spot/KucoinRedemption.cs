using CryptoExchange.Net.Converters.SystemTextJson;

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
        /// Redeem order id
        /// </summary>
        [JsonPropertyName("redeemOrderNo")]
        public string RedeemOrderId { get; set; } = string.Empty;
        /// <summary>
        /// Redeem quantity
        /// </summary>
        [JsonPropertyName("redeemSize")]
        public decimal RedeemQuantity { get; set; }
        /// <summary>
        /// Redeemed quantity
        /// </summary>
        [JsonPropertyName("receiptSize")]
        public decimal ReceiptQuantity { get; set; }
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
