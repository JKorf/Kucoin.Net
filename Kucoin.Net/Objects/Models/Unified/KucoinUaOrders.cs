using Kucoin.Net.Enums;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Orders
    /// </summary>
    public record KucoinUaOrders
    {
        /// <summary>
        /// Page number
        /// </summary>
        [JsonPropertyName("pageNumber")]
        public int PageNumber { get; set; }
        /// <summary>
        /// Page quantity
        /// </summary>
        [JsonPropertyName("pageSize")]
        public int? PageQuantity { get; set; }
        /// <summary>
        /// Total num
        /// </summary>
        [JsonPropertyName("totalNum")]
        public int? TotalNum { get; set; }
        /// <summary>
        /// Total page
        /// </summary>
        [JsonPropertyName("totalPage")]
        public int? TotalPage { get; set; }
        /// <summary>
        /// Items
        /// </summary>
        [JsonPropertyName("items")]
        public KucoinUaOrder[] Items { get; set; } = [];
        /// <summary>
        /// Trade type
        /// </summary>
        [JsonPropertyName("tradeType")]
        public UnifiedAccountType AccountType { get; set; }
    }
}
