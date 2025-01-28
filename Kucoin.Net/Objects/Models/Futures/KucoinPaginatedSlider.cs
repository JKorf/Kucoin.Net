using System;
using System.Collections.Generic;


namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Data container for paged lists
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public record KucoinPaginatedSlider<T>
    {
        /// <summary>
        /// If there are more pages
        /// </summary>
        [JsonPropertyName("hasMore")]
        public bool HasMore { get; set; }
        /// <summary>
        /// Data list
        /// </summary>
        [JsonPropertyName("dataList")]
        public IEnumerable<T> Data { get; set; } = Array.Empty<T>();
    }
}
