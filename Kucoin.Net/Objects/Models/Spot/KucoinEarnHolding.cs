using Kucoin.Net.Enums;
using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Earn account holding info
    /// </summary>
    public record KucoinEarnHolding
    {
        /// <summary>
        /// Holding ID
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;

        /// <summary>
        /// Product ID
        /// </summary>
        [JsonPropertyName("productId")]
        public string ProductId { get; set; } = string.Empty;

        /// <summary>
        /// Product category
        /// </summary>
        [JsonPropertyName("productCategory")]
        public EarnProductCategory ProductCategory { get; set; }

        /// <summary>
        /// Product sub-type
        /// </summary>
        [JsonPropertyName("productType")]
        public string ProductType { get; set; } = string.Empty;

        /// <summary>
        /// Hold asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// Income asset
        /// </summary>
        [JsonPropertyName("incomeCurrency")]
        public string IncomeAsset { get; set; } = string.Empty;

        /// <summary>
        /// Annualized Rate of Return, for example, 0.035 is equal to 3.5% annualized rate of return
        /// </summary>
        [JsonPropertyName("returnRate")]
        public decimal ReturnRate { get; set; }

        /// <summary>
        /// Holding amount
        /// </summary>
        [JsonPropertyName("holdAmount")]
        public decimal HoldAmount { get; set; }

        /// <summary>
        /// Redeemed amount
        /// </summary>
        [JsonPropertyName("redeemedAmount")]
        public decimal RedeemedAmount { get; set; }

        /// <summary>
        /// Redeeming amount
        /// </summary>
        [JsonPropertyName("redeemingAmount")]
        public decimal RedeemingAmount { get; set; }

        /// <summary>
        /// Product earliest interest start time
        /// </summary>
        [JsonPropertyName("lockStartTime")]
        public DateTime LockStartTime { get; set; }

        /// <summary>
        /// Product maturity time
        /// </summary>
        [JsonPropertyName("lockEndTime")]
        public DateTime? LockEndTime { get; set; }

        /// <summary>
        /// Most recent subscription time
        /// </summary>
        [JsonPropertyName("purchaseTime")]
        public DateTime PurchaseTime { get; set; }

        /// <summary>
        /// Redemption waiting period (days)
        /// </summary>
        [JsonPropertyName("redeemPeriod")]
        public int RedeemPeriod { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public EarnHoldingStatus Status { get; set; }

        /// <summary>
        /// Whether the fixed product supports early redemption
        /// </summary>
        [JsonPropertyName("earlyRedeemSupported")]
        public bool EarlyRedeemable { get; set; }
    }
}