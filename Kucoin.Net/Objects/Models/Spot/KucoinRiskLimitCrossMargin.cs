namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Risk limit info
    /// </summary>
    [SerializationModel]
    public record KucoinRiskLimitCrossMargin
    {
        /// <summary>
        /// ["<c>currency</c>"] The asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>borrowMaxAmount</c>"] Max borrow quantity
        /// </summary>
        [JsonPropertyName("borrowMaxAmount")]
        public decimal BorrowMaxQuantity { get; set; }

        /// <summary>
        /// ["<c>buyMaxAmount</c>"] Max buy quantity
        /// </summary>
        [JsonPropertyName("buyMaxAmount")]
        public decimal BuyMaxQuantity { get; set; }

        /// <summary>
        /// ["<c>precision</c>"] Precision
        /// </summary>
        [JsonPropertyName("precision")]
        public int Precision { get; set; }
    }
}
