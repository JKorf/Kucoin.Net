using System;
using System.Collections.Generic;
using Newtonsoft.Json;

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
        public bool HasMore { get; set; }
        /// <summary>
        /// Data list
        /// </summary>
        [JsonProperty("dataList")]
        public IEnumerable<T> Data { get; set; } = Array.Empty<T>();
    }
}
