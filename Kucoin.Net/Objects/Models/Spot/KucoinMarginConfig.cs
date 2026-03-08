using System;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Margin configuration
    /// </summary>
    [SerializationModel]
    public record KucoinMarginConfig
    {
        /// <summary>
        /// ["<c>currencyList</c>"] Available assets for margin trade
        /// </summary>
        [JsonPropertyName("currencyList")]
        public string[] Assets { get; set; } = Array.Empty<string>();
        /// <summary>
        /// ["<c>warningDebtRatio</c>"] Warning debt ratio
        /// </summary>
        [JsonPropertyName("warningDebtRatio")]
        public decimal WarningDebtRatio { get; set; }
        /// <summary>
        /// ["<c>liqDebtRatio</c>"] Forced liquidation ratio
        /// </summary>
        [JsonPropertyName("liqDebtRatio")]
        public decimal LiquidationDebtRatio { get; set; }
        /// <summary>
        /// ["<c>maxLeverage</c>"] Max leverage
        /// </summary>
        [JsonPropertyName("maxLeverage")]
        public decimal MaxLeverage { get; set; }
    }
}
