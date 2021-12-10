using CryptoExchange.Net.Converters;
using Kucoin.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Margin
{
    /// <summary>
    /// Kucoin Borrow Order
    /// </summary>
    public class KucoinBorrowOrder
    {
        /// <summary>
        /// The OrderId of the borrow order
        /// </summary>
        public string OrderId { get; set; } = string.Empty;

        /// <summary>
        /// The asset of the borrow order
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// Total size of the borrow order
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Size executed
        /// </summary>
        public decimal Filled { get; set; }

        /// <summary>
        /// The list of Borrow Order Details
        /// </summary>
        [JsonProperty("matchList")]
        public IEnumerable<KucoinBorrowOrderDetails> ExecutionDetails { get; set; } = Array.Empty<KucoinBorrowOrderDetails>();

        /// <summary>
        /// Status of request
        /// </summary>
        [JsonConverter(typeof(BorrowStatusConverter))]
        public KucoinBorrowStatus Status { get; set; }
    }

    /// <summary>
    /// Kucoin Borrow Execution details
    /// </summary>
    public class KucoinBorrowOrderDetails
    {
        /// <summary>
        /// The Asset of the borrow order
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// Daily interest rate
        /// </summary>
        [JsonProperty("dailyIntRate")]
        public decimal DailyInterestRate { get; set; }

        /// <summary>
        /// Total size of the borrow order
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Term of the borrow order
        /// </summary>
        public string Term { get; set; } = string.Empty;

        /// <summary>
        /// The timestamp of the data
        /// </summary>
        [JsonProperty("timestamp"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// The trade id of the borrow order
        /// </summary>
        public string TradeId { get; set; } = string.Empty;
    }
}
