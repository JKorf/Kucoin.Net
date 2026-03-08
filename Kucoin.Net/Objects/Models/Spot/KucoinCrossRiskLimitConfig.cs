using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Cross margin risk limit and asset configuration
    /// </summary>
    [SerializationModel]
    public record KucoinCrossRiskLimitConfig
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset name
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>borrowMaxAmount</c>"] Max borrow quantity
        /// </summary>
        [JsonPropertyName("borrowMaxAmount")]
        public decimal BorrowMaxQuantity { get; set; }
        /// <summary>
        /// ["<c>buyMaxAmount</c>"] Max buy quantity
        /// </summary>
        [JsonPropertyName("buyMaxAmount")]
        public decimal BuyMaxQuantity { get; set; }
        /// <summary>
        /// ["<c>holdMaxAmount</c>"] Max hold quantity
        /// </summary>
        [JsonPropertyName("holdMaxAmount")]
        public decimal HoldMaxQuantity { get; set; }
        /// <summary>
        /// ["<c>borrowCoefficient</c>"] Borrow coefficient
        /// </summary>
        [JsonPropertyName("borrowCoefficient")]
        public decimal BorrowCoefficient { get; set; }
        /// <summary>
        /// ["<c>marginCoefficient</c>"] Margin coefficient
        /// </summary>
        [JsonPropertyName("marginCoefficient")]
        public decimal MarginCoefficient { get; set; }
        /// <summary>
        /// ["<c>precision</c>"] Asset precision
        /// </summary>
        [JsonPropertyName("precision")]
        public int Precision { get; set; }
        /// <summary>
        /// ["<c>borrowMinAmount</c>"] Min borrow quantity
        /// </summary>
        [JsonPropertyName("borrowMinAmount")]
        public decimal? BorrowMinQuantity { get; set; }
        /// <summary>
        /// ["<c>borrowMinUnit</c>"] Minimum unit for borrowing
        /// </summary>
        [JsonPropertyName("borrowMinUnit")]
        public decimal? BorrowMinUnit { get; set; }
        /// <summary>
        /// ["<c>borrowEnabled</c>"] Borrow is enabled
        /// </summary>
        [JsonPropertyName("borrowEnabled")]
        public bool BorrowEnabled { get; set; }
    }
}
