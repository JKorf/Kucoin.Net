namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Cross margin config
    /// </summary>
    public record KucoinUaCrossMarginConfig
    {
        /// <summary>
        /// ["<c>maxLeverage</c>"] Max leverage
        /// </summary>
        [JsonPropertyName("maxLeverage")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// ["<c>alertRiskRatio</c>"] Alert risk ratio
        /// </summary>
        [JsonPropertyName("alertRiskRatio")]
        public decimal AlertRiskRatio { get; set; }
        /// <summary>
        /// ["<c>liquidationRiskRatio</c>"] Liquidation risk ratio
        /// </summary>
        [JsonPropertyName("liquidationRiskRatio")]
        public decimal LiquidationRiskRatio { get; set; }
        /// <summary>
        /// ["<c>currencyList</c>"] Asset list
        /// </summary>
        [JsonPropertyName("currencyList")]
        public string[] AssetList { get; set; } = [];
    }
}
