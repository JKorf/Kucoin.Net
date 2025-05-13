using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using Kucoin.Net.Enums;


namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Trade info
    /// </summary>
    [SerializationModel]
    public record KucoinFuturesTrade
    {
        /// <summary>
        /// The sequence number of the trade
        /// </summary>
        [JsonPropertyName("sequence")]
        public long Sequence { get; set; }
        /// <summary>
        /// The price of the trade
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// The quantity of the trade
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The side of the trade
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Trade id
        /// </summary>
        [JsonPropertyName("tradeId")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Taker order id
        /// </summary>
        [JsonPropertyName("takerOrderId")]
        public string TakerOrderId { get; set; } = string.Empty;
        /// <summary>
        /// Maker order id
        /// </summary>
        [JsonPropertyName("makerOrderId")]
        public string MakerOrderId { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
