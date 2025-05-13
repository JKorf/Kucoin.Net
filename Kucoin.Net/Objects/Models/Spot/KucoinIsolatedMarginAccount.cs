using CryptoExchange.Net.Converters.SystemTextJson;
using Kucoin.Net.Enums;

using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Margin accounts info
    /// </summary>
    [SerializationModel]
    public record KucoinIsolatedMarginAccountsInfo
    {
        /// <summary>
        /// The total balance of the isolated margin account (in the specified coin)
        /// </summary>
        [JsonPropertyName("totalConversionBalance")]
        public decimal TotalConversionBalance { get; set; }
        /// <summary>
        /// Total liabilities of the isolated margin account (in the specified coin)
        /// </summary>
        [JsonPropertyName("liabilityConversionBalance")]
        public decimal LiabilityConversionBalance { get; set; }
        /// <summary>
        /// Account list
        /// </summary>
        [JsonPropertyName("assets")]
        public KucoinIsolatedMarginAccount[] Assets { get; set; } = Array.Empty<KucoinIsolatedMarginAccount>();
    }

    /// <summary>
    /// Isolated margin account info
    /// </summary>
    [SerializationModel]
    public record KucoinIsolatedMarginAccount
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Position status
        /// </summary>

        [JsonPropertyName("status")]
        public IsolatedMargingAccountStatus Status { get; set; }
        /// <summary>
        /// Debt ratio
        /// </summary>
        [JsonPropertyName("debtRatio")]
        public decimal DebtRatio { get; set; }
        /// <summary>
        /// Base asset info
        /// </summary>
        [JsonPropertyName("baseAsset")]
        public KucoinIsolatedMarginAccountAsset BaseAsset { get; set; } = null!;
        /// <summary>
        /// Quote asset info
        /// </summary>
        [JsonPropertyName("quoteAsset")]
        public KucoinIsolatedMarginAccountAsset QuoteAsset { get; set; } = null!;
    }

    /// <summary>
    /// Isolate margin account asset info
    /// </summary>
    [SerializationModel]
    public record KucoinIsolatedMarginAccountAsset
    {
        /// <summary>
        /// Currency
        /// </summary>
        [JsonPropertyName("currency")]
        public string Currency { get; set; } = string.Empty;
        /// <summary>
        /// Total balance
        /// </summary>
        [JsonPropertyName("totalBalance")]
        public decimal TotalBalance { get; set; }
        /// <summary>
        /// Frozen balance
        /// </summary>
        [JsonPropertyName("holdBalance")]
        public decimal HoldBalance { get; set; }
        /// <summary>
        /// Available balance
        /// </summary>
        [JsonPropertyName("availableBalance")]
        public decimal AvailableBalance { get; set; }
        /// <summary>
        /// Liability
        /// </summary>
        [JsonPropertyName("liability")]
        public decimal Liability { get; set; }
        /// <summary>
        /// Interset
        /// </summary>
        [JsonPropertyName("interest")]
        public decimal Interest { get; set; }
        /// <summary>
        /// Borrowable quantity
        /// </summary>
        [JsonPropertyName("borrowableAmount")]
        public decimal BorrowableQuantity { get; set; }
    }
}
