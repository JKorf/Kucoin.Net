using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Isolated margin risk limit and asset configuration
    /// </summary>
    [SerializationModel]
    public record KucoinIsolatedRiskLimitConfig
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>baseMaxBorrowAmount</c>"] Base asset max borrow quantity
        /// </summary>
        [JsonPropertyName("baseMaxBorrowAmount")]
        public decimal BaseBorrowMaxQuantity { get; set; }
        /// <summary>
        /// ["<c>baseMaxBuyAmount</c>"] Base asset max buy quantity
        /// </summary>
        [JsonPropertyName("baseMaxBuyAmount")]
        public decimal BaseBuyMaxQuantity { get; set; }
        /// <summary>
        /// ["<c>baseMaxHoldAmount</c>"] Base asset max hold quantity
        /// </summary>
        [JsonPropertyName("baseMaxHoldAmount")]
        public decimal BaseHoldMaxQuantity { get; set; }
        /// <summary>
        /// ["<c>basePrecision</c>"] Base asset precision
        /// </summary>
        [JsonPropertyName("basePrecision")]
        public int BasePrecision { get; set; }
        /// <summary>
        /// ["<c>baseBorrowMinAmount</c>"] Base asset min borrow quantity
        /// </summary>
        [JsonPropertyName("baseBorrowMinAmount")]
        public decimal? BaseBorrowMinQuantity { get; set; }
        /// <summary>
        /// ["<c>baseBorrowMinUnit</c>"] Base asset minimum unit for borrowing
        /// </summary>
        [JsonPropertyName("baseBorrowMinUnit")]
        public decimal? BaseBorrowMinUnit { get; set; }
        /// <summary>
        /// ["<c>baseBorrowEnabled</c>"] Base asset borrow is enabled
        /// </summary>
        [JsonPropertyName("baseBorrowEnabled")]
        public bool BaseBorrowEnabled { get; set; }
        /// <summary>
        /// ["<c>quoteMaxBorrowAmount</c>"] Quote asset max borrow quantity
        /// </summary>
        [JsonPropertyName("quoteMaxBorrowAmount")]
        public decimal QuoteBorrowMaxQuantity { get; set; }
        /// <summary>
        /// ["<c>quoteMaxBuyAmount</c>"] Quote asset max buy quantity
        /// </summary>
        [JsonPropertyName("quoteMaxBuyAmount")]
        public decimal QuoteBuyMaxQuantity { get; set; }
        /// <summary>
        /// ["<c>quoteMaxHoldAmount</c>"] Quote asset max hold quantity
        /// </summary>
        [JsonPropertyName("quoteMaxHoldAmount")]
        public decimal QuoteHoldMaxQuantity { get; set; }
        /// <summary>
        /// ["<c>quotePrecision</c>"] Quote asset precision
        /// </summary>
        [JsonPropertyName("quotePrecision")]
        public int QuotePrecision { get; set; }
        /// <summary>
        /// ["<c>quoteBorrowMinAmount</c>"] Quote asset min borrow quantity
        /// </summary>
        [JsonPropertyName("quoteBorrowMinAmount")]
        public decimal? QuoteBorrowMinQuantity { get; set; }
        /// <summary>
        /// ["<c>quoteBorrowMinUnit</c>"] Quote asset minimum unit for borrowing
        /// </summary>
        [JsonPropertyName("quoteBorrowMinUnit")]
        public decimal? QuoteBorrowMinUnit { get; set; }
        /// <summary>
        /// ["<c>quoteBorrowEnabled</c>"] Quote asset borrow is enabled
        /// </summary>
        [JsonPropertyName("quoteBorrowEnabled")]
        public bool QuoteBorrowEnabled { get; set; }
    }
}
