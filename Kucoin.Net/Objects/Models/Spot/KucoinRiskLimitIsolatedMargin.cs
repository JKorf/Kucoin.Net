using CryptoExchange.Net.Converters.SystemTextJson;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Risk limit info
    /// </summary>
    [SerializationModel]
    public record KucoinRiskLimitIsolatedMargin
    {
        /// <summary>
        /// The Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Max borrow quantity
        /// </summary>
        [JsonPropertyName("baseMaxBorrowAmount")]
        public decimal BaseMaxBorrowQuantity { get; set; }

        /// <summary>
        /// Max borrow quantity
        /// </summary>
        [JsonPropertyName("quoteMaxBorrowAmount")]
        public decimal QuoteMaxBorrowQuantity { get; set; }

        /// <summary>
        /// BaseMax buy quantity
        /// </summary>
        [JsonPropertyName("baseMaxBuyAmount")]
        public decimal BaseMaxBuyQuantity { get; set; }

        /// <summary>
        /// Quote Max buy quantity
        /// </summary>
        [JsonPropertyName("quoteMaxBuyAmount")]
        public decimal QuoteMaxBuyQuantity { get; set; }

        /// <summary>
        /// Base Precision
        /// </summary>
        ///         
        [JsonPropertyName("basePrecision")]
        public int BasePrecision { get; set; }

        /// <summary>
        /// Quote Precision
        /// </summary>
        ///         
        [JsonPropertyName("quotePrecision")]
        public int QuotePrecision { get; set; }
    }
}
