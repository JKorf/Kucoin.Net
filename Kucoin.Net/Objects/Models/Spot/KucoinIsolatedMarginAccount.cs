using Kucoin.Net.Enums;

using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Margin accounts info
    /// </summary>
    [SerializationModel]
    public record KucoinIsolatedMarginAccountsInfo
    {
        /// <summary>
        /// ["<c>totalConversionBalance</c>"] The total balance of the isolated margin account (in the specified coin)
        /// </summary>
        [JsonPropertyName("totalConversionBalance")]
        public decimal TotalConversionBalance { get; set; }
        /// <summary>
        /// ["<c>liabilityConversionBalance</c>"] Total liabilities of the isolated margin account (in the specified coin)
        /// </summary>
        [JsonPropertyName("liabilityConversionBalance")]
        public decimal LiabilityConversionBalance { get; set; }
        /// <summary>
        /// ["<c>assets</c>"] Account list
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
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] Position status
        /// </summary>

        [JsonPropertyName("status")]
        public IsolatedMargingAccountStatus Status { get; set; }
        /// <summary>
        /// ["<c>debtRatio</c>"] Debt ratio
        /// </summary>
        [JsonPropertyName("debtRatio")]
        public decimal DebtRatio { get; set; }
        /// <summary>
        /// ["<c>baseAsset</c>"] Base asset info
        /// </summary>
        [JsonPropertyName("baseAsset")]
        public KucoinIsolatedMarginAccountAsset BaseAsset { get; set; } = null!;
        /// <summary>
        /// ["<c>quoteAsset</c>"] Quote asset info
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
        /// ["<c>currency</c>"] Currency
        /// </summary>
        [JsonPropertyName("currency")]
        public string Currency { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>totalBalance</c>"] Total balance
        /// </summary>
        [JsonPropertyName("totalBalance")]
        public decimal TotalBalance { get; set; }
        /// <summary>
        /// ["<c>holdBalance</c>"] Frozen balance
        /// </summary>
        [JsonPropertyName("holdBalance")]
        public decimal HoldBalance { get; set; }
        /// <summary>
        /// ["<c>availableBalance</c>"] Available balance
        /// </summary>
        [JsonPropertyName("availableBalance")]
        public decimal AvailableBalance { get; set; }
        /// <summary>
        /// ["<c>liability</c>"] Liability
        /// </summary>
        [JsonPropertyName("liability")]
        public decimal Liability { get; set; }
        /// <summary>
        /// ["<c>interest</c>"] Interset
        /// </summary>
        [JsonPropertyName("interest")]
        public decimal Interest { get; set; }
        /// <summary>
        /// ["<c>borrowableAmount</c>"] Borrowable quantity
        /// </summary>
        [JsonPropertyName("borrowableAmount")]
        public decimal BorrowableQuantity { get; set; }
    }
}
