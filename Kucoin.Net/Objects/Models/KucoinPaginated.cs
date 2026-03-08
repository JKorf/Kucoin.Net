using System;


namespace Kucoin.Net.Objects.Models
{
    /// <summary>
    /// Page with results
    /// </summary>
    /// <typeparam name="T">Type of the items in the book</typeparam>
    [SerializationModel]
    public record KucoinPaginated<T>
    {
        /// <summary>
        /// ["<c>totalNum</c>"] The total number of results
        /// </summary>
        [JsonPropertyName("totalNum")]
        public int TotalItems { get; set; }
        /// <summary>
        /// ["<c>totalPage</c>"] The total number of pages
        /// </summary>
        [JsonPropertyName("totalPage")]
        public int TotalPages { get; set; }
        /// <summary>
        /// ["<c>pageSize</c>"] The amount of items per page
        /// </summary>
        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }
        /// <summary>
        /// ["<c>currentPage</c>"] The current page
        /// </summary>
        [JsonPropertyName("currentPage")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// ["<c>items</c>"] The items on this page
        /// </summary>
        [JsonPropertyName("items")]
        public T[] Items { get; set; } = Array.Empty<T>();
    }
}
