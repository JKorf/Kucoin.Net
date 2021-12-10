using CryptoExchange.Net.Converters;
using Kucoin.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects.Margin
{
    /// <summary>
    /// Kucoin Lend Order (Active and Lent history)
    /// </summary>
    public class KucoinLendOrder
    {
        /// <summary>
        /// Lend order ID
        /// </summary>
        public string OrderId { get; set; } = string.Empty;

        /// <summary>
        /// Currency
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// Total size
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Size executed
        /// </summary>
        public decimal FilledSize { get; set; }

        /// <summary>
        /// Daily interest rate
        /// </summary>
        [JsonProperty("dailyIntRate")]
        public decimal DailyInterestRate { get; set; }

        /// <summary>
        /// Term (Unit: Day)
        /// </summary>
        public int Rate { get; set; }

        /// <summary>
        /// Order status
        /// </summary>
        [JsonConverter(typeof(LentOrderStatusConverter))]
        public KucoinLentOrderStatus? Status { get; set; }

        /// <summary>
        /// Time of the event (millisecond)
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime CreatedAt { get; set; }
    }
}
