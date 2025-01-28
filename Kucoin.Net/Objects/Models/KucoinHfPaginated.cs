using System;
using System.Collections.Generic;


namespace Kucoin.Net.Objects.Models
{
    /// <summary>
    /// Page with results
    /// </summary>
    /// <typeparam name="T">Type of the items in the book</typeparam>
    public record KucoinHfPaginated<T>
    {
        /// <summary>
        /// The last result id
        /// </summary>
        [JsonPropertyName("lastId")]
        public long LastId { get; set; }
        /// <summary>
        /// The items on this page
        /// </summary>
        [JsonPropertyName("items")]
        public IEnumerable<T> Items { get; set; } = Array.Empty<T>();
    }
}
