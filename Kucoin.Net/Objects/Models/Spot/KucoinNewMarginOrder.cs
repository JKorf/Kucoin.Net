namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// New order id
    /// </summary>
    [SerializationModel]
    public record KucoinNewMarginOrder : KucoinOrderId
    {
        /// <summary>
        /// ["<c>borrowSize</c>"] Borrow quantity
        /// </summary>
        [JsonPropertyName("borrowSize")]
        public decimal? BorrowQuantity { get; set; }
        /// <summary>
        /// ["<c>loanApplyId</c>"] Loan apply id
        /// </summary>
        [JsonPropertyName("loanApplyId")]
        public string LoanApplyId { get; set; } = string.Empty;
    }
}
