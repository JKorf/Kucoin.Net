using CryptoExchange.Net.Converters;
using Kucoin.Net.Enums;
using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Lend order info
    /// </summary>
    public class KucoinLendOrder
    {
        /// <summary>
        /// Order id
        /// </summary>
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Asset being lend
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Filled quantity
        /// </summary>
        [JsonProperty("filledSize")]
        public decimal FilledQuantity { get; set; }
        /// <summary>
        /// Daily interest rate, 0.01 = 1%
        /// </summary>
        [JsonProperty("dailyIntRate")]
        public decimal DailyInterestRate { get; set; }
        /// <summary>
        /// Term in days
        /// </summary>
        public int Term { get; set; }
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonProperty("createdAt")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Status of the order
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public LendStatus? Status { get; set; }
    }
}
