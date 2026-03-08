using Kucoin.Net.Enums;

using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Margin account info
    /// </summary>
    [SerializationModel]
    public record KucoinCrossMarginAccount
    {
        /// <summary>
        /// ["<c>totalLiabilityOfQuoteCurrency</c>"] Total Assets in Quote Currency
        /// </summary>
        [JsonPropertyName("totalLiabilityOfQuoteCurrency")]
        public decimal TotalLiabilityOfQuoteAsset { get; set; }
        /// <summary>
        /// ["<c>totalAssetOfQuoteCurrency</c>"] Total Liability in Quote Currency
        /// </summary>
        [JsonPropertyName("totalAssetOfQuoteCurrency")]
        public decimal TotalAssetOfQuoteAsset { get; set; }
        /// <summary>
        /// ["<c>debtRatio</c>"] debt ratio
        /// </summary>
        [JsonPropertyName("debtRatio")]
        public decimal DebtRatio { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public CrossMarginStatus Status { get; set; }
        /// <summary>
        /// ["<c>accounts</c>"] Accounts
        /// </summary>
        [JsonPropertyName("accounts")]
        public KucoinCrossMarginAccountAsset[] Accounts { get; set; } = Array.Empty<KucoinCrossMarginAccountAsset>();
    }

    /// <summary>
    /// Margin account asset info
    /// </summary>
    [SerializationModel]
    public record KucoinCrossMarginAccountAsset
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>borrowEnabled</c>"] Support borrow or not
        /// </summary>
        [JsonPropertyName("borrowEnabled")]
        public bool BorrowEnabled { get; set; }
        /// <summary>
        /// ["<c>transferInEnabled</c>"] Support transfer or not
        /// </summary>
        [JsonPropertyName("transferInEnabled")]
        public bool TransferEnabled { get; set; }
        /// <summary>
        /// ["<c>borrowed</c>"] Borrowed
        /// </summary>
        [JsonPropertyName("borrowed")]
        public decimal Borrowed { get; set; }
        /// <summary>
        /// ["<c>total</c>"] Total Assets
        /// </summary>
        [JsonPropertyName("total")]
        public decimal TotalAsset { get; set; }
        /// <summary>
        /// ["<c>available</c>"] Account available assets (total assets - frozen)
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// ["<c>hold</c>"] Account frozen assets
        /// </summary>
        [JsonPropertyName("hold")]
        public decimal Hold { get; set; }
        /// <summary>
        /// ["<c>maxBorrowSize</c>"] The user's remaining maximum loan amount
        /// </summary>
        [JsonPropertyName("maxBorrowSize")]
        public decimal MaxBorrowQuantity { get; set; }
        /// <summary>
        /// ["<c>liability</c>"] Liabilities
        /// </summary>
        [JsonPropertyName("liability")]
        public decimal Liability { get; set; }
        /// <summary>
        /// ["<c>liabilityPrincipal</c>"] Outstanding principal � the unpaid loan amount
        /// </summary>
        [JsonPropertyName("liabilityPrincipal")]
        public decimal LiabilityPrincipal { get; set; }
        /// <summary>
        /// ["<c>liabilityInterest</c>"] Accrued interest � the unpaid interest amount
        /// </summary>
        [JsonPropertyName("liabilityInterest")]
        public decimal LiabilityInterest { get; set; }
    }
}
