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
        [JsonPropertyName("accounts")] // API docs incorrectly has this as 'assets'
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
        /// Support repay or not
        /// </summary>
        [JsonPropertyName("repayEnabled")]
        public bool RepayEnabled { get; set; }
        /// <summary>
        /// Support transfer or not
        /// </summary>
        [JsonPropertyName("transferEnabled")]
        public bool TransferEnabled { get; set; }
        /// <summary>
        /// Borrowed
        /// </summary>
        [JsonPropertyName("borrowed")]
        public decimal Borrowed { get; set; }
        /// <summary>
        /// Total Assets
        /// </summary>
        [JsonPropertyName("totalAsset")]
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
    }
}
