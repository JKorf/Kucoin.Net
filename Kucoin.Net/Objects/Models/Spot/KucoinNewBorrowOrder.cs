

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// New Borrow order
    /// </summary>
    public record KucoinNewBorrowOrder
    {
        /// <summary>
        /// The id of the new borrow order
        /// </summary>
        [JsonPropertyName("orderNo")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Actual borrowed quantity
        /// </summary>
        [JsonPropertyName("actualSize")]
        public decimal BorrowedQuantity { get; set; }
    }
}
