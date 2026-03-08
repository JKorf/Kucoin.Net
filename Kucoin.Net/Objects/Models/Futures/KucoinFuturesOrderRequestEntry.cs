using Kucoin.Net.Enums;

using System;

namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Futures order request
    /// </summary>
    [SerializationModel]
    public record KucoinFuturesOrderRequestEntry
    {
        /// <summary>
        /// ["<c>clientOid</c>"] Client order id
        /// </summary>
        [JsonPropertyName("clientOid"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? ClientOrderId { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonConverter(typeof(DecimalStringWriterConverter))]
        public decimal? Leverage { get; set; }
        /// <summary>
        /// ["<c>size</c>"] Amount of contracts to buy or sell, one of `Quantity`, `QuantityInBaseAsset` or `QuantityInQuoteAsset` should be provided
        /// </summary>
        [JsonPropertyName("size"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int? Quantity { get; set; }
        /// <summary>
        /// ["<c>qty</c>"] Quantity in base asset, one of `Quantity`, `QuantityInBaseAsset` or `QuantityInQuoteAsset` should be provided
        /// </summary>
        [JsonPropertyName("qty"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? QuantityInBaseAsset { get; set; }
        /// <summary>
        /// ["<c>valueQty</c>"] Quantity in quote asset, one of `Quantity`, `QuantityInBaseAsset` or `QuantityInQuoteAsset` should be provided
        /// </summary>
        [JsonPropertyName("valueQty"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? QuantityInQuoteAsset { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Limit price
        /// </summary>
        [JsonPropertyName("price"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonConverter(typeof(DecimalStringWriterConverter))]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>timeInForce</c>"] Time in force
        /// </summary>
        [JsonPropertyName("timeInForce"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TimeInForce? TimeInForce { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Order type
        /// </summary>
        [JsonPropertyName("type"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public NewOrderType? OrderType { get; set; }
        /// <summary>
        /// ["<c>remark</c>"] Remark
        /// </summary>
        [JsonPropertyName("remark"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Remark { get; set; }
        /// <summary>
        /// ["<c>stop</c>"] Stop type
        /// </summary>
        [JsonPropertyName("stop"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public StopType? Stop { get; set; }
        /// <summary>
        /// ["<c>stopPriceType</c>"] Stop price type
        /// </summary>
        [JsonPropertyName("stopPriceType"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public StopPriceType? StopPriceType { get; set; }
        /// <summary>
        /// ["<c>stopPrice</c>"] Stop price
        /// </summary>
        [JsonPropertyName("stopPrice"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? StopPrice { get; set; }
        /// <summary>
        /// ["<c>reduceOnly</c>"] Reduce only
        /// </summary>
        [JsonPropertyName("reduceOnly"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool? ReduceOnly { get; set; }
        /// <summary>
        /// ["<c>closeOrder</c>"] Close order
        /// </summary>
        [JsonPropertyName("closeOrder"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool? CloseOrder { get; set; }
        /// <summary>
        /// ["<c>forceHold</c>"] Force hold
        /// </summary>
        [JsonPropertyName("forceHold"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool? ForceHold { get; set; }
        /// <summary>
        /// ["<c>postOnly</c>"] Post only
        /// </summary>
        [JsonPropertyName("postOnly"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool? PostOnly { get; set; }
        /// <summary>
        /// ["<c>hidden</c>"] Is hidden
        /// </summary>
        [JsonPropertyName("hidden"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool? Hidden { get; set; }
        /// <summary>
        /// ["<c>iceberg</c>"] Is iceberg order
        /// </summary>
        [JsonPropertyName("iceberg"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool? Iceberg { get; set; }
        /// <summary>
        /// ["<c>visibleSize</c>"] Visible size
        /// </summary>
        [JsonPropertyName("visibleSize"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool? VisibleSize { get; set; }
        /// <summary>
        /// ["<c>stp</c>"] Self trade prevention type
        /// </summary>
        [JsonPropertyName("stp"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]

        public SelfTradePrevention? SelfTradePrevention { get; set; }
        /// <summary>
        /// ["<c>marginMode</c>"] Margin mode
        /// </summary>
        [JsonPropertyName("marginMode"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]

        public FuturesMarginMode? MarginMode { get; set; }
        /// <summary>
        /// ["<c>positionSide</c>"] Position side (required in HedgeMode)
        /// </summary>
        [JsonPropertyName("positionSide"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]

        public PositionSide? PositionSide { get; set; }
    }
}
