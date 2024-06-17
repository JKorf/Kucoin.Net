using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Converters;
using Kucoin.Net.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// The order model in bulk order creation response
    /// </summary>
    public class KucoinBulkMinimalResponseEntry
    {
        /// <summary>
        /// The id of the order
        /// </summary>
        [JsonProperty("id")]
        public string OrderId { get; set; } = string.Empty;        
        /// <summary>
        /// The cause of failure
        /// </summary>
        [JsonProperty("failMsg")]
        public string? Error { get; set; }
        /// <summary>
        /// Successful
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }
#warning Check what the actual response is
    }
}
