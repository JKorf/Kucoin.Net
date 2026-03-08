using System;
using Kucoin.Net.Enums;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Trade info
    /// </summary>
    [SerializationModel]
    public record KucoinTrade
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
        /// ["<c>time</c>"] The timestamp of the trade
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter)), JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
    }
}
