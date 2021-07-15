using CryptoExchange.Net.Converters;
using Kucoin.Net.Converts;
using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects.Sockets
{
    /// <summary>
    /// Match info
    /// </summary>
    public class KucoinStreamMatch
    {
        /// <summary>
        /// The sequence of the match
        /// </summary>
        public long Sequence { get; set; }
        /// <summary>
        /// The symbol the match is for
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The side of the match
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public KucoinOrderSide Side { get; set; }
        /// <summary>
        /// The quantity of the match
        /// </summary>
        [JsonProperty("Size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The price of the match
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// The taker order id
        /// </summary>
        public string TakerOrderId { get; set; } = string.Empty;
        /// <summary>
        /// The maker order id
        /// </summary>
        public string MakerOrderId { get; set; } = string.Empty;
        /// <summary>
        /// The timestamp of the match
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(TimestampNanoSecondsConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// The type
        /// </summary>
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// The id of the trade
        /// </summary>
        public string TradeId { get; set; } = string.Empty;
    }
}
