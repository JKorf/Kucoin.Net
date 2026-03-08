using System;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// 24 hours stats
    /// </summary>
    [SerializationModel]
    public record Kucoin24HourStat
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol the stat is for
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>high</c>"] The highest price in the last 24 hours
        /// </summary>
        [JsonPropertyName("high")]
        public decimal? HighPrice { get; set; }
        /// <summary>
        /// ["<c>low</c>"] The lowest price in the last 24 hours
        /// </summary>
        [JsonPropertyName("low")]
        public decimal? LowPrice { get; set; }
        /// <summary>
        /// ["<c>vol</c>"] The volume of the past 24 hours
        /// </summary>
        [JsonPropertyName("vol")]
        public decimal? Volume { get; set; }
        /// <summary>
        /// ["<c>volValue</c>"] The value of the volume in the past 24 hours
        /// </summary>
        [JsonPropertyName("volValue")]
        public decimal? QuoteVolume { get; set; }
        /// <summary>
        /// ["<c>last</c>"] The last trade price
        /// </summary>
        [JsonPropertyName("last")]
        public decimal? LastPrice { get; set; }
        /// <summary>
        /// ["<c>buy</c>"] The best ask price
        /// </summary>
        [JsonPropertyName("buy")]
        public decimal? BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>sell</c>"] The best bid price
        /// </summary>
        [JsonPropertyName("sell")]
        public decimal? BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>changePrice</c>"] The price change since 24 hours ago
        /// </summary>
        [JsonPropertyName("changePrice")]
        public decimal? ChangePrice { get; set; }
        /// <summary>
        /// ["<c>changeRate</c>"] The percentage change since 24 hours ago
        /// </summary>
        [JsonPropertyName("changeRate")]
        public decimal? ChangePercentage { get; set; }
        /// <summary>
        /// ["<c>time</c>"] The timestamp of the data
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter)), JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>averagePrice</c>"] The average trade price in the last 24 hours
        /// </summary>
        [JsonPropertyName("averagePrice")]
        public decimal? AveragePrice { get; set; }

        /// <summary>
        /// ["<c>takerFeeRate</c>"] Basic Taker Fee
        /// </summary>
        [JsonPropertyName("takerFeeRate")]
        public decimal? TakerFeeRate { get; set; }
        /// <summary>
        /// ["<c>makerFeeRate</c>"] Basic Maker Fee
        /// </summary>
        [JsonPropertyName("makerFeeRate")]
        public decimal? MakerFeeRate { get; set; }
        /// <summary>
        /// ["<c>takerCoefficient</c>"] Taker Fee Coefficient
        /// </summary>
        [JsonPropertyName("takerCoefficient")]
        public decimal? TakerCoefficient { get; set; }
        /// <summary>
        /// ["<c>makerCoefficient</c>"] Maker Fee Coefficient
        /// </summary>
        [JsonPropertyName("makerCoefficient")]
        public decimal? MakerCoefficient { get; set; }
    }
}
