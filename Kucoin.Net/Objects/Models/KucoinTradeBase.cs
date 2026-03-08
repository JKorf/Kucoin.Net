using System;
using Kucoin.Net.Enums;


namespace Kucoin.Net.Objects.Models
{
    /// <summary>
    /// Trade info
    /// </summary>
    [SerializationModel]
    public record KucoinTradeBase
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol the fill is for
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>side</c>"] The side of the fill
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>price</c>"] The price of the fill
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>size</c>"] The quantity of the fill
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// ["<c>fee</c>"] The quantity of fee of the fill
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>feeRate</c>"] The price of the fee
        /// </summary>
        [JsonPropertyName("feeRate")]
        public decimal FeePrice { get; set; }
        /// <summary>
        /// ["<c>feeCurrency</c>"] The asset of the fee
        /// </summary>
        [JsonPropertyName("feeCurrency")]
        public string FeeAsset { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>createdAt</c>"] The time the fill was created
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("createdAt")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>tradeId</c>"] The id of the trade
        /// </summary>
        [JsonPropertyName("tradeId"), JsonConverter(typeof(NumberStringConverter))]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>orderId</c>"] The id of the order
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>liquidity</c>"] Maker or taker
        /// </summary>
        [JsonPropertyName("liquidity")]
        public LiquidityType Liquidity { get; set; }
    }
}
