using CryptoExchange.Net.Converters.SystemTextJson;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// New order id
    /// </summary>
    [SerializationModel]
    public record KucoinNewMarginOrder : KucoinOrderId
    {
        /// <summary>
        /// Borrow quantity
        /// </summary>
        [JsonPropertyName("borrowSize")]
        public decimal? BorrowQuantity { get; set; }
        /// <summary>
        /// Loan apply id
        /// </summary>
        [JsonPropertyName("loanApplyId")]
        public string LoanApplyId { get; set; } = string.Empty;
    }
}
