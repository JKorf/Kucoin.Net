using CryptoExchange.Net.Converters.SystemTextJson;

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
        /// Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Base asset max borrow quantity
        /// </summary>
        [JsonPropertyName("baseMaxBorrowAmount")]
        public decimal BaseBorrowMaxQuantity { get; set; }
        /// <summary>
        /// Base asset max buy quantity
        /// </summary>
        [JsonPropertyName("baseMaxBuyAmount")]
        public decimal BaseBuyMaxQuantity { get; set; }
        /// <summary>
        /// Base asset max hold quantity
        /// </summary>
        [JsonPropertyName("baseMaxHoldAmount")]
        public decimal BaseHoldMaxQuantity { get; set; }
        /// <summary>
        /// Base asset precision
        /// </summary>
        [JsonPropertyName("basePrecision")]
        public int BasePrecision { get; set; }
        /// <summary>
        /// Base asset min borrow quantity
        /// </summary>
        [JsonPropertyName("baseBorrowMinAmount")]
        public decimal? BaseBorrowMinQuantity { get; set; }
        /// <summary>
        /// Base asset minimum unit for borrowing
        /// </summary>
        [JsonPropertyName("baseBorrowMinUnit")]
        public decimal? BaseBorrowMinUnit { get; set; }
        /// <summary>
        /// Base asset borrow is enabled
        /// </summary>
        [JsonPropertyName("baseBorrowEnabled")]
        public bool BaseBorrowEnabled { get; set; }
        /// <summary>
        /// Quote asset max borrow quantity
        /// </summary>
        [JsonPropertyName("quoteMaxBorrowAmount")]
        public decimal QuoteBorrowMaxQuantity { get; set; }
        /// <summary>
        /// Quote asset max buy quantity
        /// </summary>
        [JsonPropertyName("quoteMaxBuyAmount")]
        public decimal QuoteBuyMaxQuantity { get; set; }
        /// <summary>
        /// Quote asset max hold quantity
        /// </summary>
        [JsonPropertyName("quoteMaxHoldAmount")]
        public decimal QuoteHoldMaxQuantity { get; set; }
        /// <summary>
        /// Quote asset precision
        /// </summary>
        [JsonPropertyName("quotePrecision")]
        public int QuotePrecision { get; set; }
        /// <summary>
        /// Quote asset min borrow quantity
        /// </summary>
        [JsonPropertyName("quoteBorrowMinAmount")]
        public decimal? QuoteBorrowMinQuantity { get; set; }
        /// <summary>
        /// Quote asset minimum unit for borrowing
        /// </summary>
        [JsonPropertyName("quoteBorrowMinUnit")]
        public decimal? QuoteBorrowMinUnit { get; set; }
        /// <summary>
        /// Quote asset borrow is enabled
        /// </summary>
        [JsonPropertyName("quoteBorrowEnabled")]
        public bool QuoteBorrowEnabled { get; set; }
    }
}
