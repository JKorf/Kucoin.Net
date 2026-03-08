using Kucoin.Net.Enums;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Orders
    /// </summary>
    public record KucoinUaOrders
    {
        /// <summary>
        /// ["<c>pageNumber</c>"] Page number
        /// </summary>
        [JsonPropertyName("pageNumber")]
        public int PageNumber { get; set; }
        /// <summary>
        /// ["<c>pageSize</c>"] Page quantity
        /// </summary>
        [JsonPropertyName("pageSize")]
        public int? PageQuantity { get; set; }
        /// <summary>
        /// ["<c>totalNum</c>"] Total num
        /// </summary>
        [JsonPropertyName("totalNum")]
        public int? TotalNum { get; set; }
        /// <summary>
        /// ["<c>totalPage</c>"] Total page
        /// </summary>
        [JsonPropertyName("totalPage")]
        public int? TotalPage { get; set; }
        /// <summary>
        /// ["<c>items</c>"] Items
        /// </summary>
        [JsonPropertyName("items")]
        public KucoinUaOrder[] Items { get; set; } = [];
        /// <summary>
        /// ["<c>tradeType</c>"] Trade type
        /// </summary>
        [JsonPropertyName("tradeType")]
        public UnifiedAccountType AccountType { get; set; }
    }
}
