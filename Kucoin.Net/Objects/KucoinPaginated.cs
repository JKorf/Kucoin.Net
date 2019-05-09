using Newtonsoft.Json;

namespace Kucoin.Net.Objects
{
    public class KucoinPaginated<T>
    {
        /// <summary>
        /// The total number of results
        /// </summary>
        [JsonProperty("totalNum")]
        public int TotalItems { get; set; }
        /// <summary>
        /// The total number of pages
        /// </summary>
        [JsonProperty("totalPage")]
        public int TotalPages { get; set; }
        /// <summary>
        /// The amount of items per page
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// The current page
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// The items on this page
        /// </summary>
        public T[] Items { get; set; }
    }
}
