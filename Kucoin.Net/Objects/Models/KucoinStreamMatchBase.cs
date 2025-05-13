using CryptoExchange.Net.Converters.SystemTextJson;
using Kucoin.Net.Enums;


namespace Kucoin.Net.Objects.Models
{
    /// <summary>
    /// Match info
    /// </summary>
    [SerializationModel]
    public record KucoinStreamMatchBase
    {
        /// <summary>
        /// The sequence of the match
        /// </summary>
        [JsonPropertyName("sequence")]
        public long Sequence { get; set; }
        /// <summary>
        /// The symbol the match is for
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The side of the match
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// The quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The price of the match
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// The taker order id
        /// </summary>
        [JsonPropertyName("takerOrderId")]
        public string TakerOrderId { get; set; } = string.Empty;
        /// <summary>
        /// The maker order id
        /// </summary>
        [JsonPropertyName("makerOrderId")]
        public string MakerOrderId { get; set; } = string.Empty;
        /// <summary>
        /// The id of the trade
        /// </summary>
        [JsonPropertyName("tradeId")]
        public string Id { get; set; } = string.Empty;

    }
}
