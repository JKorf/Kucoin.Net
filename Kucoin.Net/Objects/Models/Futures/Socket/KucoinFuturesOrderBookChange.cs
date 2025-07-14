using CryptoExchange.Net.Converters.SystemTextJson;
using Kucoin.Net.Enums;

using System;
using System.Globalization;

namespace Kucoin.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Order book change
    /// </summary>
    [SerializationModel]
    public record KucoinFuturesOrderBookChange
    {
        /// <summary>
        /// Sequence number
        /// </summary>
        [JsonPropertyName("sequence")]
        public long Sequence { get; set; }
        /// <summary>
        /// Data timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonInclude, JsonPropertyName("change")]
        internal string Change { get; set; } = string.Empty;
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price => decimal.Parse(Change.Split(',')[0], NumberStyles.Float, CultureInfo.InvariantCulture);
        /// <summary>
        /// Side
        /// </summary>
        public OrderSide Side => string.Equals(Change.Split(',')[1], "sell", StringComparison.Ordinal) ? OrderSide.Sell : OrderSide.Buy;
        /// <summary>
        /// Quantity
        /// </summary>
        public decimal Quantity => decimal.Parse(Change.Split(',')[2], NumberStyles.Float, CultureInfo.InvariantCulture);
    }
}
