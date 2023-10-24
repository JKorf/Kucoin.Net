using CryptoExchange.Net.Converters;
using Kucoin.Net.Converters;
using Kucoin.Net.Enums;
using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// New Borrow order
    /// </summary>
    public class KucoinNewBorrowOrder
    {
        /// <summary>
        /// The id of the new borrow order
        /// </summary>
        [JsonProperty("orderId")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Currency of current Order
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;
    }

    public class MarginBorrowHistory
    {

        [JsonProperty("orderNo")]
        public string Id { get; set; }

        /// <summary>
        /// The asset of the borrow order
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;

        public string Symbol { get; set; }
        /// <summary>
        /// Initiated borrowing amount
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Actual borrowed amount
        /// </summary>
        [JsonProperty("actualSize")]
        public decimal Filled { get; set; }
        /// <summary>
        /// Status of request
        /// </summary>
        [JsonConverter(typeof(BorrowStatusConverter))]
        public BorrowStatus Status { get; set; }

        [JsonProperty("createdTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateDate { get; set; }


    }

    public class MarginRepayHistory
    {
        [JsonProperty("orderNo")]
        public string Id { get; set; }

        /// <summary>
        /// The asset of the borrow order
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;

        public string Symbol { get; set; }

        /// <summary>
        /// Initiated borrowing amount
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }

        public decimal Principal { get; set; }
        public decimal Interest { get; set; }

        [JsonConverter(typeof(RepayStatusConverter))]
        public RepayStatus Status { get; set; }

        [JsonProperty("createdTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateDate { get; set; }


    }
}
