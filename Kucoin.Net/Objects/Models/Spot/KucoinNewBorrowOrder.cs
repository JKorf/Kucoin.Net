namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// New Borrow order
    /// </summary>
    [SerializationModel]
    public record KucoinNewBorrowOrder
    {
        /// <summary>
        /// ["<c>orderNo</c>"] The id of the new borrow order
        /// </summary>
        [JsonPropertyName("orderNo")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>actualSize</c>"] Actual borrowed quantity
        /// </summary>
        [JsonPropertyName("actualSize")]
        public decimal BorrowedQuantity { get; set; }
    }
}
