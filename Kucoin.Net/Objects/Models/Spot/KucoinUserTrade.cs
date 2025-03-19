using CryptoExchange.Net.Converters.SystemTextJson;
using Kucoin.Net.Enums;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// User trade info
    /// </summary>
    [SerializationModel]
    public record KucoinUserTrade: KucoinTradeBase
    {
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonPropertyName("id"), JsonConverter(typeof(NumberStringConverter))]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonPropertyName("type")]
        public OrderType Type { get; set; }        
        /// <summary>
        /// The funds of the fill
        /// </summary>
        [JsonPropertyName("funds")]
        public decimal QuoteQuantity { get; set; }        
        /// <summary>
        /// The stop condition of the fill
        /// </summary>
        [JsonPropertyName("stop")]
        public StopCondition? Stop { get; set; }
        /// <summary>
        /// The id of the counter order
        /// </summary>
        [JsonPropertyName("counterOrderId")]
        public string CounterOrderId { get; set; } = string.Empty;
        /// <summary>
        /// Was forced to become taker
        /// </summary>
        [JsonPropertyName("forceTaker")]
        public bool ForceTaker { get; set; }
        /// <summary>
        /// Trade type
        /// </summary>
        [JsonPropertyName("tradeType")]
        public string TradeType { get; set; } = string.Empty;
    }
}
