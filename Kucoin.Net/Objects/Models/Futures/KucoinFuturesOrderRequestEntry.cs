using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Client order id
        /// </summary>
        [JsonPropertyName("clientOid"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? ClientOrderId { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonConverter(typeof(DecimalStringWriterConverter))]
        public decimal? Leverage { get; set; }
        /// <summary>
        /// Amount of contracts to buy or sell, one of `Quantity`, `QuantityInBaseAsset` or `QuantityInQuoteAsset` should be provided
        /// </summary>
        [JsonPropertyName("size"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int? Quantity { get; set; }
        /// <summary>
        /// Quantity in base asset, one of `Quantity`, `QuantityInBaseAsset` or `QuantityInQuoteAsset` should be provided
        /// </summary>
        [JsonPropertyName("qty"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? QuantityInBaseAsset { get; set; }
        /// <summary>
        /// Quantity in quote asset, one of `Quantity`, `QuantityInBaseAsset` or `QuantityInQuoteAsset` should be provided
        /// </summary>
        [JsonPropertyName("valueQty"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? QuantityInQuoteAsset { get; set; }
        /// <summary>
        /// Limit price
        /// </summary>
        [JsonPropertyName("price"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonConverter(typeof(DecimalStringWriterConverter))]
        public decimal? Price { get; set; }
        /// <summary>
        /// Time in force
        /// </summary>
        [JsonPropertyName("timeInForce"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TimeInForce? TimeInForce { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("type"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public NewOrderType? OrderType { get; set; }
        /// <summary>
        /// Remark
        /// </summary>
        [JsonPropertyName("remark"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Remark { get; set; }
        /// <summary>
        /// Stop type
        /// </summary>
        [JsonPropertyName("stop"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public StopType? Stop { get; set; }
        /// <summary>
        /// Stop price type
        /// </summary>
        [JsonPropertyName("stopPriceType"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public StopPriceType? StopPriceType { get; set; }
        /// <summary>
        /// Stop price
        /// </summary>
        [JsonPropertyName("stopPrice"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? StopPrice { get; set; }
        /// <summary>
        /// Reduce only
        /// </summary>
        [JsonPropertyName("reduceOnly"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool? ReduceOnly { get; set; }
        /// <summary>
        /// Close order
        /// </summary>
        [JsonPropertyName("closeOrder"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool? CloseOrder { get; set; }
        /// <summary>
        /// Force hold
        /// </summary>
        [JsonPropertyName("forceHold"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool? ForceHold { get; set; }
        /// <summary>
        /// Post only
        /// </summary>
        [JsonPropertyName("postOnly"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool? PostOnly { get; set; }
        /// <summary>
        /// Is hidden
        /// </summary>
        [JsonPropertyName("hidden"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool? Hidden { get; set; }
        /// <summary>
        /// Is iceberg order
        /// </summary>
        [JsonPropertyName("iceberg"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool? Iceberg { get; set; }
        /// <summary>
        /// Visible size
        /// </summary>
        [JsonPropertyName("visibleSize"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool? VisibleSize { get; set; }
        /// <summary>
        /// Self trade prevention type
        /// </summary>
        [JsonPropertyName("stp"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]

        public SelfTradePrevention? SelfTradePrevention { get; set; }
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonPropertyName("marginMode"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]

        public FuturesMarginMode? MarginMode { get; set; }
    }
}
