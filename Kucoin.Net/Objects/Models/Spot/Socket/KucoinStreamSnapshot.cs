using System;


namespace Kucoin.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Stream snapshot wrapper
    /// </summary>
    [SerializationModel]
    public record KucoinStreamSnapshotWrapper
    {
        /// <summary>
        /// ["<c>sequence</c>"] The sequence number of the update
        /// </summary>
        [JsonPropertyName("sequence")]
        public long Sequence { get; set; }

        /// <summary>
        /// ["<c>data</c>"] The data
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
        /// ["<c>trading</c>"] Whether the symbol is trading
        /// </summary>
        [JsonPropertyName("trading")]
        public bool Trading { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] The symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>buy</c>"] The current best bid
        /// </summary>
        [JsonPropertyName("buy")]
        public decimal? BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>sell</c>"] The current best ask
        /// </summary>
        [JsonPropertyName("sell")]
        public decimal? BestAskPrice { get; set; }

        /// <summary>
        /// ["<c>askSize</c>"] The current best ask quantity
        /// </summary>
        [JsonPropertyName("askSize")]
        public decimal? BestAskQuantity { get; set; }
        /// <summary>
        /// ["<c>bidSize</c>"] The current best bid quantity
        /// </summary>
        [JsonPropertyName("bidSize")]
        public decimal? BestBidQuantity { get; set; }
        /// <summary>
        /// ["<c>sort</c>"] Unknown
        /// </summary>
        [JsonPropertyName("sort")]
        public int Sort { get; set; }
        /// <summary>
        /// ["<c>volValue</c>"] The value of the volume
        /// </summary>
        [JsonPropertyName("volValue")]
        public decimal VolumeValue { get; set; }
        /// <summary>
        /// ["<c>vol</c>"] The volume
        /// </summary>
        [JsonPropertyName("vol")]
        public decimal Volume { get; set; }
        /// <summary>
        /// ["<c>baseCurrency</c>"] The base asset
        /// </summary>
        [JsonPropertyName("baseCurrency")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>market</c>"] The market name
        /// </summary>
        [JsonPropertyName("market")]
        public string Market { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quoteCurrency</c>"] The quote asset
        /// </summary>
        [JsonPropertyName("quoteCurrency")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbolCode</c>"] The symbol code
        /// </summary>
        [JsonPropertyName("symbolCode")]
        public string SymbolCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>datetime</c>"] The timestamp of the data
        /// </summary>
        [JsonPropertyName("datetime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>high</c>"] The highest price
        /// </summary>
        [JsonPropertyName("high")]
        public decimal? HighPrice { get; set; }
        /// <summary>
        /// ["<c>low</c>"] The lowest price
        /// </summary>
        [JsonPropertyName("low")]
        public decimal? LowPrice { get; set; }
        /// <summary>
        /// ["<c>close</c>"] The close price
        /// </summary>
        [JsonPropertyName("close")]
        public decimal? ClosePrice { get; set; }
        /// <summary>
        /// ["<c>open</c>"] The open price
        /// </summary>
        [JsonPropertyName("open")]
        public decimal? OpenPrice { get; set; }
        /// <summary>
        /// ["<c>lastTradedPrice</c>"] The last price
        /// </summary>
        [JsonPropertyName("lastTradedPrice")]
        public decimal? LastPrice { get; set; }
        /// <summary>
        /// ["<c>changePrice</c>"] The change price
        /// </summary>
        [JsonPropertyName("changePrice")]
        public decimal? ChangePrice { get; set; }
        /// <summary>
        /// ["<c>changeRate</c>"] The change percentage
        /// </summary>
        [JsonPropertyName("changeRate")]
        public decimal? ChangePercentage { get; set; }
        /// <summary>
        /// ["<c>averagePrice</c>"] Average price
        /// </summary>
        [JsonPropertyName("averagePrice")]
        public decimal? AveragePrice { get; set; }
        /// <summary>
        /// ["<c>board</c>"] Unknown
        /// </summary>
        [JsonPropertyName("board")]
        public int Board { get; set; }
        /// <summary>
        /// ["<c>mark</c>"] Unknown
        /// </summary>
        [JsonPropertyName("mark")]
        public int Mark { get; set; }
        /// <summary>
        /// ["<c>makerCoefficient</c>"] Maker coefficent
        /// </summary>
        [JsonPropertyName("makerCoefficient")]
        public decimal? MakerCoefficient { get; set; }
        /// <summary>
        /// ["<c>takerCoefficient</c>"] Taker coefficent
        /// </summary>
        [JsonPropertyName("takerCoefficient")]
        public decimal? TakerCoefficient { get; set; }
        /// <summary>
        /// ["<c>makerFeeRate</c>"] Maker fee rate
        /// </summary>
        [JsonPropertyName("makerFeeRate")]
        public decimal? MakerFeeRate { get; set; }
        /// <summary>
        /// ["<c>takerFeeRate</c>"] Taker fee rate
        /// </summary>
        [JsonPropertyName("takerFeeRate")]
        public decimal? TakerFeeRate { get; set; }
        /// <summary>
        /// ["<c>marginTrade</c>"] Margin trade
        /// </summary>
        [JsonPropertyName("marginTrade")]
        public bool? MarginTrade { get; set; }
        /// <summary>
        /// ["<c>markets</c>"] Markets
        /// </summary>
        [JsonPropertyName("markets")]
        public string[] Markets { get; set; } = new string[0];
        /// <summary>
        /// ["<c>marketChange1h</c>"] Change info last hour
        /// </summary>
        [JsonPropertyName("marketChange1h")]
        public KucoinMarketChange MarketChange1h { get; set; } = null!;
        /// <summary>
        /// ["<c>marketChange4h</c>"] Change info last 4 hours
        /// </summary>
        [JsonPropertyName("marketChange4h")]
        public KucoinMarketChange MarketChange4h { get; set; } = null!;
        /// <summary>
        /// ["<c>marketChange24h</c>"] Change info last 24 hours
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
        /// ["<c>changePrice</c>"] Change price
        /// </summary>
        [JsonPropertyName("changePrice")]
        public decimal ChangePrice { get; set; }
        /// <summary>
        /// ["<c>changeRate</c>"] Change percentage
        /// </summary>
        [JsonPropertyName("changeRate")]
        public decimal ChangeRate { get; set; }
        /// <summary>
        /// ["<c>high</c>"] High
        /// </summary>
        [JsonPropertyName("high")]
        public decimal High { get; set; }
        /// <summary>
        /// ["<c>low</c>"] Low
        /// </summary>
        [JsonPropertyName("low")]
        public decimal Low { get; set; }
        /// <summary>
        /// ["<c>open</c>"] Open
        /// </summary>
        [JsonPropertyName("open")]
        public decimal Open { get; set; }
        /// <summary>
        /// ["<c>vol</c>"] Volume
        /// </summary>
        [JsonPropertyName("vol")]
        public decimal Volume { get; set; }
        /// <summary>
        /// ["<c>volValue</c>"] Volume value
        /// </summary>
        [JsonPropertyName("volValue")]
        public decimal VolumeValue { get; set; }
    }
}
