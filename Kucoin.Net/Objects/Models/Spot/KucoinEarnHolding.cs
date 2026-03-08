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
        /// ["<c>orderId</c>"] Holding ID
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>productId</c>"] Product ID
        /// </summary>
        [JsonPropertyName("productId")]
        public string ProductId { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>productCategory</c>"] Product category
        /// </summary>
        [JsonPropertyName("productCategory")]
        public EarnProductCategory ProductCategory { get; set; }

        /// <summary>
        /// ["<c>productType</c>"] Product sub-type
        /// </summary>
        [JsonPropertyName("productType")]
        public string ProductType { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>currency</c>"] Hold asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>incomeCurrency</c>"] Income asset
        /// </summary>
        [JsonPropertyName("incomeCurrency")]
        public string IncomeAsset { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>returnRate</c>"] Annualized Rate of Return, for example, 0.035 is equal to 3.5% annualized rate of return
        /// </summary>
        [JsonPropertyName("returnRate")]
        public decimal ReturnRate { get; set; }

        /// <summary>
        /// ["<c>holdAmount</c>"] Holding amount
        /// </summary>
        [JsonPropertyName("holdAmount")]
        public decimal HoldAmount { get; set; }

        /// <summary>
        /// ["<c>redeemedAmount</c>"] Redeemed amount
        /// </summary>
        [JsonPropertyName("redeemedAmount")]
        public decimal RedeemedAmount { get; set; }

        /// <summary>
        /// ["<c>redeemingAmount</c>"] Redeeming amount
        /// </summary>
        [JsonPropertyName("redeemingAmount")]
        public decimal RedeemingAmount { get; set; }

        /// <summary>
        /// ["<c>lockStartTime</c>"] Product earliest interest start time
        /// </summary>
        [JsonPropertyName("lockStartTime")]
        public DateTime LockStartTime { get; set; }

        /// <summary>
        /// ["<c>lockEndTime</c>"] Product maturity time
        /// </summary>
        [JsonPropertyName("lockEndTime")]
        public DateTime? LockEndTime { get; set; }

        /// <summary>
        /// ["<c>purchaseTime</c>"] Most recent subscription time
        /// </summary>
        [JsonPropertyName("purchaseTime")]
        public DateTime PurchaseTime { get; set; }

        /// <summary>
        /// ["<c>redeemPeriod</c>"] Redemption waiting period (days)
        /// </summary>
        [JsonPropertyName("redeemPeriod")]
        public int RedeemPeriod { get; set; }

        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public EarnHoldingStatus Status { get; set; }

        /// <summary>
        /// ["<c>earlyRedeemSupported</c>"] Whether the fixed product supports early redemption
        /// </summary>
        [JsonPropertyName("earlyRedeemSupported")]
        public bool EarlyRedeemable { get; set; }
    }
}