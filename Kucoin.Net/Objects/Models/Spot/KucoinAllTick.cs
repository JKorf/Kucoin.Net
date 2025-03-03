﻿

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Tick info
    /// </summary>
    public record KucoinAllTick
    {
        /// <summary>
        /// The symbol of the tick
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Name of trading pairs, it would change after renaming
        /// </summary>
        [JsonPropertyName("symbolName")]
        public string SymbolName { get; set; } = string.Empty;
        /// <summary>
        /// The best ask price
        /// </summary>
        [JsonPropertyName("sell")]
        public decimal? BestAskPrice { get; set; }
        /// <summary>
        /// The quantity of the best ask
        /// </summary>
        [JsonPropertyName("bestAskSize")]
        public decimal? BestAskQuantity { get; set; }
        /// <summary>
        /// The best bid price
        /// </summary>
        [JsonPropertyName("buy")]
        public decimal? BestBidPrice { get; set; }
        /// <summary>
        /// The quantity of the best bid
        /// </summary>
        [JsonPropertyName("bestBidSize")]
        public decimal? BestBidQuantity { get; set; }
        /// <summary>
        /// The percentage change
        /// </summary>
        [JsonPropertyName("changeRate")]
        public decimal? ChangePercentage { get; set; }
        /// <summary>
        /// The price change
        /// </summary>
        [JsonPropertyName("changePrice")]
        public decimal? ChangePrice { get; set; }
        /// <summary>
        /// The highest price
        /// </summary>
        [JsonPropertyName("high")]
        public decimal? HighPrice { get; set; }
        /// <summary>
        /// The lowest price
        /// </summary>
        [JsonPropertyName("low")]
        public decimal? LowPrice { get; set; }
        /// <summary>
        /// The volume in this tick
        /// </summary>
        [JsonPropertyName("vol")]
        public decimal? Volume { get; set; }
        /// <summary>
        /// The value of the volume in this tick
        /// </summary>
        [JsonPropertyName("volValue")]
        public decimal? QuoteVolume { get; set; }
        /// <summary>
        /// The last trade price
        /// </summary>
        [JsonPropertyName("last")]
        public decimal? LastPrice { get; set; }
        /// <summary>
        /// The average trade price in the last 24 hours
        /// </summary>
        [JsonPropertyName("averagePrice")]
        public decimal? AveragePrice { get; set; }

        /// <summary>
        /// Basic Taker Fee
        /// </summary>
        [JsonPropertyName("takerFeeRate")]
        public decimal? TakerFeeRate { get; set; }
        /// <summary>
        /// Basic Maker Fee
        /// </summary>
        [JsonPropertyName("makerFeeRate")]
        public decimal? MakerFeeRate { get; set; }
        /// <summary>
        /// Taker Fee Coefficient
        /// </summary>
        [JsonPropertyName("takerCoefficient")]
        public decimal? TakerCoefficient { get; set; }
        /// <summary>
        /// Maker Fee Coefficient
        /// </summary>
        [JsonPropertyName("makerCoefficient")]
        public decimal? MakerCoefficient { get; set; }
    }
}
