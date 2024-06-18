using Newtonsoft.Json;

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
        [JsonProperty("orderNo")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Actual borrowed quantity
        /// </summary>
        [JsonProperty("actualSize")]
        public decimal BorrowedQuantity { get; set; }
    }
}
