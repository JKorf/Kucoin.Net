using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// List
    /// </summary>
    public record KucoinUaList<T>
    {
        /// <summary>
        /// Total number of results
        /// </summary>
        [JsonPropertyName("totalNumber")]
        public int Total { get; set; }
        /// <summary>
        /// Total number of pages
        /// </summary>
        [JsonPropertyName("totalPage")]
        public int Pages { get; set; }
        /// <summary>
        /// Current page number
        /// </summary>
        [JsonPropertyName("pageNumber")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// Page size
        /// </summary>
        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("list")]
        public T Data { get; set; } = default!;
    }
}
