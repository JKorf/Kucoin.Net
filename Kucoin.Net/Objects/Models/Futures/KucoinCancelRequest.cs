﻿namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Cancel request
    /// </summary>
    public record KucoinCancelRequest
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("clientOid")]
        public string ClientOrderId { get; set; } = string.Empty;
    }
}
