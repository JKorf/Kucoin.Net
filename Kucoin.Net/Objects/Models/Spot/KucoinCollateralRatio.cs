namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Collateral ratio
    /// </summary>
    public record KucoinCollateralRatios
    {
        /// <summary>
        /// ["<c>currencyList</c>"] Asset list
        /// </summary>
        [JsonPropertyName("currencyList")]
        public string[] Assets { get; set; } = [];

        /// <summary>
        /// ["<c>items</c>"] Items
        /// </summary>
        [JsonPropertyName("items")]
        public KucoinCollateralRatio[] Items { get; set; } = [];
    }

    /// <summary>
    /// Collateral ratio info
    /// </summary>
    public record KucoinCollateralRatio
    {
        /// <summary>
        /// ["<c>lowerLimit</c>"] Lower limit
        /// </summary>
        [JsonPropertyName("lowerLimit")]
        public decimal LowerLimit { get; set; }
        /// <summary>
        /// ["<c>upperLimit</c>"] Upper limit
        /// </summary>
        [JsonPropertyName("upperLimit")]
        public decimal UpperLimit { get; set; }
        /// <summary>
        /// ["<c>collateralRatio</c>"] Collateral ratio
        /// </summary>
        [JsonPropertyName("collateralRatio")]
        public decimal CollateralRatio { get; set; }
    }

}
