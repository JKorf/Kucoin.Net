namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Trade fee
    /// </summary>
    [SerializationModel]
    public record KucoinTradeFee
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>takerFeeRate</c>"] Fee rate for trades as taker
        /// </summary>
        [JsonPropertyName("takerFeeRate")]
        public decimal TakerFeeRate { get; set; }
        /// <summary>
        /// ["<c>makerFeeRate</c>"] Fee rate for trades as maker
        /// </summary>
        [JsonPropertyName("makerFeeRate")]
        public decimal MakerFeeRate { get; set; }
    }
}
