using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// The symbol the fill is for
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The side of the fill
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// The price of the fill
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// The quantity of the fill
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// The quantity of fee of the fill
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// The price of the fee
        /// </summary>
        [JsonPropertyName("feeRate")]
        public decimal FeePrice { get; set; }
        /// <summary>
        /// The asset of the fee
        /// </summary>
        [JsonPropertyName("feeCurrency")]
        public string FeeAsset { get; set; } = string.Empty;

        /// <summary>
        /// The time the fill was created
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("createdAt")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// The id of the trade
        /// </summary>
        [JsonPropertyName("tradeId"), JsonConverter(typeof(NumberStringConverter))]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// The id of the order
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;

        /// <summary>
        /// Maker or taker
        /// </summary>
        [JsonPropertyName("liquidity")]
        public LiquidityType Liquidity { get; set; }
    }
}
