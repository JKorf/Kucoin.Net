using Kucoin.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Kline update
    /// </summary>
    public record KucoinUaKlineUpdate
    {
        /// <summary>
        /// ["<c>i</c>"] Interval
        /// </summary>
        [JsonPropertyName("i")]
        public string Interval { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>s</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>O</c>"] Open time
        /// </summary>
        [JsonPropertyName("O")]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// ["<c>C</c>"] Close time
        /// </summary>
        [JsonPropertyName("C")]
        public DateTime CloseTime { get; set; }
        /// <summary>
        /// ["<c>o</c>"] Open price
        /// </summary>
        [JsonPropertyName("o")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// ["<c>c</c>"] Close price
        /// </summary>
        [JsonPropertyName("c")]
        public decimal ClosePrice { get; set; }
        /// <summary>
        /// ["<c>h</c>"] High price
        /// </summary>
        [JsonPropertyName("h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// ["<c>l</c>"] Low price
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// ["<c>v</c>"] Volume
        /// </summary>
        [JsonPropertyName("v")]
        public decimal Volume { get; set; }
        /// <summary>
        /// ["<c>a</c>"] Transaction amount
        /// </summary>
        [JsonPropertyName("a")]
        public decimal TransactionAmount { get; set; }
        /// <summary>
        /// ["<c>S</c>"] Whether this tick is the start of new kline
        /// </summary>
        [JsonPropertyName("S")]
        public bool Start { get; set; }
    }
}
