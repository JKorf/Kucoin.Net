﻿using Kucoin.Net.Enums;


namespace Kucoin.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Futures stop order update
    /// </summary>
    public record KucoinStreamFuturesStopOrderUpdate: KucoinStreamStopOrderUpdateBase
    {
        /// <summary>
        /// Stop price type
        /// </summary>
        [JsonPropertyName("stopPriceType")]
        public StopPriceType StopPriceType { get; set; }

        /// <summary>
        /// Error info if there was an error with the order
        /// </summary>
        [JsonPropertyName("error")]
        public string? Error { get; set; }
    }
}
