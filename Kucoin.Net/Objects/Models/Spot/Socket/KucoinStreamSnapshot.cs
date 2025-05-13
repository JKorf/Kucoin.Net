using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;


namespace Kucoin.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Stream snapshot wrapper
    /// </summary>
    [SerializationModel]
    public record KucoinStreamSnapshotWrapper
    {
        /// <summary>
        /// The sequence number of the update
        /// </summary>
        [JsonPropertyName("sequence")]
        public long Sequence { get; set; }

        /// <summary>
        /// The data
        /// </summary>
        [JsonPropertyName("data")]
        public KucoinStreamSnapshot Data { get; set; } = default!;
    }

    /// <summary>
    /// Stream snapshot
    /// </summary>
    [SerializationModel]
    public record KucoinStreamSnapshot
    {
        /// <summary>
        /// Whether the symbol is trading
        /// </summary>
        [JsonPropertyName("trading")]
        public bool Trading { get; set; }
        /// <summary>
        /// The symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The current best bid
        /// </summary>
        [JsonPropertyName("buy")]
        public decimal? BestBidPrice { get; set; }
        /// <summary>
        /// The current best ask
        /// </summary>
        [JsonPropertyName("sell")]
        public decimal? BestAskPrice { get; set; }

        /// <summary>
        /// The current best ask quantity
        /// </summary>
        [JsonPropertyName("askSize")]
        public decimal? BestAskQuantity { get; set; }
        /// <summary>
        /// The current best bid quantity
        /// </summary>
        [JsonPropertyName("bidSize")]
        public decimal? BestBidQuantity { get; set; }
        /// <summary>
        /// Unknown
        /// </summary>
        [JsonPropertyName("sort")]
        public int Sort { get; set; }
        /// <summary>
        /// The value of the volume
        /// </summary>
        [JsonPropertyName("volValue")]
        public decimal VolumeValue { get; set; }
        /// <summary>
        /// The volume
        /// </summary>
        [JsonPropertyName("vol")]
        public decimal Volume { get; set; }
        /// <summary>
        /// The base asset
        /// </summary>
        [JsonPropertyName("baseCurrency")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// The market name
        /// </summary>
        [JsonPropertyName("market")]
        public string Market { get; set; } = string.Empty;
        /// <summary>
        /// The quote asset
        /// </summary>
        [JsonPropertyName("quoteCurrency")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// The symbol code
        /// </summary>
        [JsonPropertyName("symbolCode")]
        public string SymbolCode { get; set; } = string.Empty;
        /// <summary>
        /// The timestamp of the data
        /// </summary>
        [JsonPropertyName("datetime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
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
        /// The close price
        /// </summary>
        [JsonPropertyName("close")]
        public decimal? ClosePrice { get; set; }
        /// <summary>
        /// The open price
        /// </summary>
        [JsonPropertyName("open")]
        public decimal? OpenPrice { get; set; }
        /// <summary>
        /// The last price
        /// </summary>
        [JsonPropertyName("lastTradedPrice")]
        public decimal? LastPrice { get; set; }
        /// <summary>
        /// The change price
        /// </summary>
        [JsonPropertyName("changePrice")]
        public decimal? ChangePrice { get; set; }
        /// <summary>
        /// The change percentage
        /// </summary>
        [JsonPropertyName("changeRate")]
        public decimal? ChangePercentage { get; set; }
        /// <summary>
        /// Average price
        /// </summary>
        [JsonPropertyName("averagePrice")]
        public decimal? AveragePrice { get; set; }
        /// <summary>
        /// Unknown
        /// </summary>
        [JsonPropertyName("board")]
        public int Board { get; set; }
        /// <summary>
        /// Unknown
        /// </summary>
        [JsonPropertyName("mark")]
        public int Mark { get; set; }
        /// <summary>
        /// Maker coefficent
        /// </summary>
        [JsonPropertyName("makerCoefficient")]
        public decimal? MakerCoefficient { get; set; }
        /// <summary>
        /// Taker coefficent
        /// </summary>
        [JsonPropertyName("takerCoefficient")]
        public decimal? TakerCoefficient { get; set; }
        /// <summary>
        /// Maker fee rate
        /// </summary>
        [JsonPropertyName("makerFeeRate")]
        public decimal? MakerFeeRate { get; set; }
        /// <summary>
        /// Taker fee rate
        /// </summary>
        [JsonPropertyName("takerFeeRate")]
        public decimal? TakerFeeRate { get; set; }
        /// <summary>
        /// Margin trade
        /// </summary>
        [JsonPropertyName("marginTrade")]
        public bool? MarginTrade { get; set; }
        /// <summary>
        /// Markets
        /// </summary>
        [JsonPropertyName("markets")]
        public string[] Markets { get; set; } = new string[0];
        /// <summary>
        /// Change info last hour
        /// </summary>
        [JsonPropertyName("marketChange1h")]
        public KucoinMarketChange MarketChange1h { get; set; } = null!;
        /// <summary>
        /// Change info last 4 hours
        /// </summary>
        [JsonPropertyName("marketChange4h")]
        public KucoinMarketChange MarketChange4h { get; set; } = null!;
        /// <summary>
        /// Change info last 24 hours
        /// </summary>
        [JsonPropertyName("marketChange24h")]
        public KucoinMarketChange MarketChange24h { get; set; } = null!;
    }

    /// <summary>
    /// Change info
    /// </summary>
    [SerializationModel]
    public record KucoinMarketChange
    {
        /// <summary>
        /// Change price
        /// </summary>
        [JsonPropertyName("changePrice")]
        public decimal ChangePrice { get; set; }
        /// <summary>
        /// Change percentage
        /// </summary>
        [JsonPropertyName("changeRate")]
        public decimal ChangeRate { get; set; }
        /// <summary>
        /// High
        /// </summary>
        [JsonPropertyName("high")]
        public decimal High { get; set; }
        /// <summary>
        /// Low
        /// </summary>
        [JsonPropertyName("low")]
        public decimal Low { get; set; }
        /// <summary>
        /// Open
        /// </summary>
        [JsonPropertyName("open")]
        public decimal Open { get; set; }
        /// <summary>
        /// Volume
        /// </summary>
        [JsonPropertyName("vol")]
        public decimal Volume { get; set; }
        /// <summary>
        /// Volume value
        /// </summary>
        [JsonPropertyName("volValue")]
        public decimal VolumeValue { get; set; }
    }
}
