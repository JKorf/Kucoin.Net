using System;


namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Data container for paged lists
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [SerializationModel]
    public record KucoinPaginatedSlider<T>
    {
        /// <summary>
        /// ["<c>hasMore</c>"] If there are more pages
        /// </summary>
        [JsonPropertyName("hasMore")]
        public bool HasMore { get; set; }
        /// <summary>
        /// ["<c>dataList</c>"] Data list
        /// </summary>
        [JsonPropertyName("dataList")]
        public T[] Data { get; set; } = Array.Empty<T>();
    }
}
