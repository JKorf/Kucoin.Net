using CryptoExchange.Net.Converters;
using Kucoin.Net.Converts;
using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects
{
    /// <summary>
    /// Account activity info
    /// </summary>
    public class KucoinAccountActivity
    {
        /// <summary>
        /// Creation timestamp
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// The amount of the activity
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// The remaining balance after the activity
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// The fee of the activity
        /// </summary>
        public decimal Fee { get; set; }
        /// <summary>
        /// The type of activity
        /// </summary>
        public string BizType { get; set; }

        /// <summary>
        /// Additional info for this activity
        /// </summary>
        public KucoinAccountActivityContext Context { get; set; }

        /// <summary>
        /// The currency of the activity
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// The direction of the activity
        /// </summary>
        [JsonConverter(typeof(AccountDirectionConverter))]
        public KucoinAccountDirection Direction { get; set; }
    }

    /// <summary>
    /// Account activity details
    /// </summary>
    public class KucoinAccountActivityContext
    {
        /// <summary>
        /// The id for the order
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// The id for the trade (for trades)
        /// </summary>
        public string TradeId { get; set; }
        /// <summary>
        /// The symbol of the order (for trades)
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// The transaction id (for withdrawal/deposit)
        /// </summary>
        public string TransactionId { get; set; }
    }
}
