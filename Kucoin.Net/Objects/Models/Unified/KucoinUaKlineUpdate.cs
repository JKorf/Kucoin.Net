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
        /// Interval
        /// </summary>
        [JsonPropertyName("i")]
        public string Interval { get; set; } = string.Empty;
        /// <summary>
        /// Symbol name
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Open time
        /// </summary>
        [JsonPropertyName("O")]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// Close time
        /// </summary>
        [JsonPropertyName("C")]
        public DateTime CloseTime { get; set; }
        /// <summary>
        /// Open price
        /// </summary>
        [JsonPropertyName("o")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// Close price
        /// </summary>
        [JsonPropertyName("c")]
        public decimal ClosePrice { get; set; }
        /// <summary>
        /// High price
        /// </summary>
        [JsonPropertyName("h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// Low price
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// Volume
        /// </summary>
        [JsonPropertyName("v")]
        public decimal Volume { get; set; }
        /// <summary>
        /// Transaction amount
        /// </summary>
        [JsonPropertyName("a")]
        public decimal TransactionAmount { get; set; }
        /// <summary>
        /// Whether this tick is the start of new kline
        /// </summary>
        [JsonPropertyName("S")]
        public bool Start { get; set; }
    }
}
