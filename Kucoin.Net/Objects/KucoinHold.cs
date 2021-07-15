using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects
{
    /// <summary>
    /// Hold info
    /// </summary>
    public class KucoinHold
    {
        /// <summary>
        /// The currency of the hold
        /// </summary>
        public string Currency { get; set; } = string.Empty;
        /// <summary>
        /// The quantity of the hold
        /// </summary>
        [JsonProperty("holdAmount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The type the hold is for
        /// </summary>
        public string BizType { get; set; } = string.Empty;
        /// <summary>
        /// The order id of the hold
        /// </summary>
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// The time the hold was created
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// The time the hold was last updated
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime UpdatedAt { get; set; }
    }
}
