using CryptoExchange.Net.Converters.SystemTextJson;
using Kucoin.Net.Enums;

using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Margin account info
    /// </summary>
    [SerializationModel]
    public record KucoinCrossMarginAccount
    {
        /// <summary>
        /// Total Assets in Quote Currency
        /// </summary>
        [JsonPropertyName("totalLiabilityOfQuoteCurrency")]
        public decimal TotalLiabilityOfQuoteAsset { get; set; }
        /// <summary>
        /// Total Liability in Quote Currency
        /// </summary>
        [JsonPropertyName("totalAssetOfQuoteCurrency")]
        public decimal TotalAssetOfQuoteAsset { get; set; }
        /// <summary>
        /// debt ratio
        /// </summary>
        [JsonPropertyName("debtRatio")]
        public decimal DebtRatio { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public CrossMarginStatus Status { get; set; }
        /// <summary>
        /// Accounts
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
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Support borrow or not
        /// </summary>
        [JsonPropertyName("borrowEnabled")]
        public bool BorrowEnabled { get; set; }
        /// <summary>
        /// Support transfer or not
        /// </summary>
        [JsonPropertyName("transferInEnabled")]
        public bool TransferEnabled { get; set; }
        /// <summary>
        /// Borrowed
        /// </summary>
        [JsonPropertyName("borrowed")]
        public decimal Borrowed { get; set; }
        /// <summary>
        /// Total Assets
        /// </summary>
        [JsonPropertyName("total")]
        public decimal TotalAsset { get; set; }
        /// <summary>
        /// Account available assets (total assets - frozen)
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// Account frozen assets
        /// </summary>
        [JsonPropertyName("hold")]
        public decimal Hold { get; set; }
        /// <summary>
        /// The user's remaining maximum loan amount
        /// </summary>
        [JsonPropertyName("maxBorrowSize")]
        public decimal MaxBorrowQuantity { get; set; }
        /// <summary>
        /// Liabilities
        /// </summary>
        [JsonPropertyName("liability")]
        public decimal Liability { get; set; }
        /// <summary>
        /// Outstanding principal – the unpaid loan amount
        /// </summary>
        [JsonPropertyName("liabilityPrincipal")]
        public decimal LiabilityPrincipal { get; set; }
        /// <summary>
        /// Accrued interest – the unpaid interest amount
        /// </summary>
        [JsonPropertyName("liabilityInterest")]
        public decimal LiabilityInterest { get; set; }
    }
}
