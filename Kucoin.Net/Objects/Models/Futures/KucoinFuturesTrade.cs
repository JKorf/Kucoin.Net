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
        /// ["<c>sequence</c>"] The sequence number of the trade
        /// </summary>
        [JsonPropertyName("sequence")]
        public long Sequence { get; set; }
        /// <summary>
        /// ["<c>price</c>"] The price of the trade
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>size</c>"] The quantity of the trade
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>side</c>"] The side of the trade
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>tradeId</c>"] Trade id
        /// </summary>
        [JsonPropertyName("tradeId")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>takerOrderId</c>"] Taker order id
        /// </summary>
        [JsonPropertyName("takerOrderId")]
        public string TakerOrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>makerOrderId</c>"] Maker order id
        /// </summary>
        [JsonPropertyName("makerOrderId")]
        public string MakerOrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>ts</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
