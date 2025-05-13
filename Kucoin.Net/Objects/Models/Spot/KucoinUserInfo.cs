using CryptoExchange.Net.Converters.SystemTextJson;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// User info
    /// </summary>
    [SerializationModel]
    public record KucoinUserInfo
    {
        /// <summary>
        /// User level
        /// </summary>
        [JsonPropertyName("level")]
        public int Level { get; set; }
        /// <summary>
        /// Max number of default open sub-accounts (according to level)
        /// </summary>
        [JsonPropertyName("subQuantity")]
        public decimal SubQuantity { get; set; }
        /// <summary>
        /// Max number of default open sub-accounts (according to level)
        /// </summary>
        [JsonPropertyName("maxDefaultSubQuantity")]
        public decimal MaxDefaultSubQuantity { get; set; }
        /// <summary>
        /// Max number of sub-accounts = maxDefaultSubQuantity + maxSpotSubQuantity
        /// </summary>
        [JsonPropertyName("maxSubQuantity")]
        public decimal MaxSubQuantity { get; set; }
        /// <summary>
        /// Number of sub-accounts with spot trading permissions enabled
        /// </summary>
        [JsonPropertyName("spotSubQuantity")]
        public decimal SpotSubQuantity { get; set; }
        /// <summary>
        /// Number of sub-accounts with margin trading permissions enabled
        /// </summary>
        [JsonPropertyName("marginSubQuantity")]
        public decimal MarginSubQuantity { get; set; }
        /// <summary>
        /// Number of sub-accounts with futures trading permissions enabled
        /// </summary>
        [JsonPropertyName("futuresSubQuantity")]
        public decimal FuturesSubQuantity { get; set; }
        /// <summary>
        /// Max number of sub-accounts with additional Spot trading permissions
        /// </summary>
        [JsonPropertyName("maxSpotSubQuantity")]
        public decimal MaxSpotSubQuantity { get; set; }
        /// <summary>
        /// Max number of sub-accounts with additional margin trading permissions
        /// </summary>
        [JsonPropertyName("maxMarginSubQuantity")]
        public decimal MaxMarginSubQuantity { get; set; }
        /// <summary>
        /// Max number of sub-accounts with additional futures trading permissions
        /// </summary>
        [JsonPropertyName("maxFuturesSubQuantity")]
        public decimal MaxFuturesSubQuantity { get; set; }
    }
}
