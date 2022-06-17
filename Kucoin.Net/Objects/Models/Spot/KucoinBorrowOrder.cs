using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Converters;
using Kucoin.Net.Enums;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Kucoin Borrow Order
    /// </summary>
    public class KucoinBorrowOrder
    {
        /// <summary>
        /// The OrderId of the borrow order
        /// </summary>
        [JsonProperty("orderId")]
        public string Id { get; set; } = string.Empty;

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
        [JsonConverter(typeof(LoanStatusConverter))]
        public LoanStatus Status { get; set; }
    }

    /// <summary>
    /// Kucoin Borrow Execution details
    /// </summary>
    public class KucoinBorrowOrderDetails
    {
        /// <summary>
        /// The trade id of the borrow order
        /// </summary>
        [JsonProperty("tradeId")]
        public string TradeId { get; set; } = string.Empty;

        /// <summary>
        /// The Asset of the borrow order
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// Accrued interest
        /// </summary>
        [JsonProperty("accruedInterest")]
        public decimal AccruedInterest { get; set; }

        /// <summary>
        /// Daily interest rate
        /// </summary>
        [JsonProperty("dailyIntRate")]
        public decimal DailyInterestRate { get; set; }

        /// <summary>
        /// Total liabilities of the borrow order
        /// </summary>
        [JsonProperty("liability")]
        public decimal Liability { get; set; }

        /// <summary>
        /// The maturity date of the borrow order
        /// </summary>
        [JsonProperty("maturityTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime MaturityTime { get; set; }

        /// <summary>
        /// Outstanding principal to repay
        /// </summary>
        [JsonProperty("principal")]
        public decimal Principal { get; set; }

        /// <summary>
        /// Principal already repaid
        /// </summary>
        [JsonProperty("repaidSize")]
        public decimal RepaidSize { get; set; }

        /// <summary>
        /// Term of the borrow order
        /// </summary>
        [JsonProperty("term")]
        public string Term { get; set; } = string.Empty;

        /// <summary>
        /// Execution time of the borrow order
        /// </summary>
        [JsonProperty("createdAt"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreatedAt { get; set; }

    }
}
