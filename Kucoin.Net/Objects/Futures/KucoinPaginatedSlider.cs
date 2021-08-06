using Newtonsoft.Json;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Futures
{
    /// <summary>
    /// Data container for paged lists
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class KucoinPaginatedSlider<T>
    {
        /// <summary>
        /// If there are more pages
        /// </summary>
        public bool HasMore { get; set; }
        /// <summary>
        /// Data list
        /// </summary>
        [JsonProperty("dataList")]
        public IEnumerable<T> Data { get; set; }
    }
}
