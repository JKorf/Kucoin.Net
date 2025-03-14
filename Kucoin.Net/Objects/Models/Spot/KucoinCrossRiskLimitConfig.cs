using CryptoExchange.Net.Converters.SystemTextJson;

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
        /// Asset name
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Max borrow quantity
        /// </summary>
        [JsonPropertyName("borrowMaxAmount")]
        public decimal BorrowMaxQuantity { get; set; }
        /// <summary>
        /// Max buy quantity
        /// </summary>
        [JsonPropertyName("buyMaxAmount")]
        public decimal BuyMaxQuantity { get; set; }
        /// <summary>
        /// Max hold quantity
        /// </summary>
        [JsonPropertyName("holdMaxAmount")]
        public decimal HoldMaxQuantity { get; set; }
        /// <summary>
        /// Borrow coefficient
        /// </summary>
        [JsonPropertyName("borrowCoefficient")]
        public decimal BorrowCoefficient { get; set; }
        /// <summary>
        /// Margin coefficient
        /// </summary>
        [JsonPropertyName("marginCoefficient")]
        public decimal MarginCoefficient { get; set; }
        /// <summary>
        /// Asset precision
        /// </summary>
        [JsonPropertyName("precision")]
        public int Precision { get; set; }
        /// <summary>
        /// Min borrow quantity
        /// </summary>
        [JsonPropertyName("borrowMinAmount")]
        public decimal? BorrowMinQuantity { get; set; }
        /// <summary>
        /// Minimum unit for borrowing
        /// </summary>
        [JsonPropertyName("borrowMinUnit")]
        public decimal? BorrowMinUnit { get; set; }
        /// <summary>
        /// Borrow is enabled
        /// </summary>
        [JsonPropertyName("borrowEnabled")]
        public bool BorrowEnabled { get; set; }
    }
}
