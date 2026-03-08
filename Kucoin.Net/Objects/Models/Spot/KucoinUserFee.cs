namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// User fee
    /// </summary>
    [SerializationModel]
    public record KucoinUserFee
    {
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
