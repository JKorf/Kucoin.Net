using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Converts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kucoin.Net.Objects
{
    /// <summary>
    /// Order info
    /// </summary>
    public class KucoinOrderBase
    {
        /// <summary>
        /// The id of the order
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// the symbol of the order
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonConverter(typeof(OrderTypeConverter))]
        public KucoinOrderType Type { get; set; }
        /// <summary>
        /// The side of the order
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public KucoinOrderSide Side { get; set; }
        /// <summary>
        /// The price of the order
        /// </summary>
        public decimal? Price { get; set; }
        /// <summary>
        /// The quantity of the order
        /// </summary>
        [JsonProperty("size")]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// Whether the stop condition is triggered
        /// </summary>
        public bool StopTriggered { get; set; }
        /// <summary>
        /// The stop price
        /// </summary>
        public decimal? StopPrice { get; set; }

        /// <summary>
        /// The time in force of the order
        /// </summary>
        [JsonConverter(typeof(TimeInForceConverter))]
        public KucoinTimeInForce TimeInForce { get; set; }
        /// <summary>
        /// Whether the order is post only
        /// </summary>
        public bool PostOnly { get; set; }
        /// <summary>
        /// Whether the order is hidden
        /// </summary>
        public bool Hidden { get; set; }
        /// <summary>
        /// Whether it is an iceberg order
        /// </summary>
        public bool Iceberg { get; set; }
        /// <summary>
        /// The max visible size of the iceberg
        /// </summary>
        [JsonProperty("visibleSize")]
        public decimal? VisibleIcebergSize { get; set; }
        /// <summary>
        /// The client order id
        /// </summary>
        [JsonProperty("clientOid")]
        public string ClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// Remark for the order
        /// </summary>
        [JsonOptionalProperty]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// Whether the order is active
        /// </summary>
        [JsonOptionalProperty]
        public bool? IsActive { get; set; }
        /// <summary>
        /// If there is a cancel request for this order
        /// </summary>
        public bool CancelExist { get; set; }
        /// <summary>
        /// The time the order was created
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// The self trade prevention type
        /// </summary>
        [JsonProperty("stp"), JsonConverter(typeof(SelfTradePreventionConverter))]
        public KucoinSelfTradePrevention? SelfTradePrevention { get; set; }
    }
}
