using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Mark price update
    /// </summary>
    public record class KucoinUaMarkPriceUpdate
    {
        /// <summary>
        /// ["<c>s</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>mp</c>"] Mark price
        /// </summary>
        [JsonPropertyName("mp")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// ["<c>ip</c>"] Index price
        /// </summary>
        [JsonPropertyName("ip")]
        public decimal IndexPrice { get; set; }
        /// <summary>
        /// ["<c>ip</c>"] Open interest
        /// </summary>
        [JsonPropertyName("oi")]
        public decimal OpenInterest { get; set; }
        /// <summary>
        /// ["<c>ts</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
    }
}
