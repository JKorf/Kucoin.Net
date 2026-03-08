namespace Kucoin.Net.Objects.Models.Unified
{
    internal record KucoinUaFeeRates
    {
        [JsonPropertyName("list")]
        public KucoinUaFeeRate[] Rates { get; set; } = [];
    }

    /// <summary>
    /// Fee rate
    /// </summary>
    public record KucoinUaFeeRate
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>takerFeeRate</c>"] Taker fee rate
        /// </summary>
        [JsonPropertyName("takerFeeRate")]
        public decimal TakerFeeRate { get; set; }
        /// <summary>
        /// ["<c>makerFeeRate</c>"] Maker fee rate
        /// </summary>
        [JsonPropertyName("makerFeeRate")]
        public decimal MakerFeeRate { get; set; }
    }


}
