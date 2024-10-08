﻿using Kucoin.Net.Objects.Models.Spot;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Order result
    /// </summary>
    public record KucoinFuturesOrderResult: KucoinOrderId
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Result code, 200000 is success
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }
        /// <summary>
        /// Result message
        /// </summary>
        [JsonProperty("msg")]
        public string Message { get; set; } = string.Empty;
    }
}
