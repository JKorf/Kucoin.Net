using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Spot
{
    /// <summary>
    /// 24 hours stats
    /// </summary>
    public class Kucoin24HourStat
    {
        /// <summary>
        /// The symbol the stat is for
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The highest price in the last 24 hours
        /// </summary>
        public decimal? High { get; set; }
        /// <summary>
        /// The lowest price in the last 24 hours
        /// </summary>
        public decimal? Low { get; set; }
        /// <summary>
        /// The volume of the past 24 hours
        /// </summary>
        [JsonProperty("vol")]
        public decimal? Volume { get; set; }
        /// <summary>
        /// The value of the volume in the past 24 hours
        /// </summary>
        [JsonProperty("volValue")]
        public decimal? VolumeValue { get; set; }
        /// <summary>
        /// The last trade price
        /// </summary>
        public decimal? Last { get; set; }
        /// <summary>
        /// The best ask price
        /// </summary>
        [JsonProperty("buy")]
        public decimal? BestAsk { get; set; }
        /// <summary>
        /// The best bid price
        /// </summary>
        [JsonProperty("sell")]
        public decimal? BestBid { get; set; }
        /// <summary>
        /// The price change since 24 hours ago
        /// </summary>
        public decimal? ChangePrice { get; set; }
        /// <summary>
        /// The percentage change since 24 hours ago
        /// </summary>
        [JsonProperty("changeRate")]
        public decimal? ChangePercentage { get; set; }
        /// <summary>
        /// The timestamp of the data
        /// </summary>
        [JsonConverter(typeof(TimestampConverter)), JsonProperty("time")]
        public DateTime Timestamp { get; set; }
    }
}
