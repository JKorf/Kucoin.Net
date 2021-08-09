using System;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Converters;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Spot
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
        public decimal Quantity { get; set; }
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
        [JsonConverter(typeof(BizTypeConverter))]
        public KucoinBizType BizType { get; set; } = default!;
        /// <summary>
        /// The unique key for this activity 
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Additional info for this activity
        /// </summary>
        [JsonConverter(typeof(AccountActivityContextConverter))]
        public KucoinAccountActivityContext Context { get; set; } = default!;
        /// <summary>
        /// The currency of the activity
        /// </summary>
        public string Currency { get; set; } = string.Empty;
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
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// The id for the trade (for trades)
        /// </summary>
        public string TradeId { get; set; } = string.Empty;
        /// <summary>
        /// The symbol of the order (for trades)
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The transaction id (for withdrawal/deposit)
        /// </summary>
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// The txId (for orders)
        /// </summary>
        public string TxId { get; set; } = string.Empty;
        /// <summary>
        /// The Description (for pool-x staking rewards)
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }

    //[2] Deserialize JsonSerializationException: Error converting value "{"orderId":"s4369060","description":"pool-x staking rewards(2021/04/13)"}" to type 'Kucoin.Net.Objects.KucoinAccountActivityContext'. Path 'data.items[0].context', line 1, position 376.
}
