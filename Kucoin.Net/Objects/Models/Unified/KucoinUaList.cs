namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// List
    /// </summary>
    public record KucoinUaList<T>
    {
        /// <summary>
        /// ["<c>totalNumber</c>"] Total number of results
        /// </summary>
        [JsonPropertyName("totalNumber")]
        public int Total { get; set; }
        /// <summary>
        /// ["<c>totalPage</c>"] Total number of pages
        /// </summary>
        [JsonPropertyName("totalPage")]
        public int Pages { get; set; }
        /// <summary>
        /// ["<c>pageNumber</c>"] Current page number
        /// </summary>
        [JsonPropertyName("pageNumber")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// ["<c>pageSize</c>"] Page size
        /// </summary>
        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }
        /// <summary>
        /// ["<c>list</c>"] Data
        /// </summary>
        [JsonPropertyName("list")]
        public T Data { get; set; } = default!;
    }
}
