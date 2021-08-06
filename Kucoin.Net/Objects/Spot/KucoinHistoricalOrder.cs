using CryptoExchange.Net.Converters;
using Kucoin.Net.Converts;
using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects
{
    /// <summary>
    /// Historical order info
    /// </summary>
    public class KucoinHistoricalOrder
    {
        /// <summary>
        /// The id of the order
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// The symbol of the order
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The price of the order
        /// </summary>
        [JsonProperty("dealPrice")]
        public decimal Price { get; set; }
        /// <summary>
        /// The value of the order
        /// </summary>
        [JsonProperty("dealValue")]
        public decimal Value { get; set; }
        /// <summary>
        /// The quantity of the order
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The fee of the order
        /// </summary>
        public decimal Fee { get; set; }
        /// <summary>
        /// The side of the order
        /// </summary>        
        [JsonConverter(typeof(OrderSideConverter))]
        public KucoinOrderSide Side { get; set; }
        /// <summary>
        /// The time the order was created
        /// </summary>
        [JsonConverter(typeof(TimestampSecondsConverter))]
        public DateTime CreatedAt { get; set; }
    }
}
