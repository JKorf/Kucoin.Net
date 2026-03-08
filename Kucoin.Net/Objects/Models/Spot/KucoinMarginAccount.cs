using System;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Margin account info
    /// </summary>
    [SerializationModel]
    public record KucoinMarginAccount
    {
        /// <summary>
        /// ["<c>accounts</c>"] Accounts
        /// </summary>
        [JsonPropertyName("accounts")]
        public KucoinMarginAccountDetails[] Accounts { get; set; } = Array.Empty<KucoinMarginAccountDetails>();
        /// <summary>
        /// ["<c>debtRatio</c>"] Debt ratio
        /// </summary>
        [JsonPropertyName("debtRatio")]
        public decimal DebtRatio { get; set; }
    }

    /// <summary>
    /// Account details
    /// </summary>
    [SerializationModel]
    public record KucoinMarginAccountDetails
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>availableBalance</c>"] Available balance
        /// </summary>
        [JsonPropertyName("availableBalance")]
        public decimal AvailableBalance { get; set; }
        /// <summary>
        /// ["<c>holdBalance</c>"] Hold balance
        /// </summary>
        [JsonPropertyName("holdBalance")]
        public decimal HoldBalance { get; set; }
        /// <summary>
        /// ["<c>liability</c>"] Liability
        /// </summary>
        [JsonPropertyName("liability")]
        public decimal Liability { get; set; }
        /// <summary>
        /// ["<c>maxBorrowSize</c>"] Max borrow quantity
        /// </summary>
        [JsonPropertyName("maxBorrowSize")]
        public decimal MaxBorrowQuantity { get; set; }
        /// <summary>
        /// ["<c>totalBalance</c>"] Total balance
        /// </summary>
        [JsonPropertyName("totalBalance")]
        public decimal TotalBalance { get; set; }
    }
}
