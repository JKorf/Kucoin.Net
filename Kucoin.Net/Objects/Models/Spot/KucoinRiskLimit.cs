using CryptoExchange.Net.Converters.SystemTextJson;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Risk limit info
    /// </summary>
    [SerializationModel]
    public record KucoinRiskLimit
    {
        /// <summary>
        /// The asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
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
        /// Precision
        /// </summary>
        [JsonPropertyName("precision")]
        public int Precision { get; set; }
    }
}
