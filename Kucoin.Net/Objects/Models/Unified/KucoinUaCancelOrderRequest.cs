namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Cancel order request
    /// </summary>
    public record KucoinUaCancelOrderRequest
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>orderId</c>"] Order id, either this or ClientOrderId should be provided
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault), JsonPropertyName("orderId")]
        public string? OrderId { get; set; }
        /// <summary>
        /// ["<c>clientOid</c>"] Client order id, either this or OrderId should be provided
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault), JsonPropertyName("clientOid")]
        public string? ClientOrderId { get; set; }
    }
}
