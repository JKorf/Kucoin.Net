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
        /// ["<c>id</c>"] Transaction id
        /// </summary>
        [JsonPropertyName("id"), JsonConverter(typeof(NumberStringConverter))]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>type</c>"] The type of the order
        /// </summary>
        [JsonPropertyName("type")]
        public OrderType Type { get; set; }        
        /// <summary>
        /// ["<c>funds</c>"] The funds of the fill
        /// </summary>
        [JsonPropertyName("funds")]
        public decimal QuoteQuantity { get; set; }        
        /// <summary>
        /// ["<c>stop</c>"] The stop condition of the fill
        /// </summary>
        [JsonPropertyName("stop")]
        public StopCondition? Stop { get; set; }
        /// <summary>
        /// ["<c>counterOrderId</c>"] The id of the counter order
        /// </summary>
        [JsonPropertyName("counterOrderId")]
        public string CounterOrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>forceTaker</c>"] Was forced to become taker
        /// </summary>
        [JsonPropertyName("forceTaker")]
        public bool ForceTaker { get; set; }
        /// <summary>
        /// ["<c>tradeType</c>"] Trade type
        /// </summary>
        [JsonPropertyName("tradeType")]
        public string TradeType { get; set; } = string.Empty;
    }
}
