﻿namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// User fee
    /// </summary>
    public record KucoinUserFee
    {
        /// <summary>
        /// Fee rate for trades as taker
        /// </summary>
        [JsonPropertyName("takerFeeRate")]
        public decimal TakerFeeRate { get; set; }
        /// <summary>
        /// Fee rate for trades as maker
        /// </summary>
        [JsonPropertyName("makerFeeRate")]
        public decimal MakerFeeRate { get; set; }
    }
}
