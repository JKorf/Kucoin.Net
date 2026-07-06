namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// User info
    /// </summary>
    [SerializationModel]
    public record KucoinUserInfo
    {
        /// <summary>
        /// ["<c>level</c>"] User level
        /// </summary>
        [JsonPropertyName("level")]
        public int Level { get; set; }
        /// <summary>
        /// ["<c>subQuantity</c>"] Max number of default open sub-accounts (according to level)
        /// </summary>
        [JsonPropertyName("subQuantity")]
        public int SubQuantity { get; set; }
        /// <summary>
        /// ["<c>maxDefaultSubQuantity</c>"] Max number of default open sub-accounts (according to level)
        /// </summary>
        [JsonPropertyName("maxDefaultSubQuantity")]
        public int MaxDefaultSubQuantity { get; set; }
        /// <summary>
        /// ["<c>maxSubQuantity</c>"] Max number of sub-accounts = maxDefaultSubQuantity + maxSpotSubQuantity
        /// </summary>
        [JsonPropertyName("maxSubQuantity")]
        public int MaxSubQuantity { get; set; }
        /// <summary>
        /// ["<c>spotSubQuantity</c>"] Number of sub-accounts with spot trading permissions enabled
        /// </summary>
        [JsonPropertyName("spotSubQuantity")]
        public int SpotSubQuantity { get; set; }
        /// <summary>
        /// ["<c>optionSubQuantity</c>"] Number of sub-accounts with options trading permissions enabled
        /// </summary>
        [JsonPropertyName("optionSubQuantity")]
        public int OptionSubQuantity { get; set; }
        /// <summary>
        /// ["<c>maxOptionSubQuantity</c>"] Max number of sub-accounts with options permissions
        /// </summary>
        [JsonPropertyName("maxOptionSubQuantity")]
        public int MaxOptionSubQuantity { get; set; }
        /// <summary>
        /// ["<c>marginSubQuantity</c>"] Number of sub-accounts with margin trading permissions enabled
        /// </summary>
        [JsonPropertyName("marginSubQuantity")]
        public int MarginSubQuantity { get; set; }
        /// <summary>
        /// ["<c>futuresSubQuantity</c>"] Number of sub-accounts with futures trading permissions enabled
        /// </summary>
        [JsonPropertyName("futuresSubQuantity")]
        public int FuturesSubQuantity { get; set; }
        /// <summary>
        /// ["<c>maxSpotSubQuantity</c>"] Max number of sub-accounts with additional Spot trading permissions
        /// </summary>
        [JsonPropertyName("maxSpotSubQuantity")]
        public int MaxSpotSubQuantity { get; set; }
        /// <summary>
        /// ["<c>maxMarginSubQuantity</c>"] Max number of sub-accounts with additional margin trading permissions
        /// </summary>
        [JsonPropertyName("maxMarginSubQuantity")]
        public int MaxMarginSubQuantity { get; set; }
        /// <summary>
        /// ["<c>maxFuturesSubQuantity</c>"] Max number of sub-accounts with additional futures trading permissions
        /// </summary>
        [JsonPropertyName("maxFuturesSubQuantity")]
        public int MaxFuturesSubQuantity { get; set; }
    }
}
