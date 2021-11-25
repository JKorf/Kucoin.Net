using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Stream snapshot wrapper
    /// </summary>
    public class KucoinStreamSnapshotWrapper
    {
        /// <summary>
        /// The sequence number of the update
        /// </summary>
        public long Sequence { get; set; }

        /// <summary>
        /// The data
        /// </summary>
        public KucoinStreamSnapshot Data { get; set; } = default!;
    }

    /// <summary>
    /// Stream snapshot
    /// </summary>
    public class KucoinStreamSnapshot
    {
        /// <summary>
        /// Whether the symbol is trading
        /// </summary>
        public bool Trading { get; set; }
        /// <summary>
        /// The symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The current best bid
        /// </summary>
        [JsonProperty("buy")]
        public decimal? BestBidPrice { get; set; }
        /// <summary>
        /// The current best ask
        /// </summary>
        [JsonProperty("sell")]
        public decimal? BestAskPrice { get; set; }

        /// <summary>
        /// Unknown
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// The value of the volume
        /// </summary>
        [JsonProperty("volValue")]
        public decimal VolumeValue { get; set; }
        /// <summary>
        /// The volume
        /// </summary>
        [JsonProperty("vol")]
        public decimal Volume { get; set; }
        /// <summary>
        /// The base asset
        /// </summary>
        [JsonProperty("baseCurrency")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// The market name
        /// </summary>
        public string Market { get; set; } = string.Empty;
        /// <summary>
        /// The quote asset
        /// </summary>
        [JsonProperty("quoteCurrency")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// The symbol code
        /// </summary>
        public string SymbolCode { get; set; } = string.Empty;
        /// <summary>
        /// The timestamp of the data
        /// </summary>
        [JsonProperty("dateTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// The highest price
        /// </summary>
        [JsonProperty("high")]
        public decimal? HighPrice { get; set; }
        /// <summary>
        /// The lowest price
        /// </summary>
        [JsonProperty("low")]
        public decimal? LowPrice { get; set; }
        /// <summary>
        /// The last price
        /// </summary>
        [JsonProperty("lastTradedPrice")]
        public decimal? LastPrice { get; set; }
        /// <summary>
        /// The change price
        /// </summary>
        public decimal? ChangePrice { get; set; }
        /// <summary>
        /// The change percentage
        /// </summary>
        [JsonProperty("changeRate")]
        public decimal? ChangePercentage { get; set; }
        /// <summary>
        /// Unknown
        /// </summary>
        public int Board { get; set; }
        /// <summary>
        /// Unknown
        /// </summary>
        public int Mark { get; set; }
    }
}
