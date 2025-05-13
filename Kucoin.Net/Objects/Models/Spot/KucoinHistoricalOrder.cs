using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// The id of the order
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// The symbol of the order
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The price of the order
        /// </summary>
        [JsonPropertyName("dealPrice")]
        public decimal Price { get; set; }
        /// <summary>
        /// The value of the order
        /// </summary>
        [JsonPropertyName("dealValue")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// The quantity of the order
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The fee of the order
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// The side of the order
        /// </summary>        
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// The time the order was created
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("createdAt")]
        public DateTime CreateTime { get; set; }
    }
}
