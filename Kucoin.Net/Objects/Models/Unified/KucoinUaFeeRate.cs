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
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Taker fee rate
        /// </summary>
        [JsonPropertyName("takerFeeRate")]
        public decimal TakerFeeRate { get; set; }
        /// <summary>
        /// Maker fee rate
        /// </summary>
        [JsonPropertyName("makerFeeRate")]
        public decimal MakerFeeRate { get; set; }
    }


}
