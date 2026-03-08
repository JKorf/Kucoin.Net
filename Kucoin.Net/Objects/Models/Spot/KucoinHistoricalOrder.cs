using System;
using Kucoin.Net.Enums;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Historical order info
    /// </summary>
    [SerializationModel]
    public record KucoinHistoricalOrder
    {
        /// <summary>
        /// ["<c>id</c>"] The id of the order
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbol</c>"] The symbol of the order
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>dealPrice</c>"] The price of the order
        /// </summary>
        [JsonPropertyName("dealPrice")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>dealValue</c>"] The value of the order
        /// </summary>
        [JsonPropertyName("dealValue")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] The quantity of the order
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] The fee of the order
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>side</c>"] The side of the order
        /// </summary>        
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>createdAt</c>"] The time the order was created
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("createdAt")]
        public DateTime CreateTime { get; set; }
    }
}
