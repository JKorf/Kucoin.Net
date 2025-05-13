using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;


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
        /// The total number of results
        /// </summary>
        [JsonPropertyName("totalNum")]
        public int TotalItems { get; set; }
        /// <summary>
        /// The total number of pages
        /// </summary>
        [JsonPropertyName("totalPage")]
        public int TotalPages { get; set; }
        /// <summary>
        /// The amount of items per page
        /// </summary>
        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }
        /// <summary>
        /// The current page
        /// </summary>
        [JsonPropertyName("currentPage")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// The items on this page
        /// </summary>
        [JsonPropertyName("items")]
        public T[] Items { get; set; } = Array.Empty<T>();
    }
}
