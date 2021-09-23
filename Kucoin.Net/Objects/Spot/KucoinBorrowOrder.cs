using CryptoExchange.Net.Converters;
using CryptoExchange.Net.ExchangeInterfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kucoin.Net.Objects.Spot
{
    /// <summary>
    /// Kucoin Borrow Order
    /// </summary>
    public class KucoinBorrowOrder
    {
        /// <summary>
        /// The OrderId of the borrow order
        /// </summary>
        public string OrderId { get; set; } =  string.Empty;

        /// <summary>
        /// The currency of the borrow order
        /// </summary>
        public string Currency { get; set; } = string.Empty;

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
        public string Status { get; set; } = string.Empty;
    }

    /// <summary>
    /// Kucoin Borrow Execution details
    /// </summary>
    public class KucoinBorrowOrderDetails
    {
        /// <summary>
        /// The currency of the borrow order
        /// </summary>
        public string Currency { get; set; } = string.Empty;

        /// <summary>
        /// Daily interest rate
        /// </summary>
        public string DailyIntRate { get; set; } = string.Empty;

        /// <summary>
        /// Total size of the borrow order
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Total size of the borrow order
        /// </summary>
        public string Term { get; set; } = string.Empty;

        /// <summary>
        /// The timestamp of the data
        /// </summary>
        [JsonProperty("timestamp"), JsonConverter(typeof(TimestampNanoSecondsConverter))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// The trade id of the borrow order
        /// </summary>
        public string TradeId { get; set; } = string.Empty;
    }
}
