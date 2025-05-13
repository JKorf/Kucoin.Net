using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Converters;


namespace Kucoin.Net.Objects.Models.Futures.Socket
{
    [SerializationModel]
    internal record KucoinStreamFuturesKlineUpdate
    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        [JsonPropertyName("candles")]
        public KucoinStreamFuturesKline Klines { get; set; } = null!;
    }

    /// <summary>
    /// Kline info
    /// </summary>
    [JsonConverter(typeof(ArrayConverter<KucoinStreamFuturesKline>))]
    [SerializationModel]
    public record KucoinStreamFuturesKline
    {
        /// <summary>
        /// The start time of the kline
        /// </summary>
        [ArrayProperty(0), JsonConverter(typeof(DateTimeConverter))]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// The open price
        /// </summary>
        [ArrayProperty(1)]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// The close price
        /// </summary>
        [ArrayProperty(2)]
        public decimal ClosePrice { get; set; }
        /// <summary>
        /// The highest price during this kline
        /// </summary>
        [ArrayProperty(3)]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// The lowest price during this kline
        /// </summary>
        [ArrayProperty(4)]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// The volume of the kline
        /// </summary>
        [ArrayProperty(6)]
        public decimal Volume { get; set; }
    }
}
