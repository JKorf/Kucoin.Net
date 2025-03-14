using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Margin account info
    /// </summary>
    [SerializationModel]
    public record KucoinMarginAccount
    {
        /// <summary>
        /// Accounts
        /// </summary>
        [JsonPropertyName("accounts")]
        public KucoinMarginAccountDetails[] Accounts { get; set; } = Array.Empty<KucoinMarginAccountDetails>();
        /// <summary>
        /// Debt ratio
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
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Available balance
        /// </summary>
        [JsonPropertyName("availableBalance")]
        public decimal AvailableBalance { get; set; }
        /// <summary>
        /// Hold balance
        /// </summary>
        [JsonPropertyName("holdBalance")]
        public decimal HoldBalance { get; set; }
        /// <summary>
        /// Liability
        /// </summary>
        [JsonPropertyName("liability")]
        public decimal Liability { get; set; }
        /// <summary>
        /// Max borrow quantity
        /// </summary>
        [JsonPropertyName("maxBorrowSize")]
        public decimal MaxBorrowQuantity { get; set; }
        /// <summary>
        /// Total balance
        /// </summary>
        [JsonPropertyName("totalBalance")]
        public decimal TotalBalance { get; set; }
    }
}
