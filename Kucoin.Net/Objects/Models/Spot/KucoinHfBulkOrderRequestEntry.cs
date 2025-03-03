﻿using Kucoin.Net.Enums;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// The order model to be sent via bulk order endpoint
    /// </summary>
    public record KucoinHfBulkOrderRequestEntry
    {
        /// <summary>
        /// The symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The client order id
        /// </summary>
        [JsonPropertyName("clientOid")]
        public string ClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// The side of the order
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// The price of the order
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// The quantity of the order
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonPropertyName("type")]
        public NewOrderType Type { get; set; }

        /// <summary>
        /// Remark for the order
        /// </summary>
        [JsonPropertyName("remark"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Remark { get; set; }
        /// <summary>
        /// Tags for the order
        /// </summary>
        [JsonPropertyName("tags"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Tags { get; set; }
        /// <summary>
        /// The self trade prevention type
        /// </summary>
        [JsonPropertyName("stp"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public SelfTradePrevention? SelfTradePrevention { get; set; }
        /// <summary>
        /// The time in force of the order
        /// </summary>
        [JsonPropertyName("timeInForce"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TimeInForce? TimeInForce { get; set; }
        /// <summary>
        /// Timespan in seconds after which the order is canceled
        /// </summary>
        [JsonPropertyName("cancelAfter"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int? CancelAfter { get; set; }
        /// <summary>
        /// Whether the order is post only
        /// </summary>
        [JsonPropertyName("postOnly"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool? PostOnly { get; set; }
        /// <summary>
        /// Whether the order is hidden
        /// </summary>
        [JsonPropertyName("hidden"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool? Hidden { get; set; }
        /// <summary>
        /// Whether it is an iceberg order
        /// </summary>
        [JsonPropertyName("iceberg"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool? Iceberg { get; set; }
        /// <summary>
        /// The max visible size of the iceberg
        /// </summary>
        [JsonPropertyName("visibleSize"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? VisibleIcebergSize { get; set; }
    }
}
