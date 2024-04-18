using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Leveraged token info
    /// </summary>
    public record KucoinLeveragedToken
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Net worth
        /// </summary>
        [JsonProperty("netAsset")]
        public decimal NetWorth { get; set; }
        /// <summary>
        /// Target leverage
        /// </summary>
        [JsonProperty("targetLeverage")]
        public string TargetLeverage { get; set; } = string.Empty;
        /// <summary>
        /// Actual leverage
        /// </summary>
        [JsonProperty("actualLeverage")]
        public decimal ActualLeverage { get; set; }
        /// <summary>
        /// Issued size
        /// </summary>
        [JsonProperty("issuedSize")]
        public decimal IssuedQuantity { get; set; }
        /// <summary>
        /// Basket info
        /// </summary>
        [JsonProperty("basket")]
        public string Basket { get; set; } = string.Empty;
    }
}
