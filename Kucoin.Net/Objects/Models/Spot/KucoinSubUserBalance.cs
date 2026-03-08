using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Sub user balances
    /// </summary>
    [SerializationModel]
    public record KucoinSubUserBalances
    {
        /// <summary>
        /// ["<c>subUserId</c>"] The sub user id
        /// </summary>
        [JsonPropertyName("subUserId")]
        public string SubAccountId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>subName</c>"] The sub user name
        /// </summary>
        [JsonPropertyName("subName")]
        public string SubName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>mainAccounts</c>"] Main account balances
        /// </summary>
        [JsonPropertyName("mainAccounts")]
        public KucoinSubUserBalance[] MainAccounts { get; set; } = Array.Empty<KucoinSubUserBalance>();
        /// <summary>
        /// ["<c>tradeAccounts</c>"] Trade account balances
        /// </summary>
        [JsonPropertyName("tradeAccounts")]
        public KucoinSubUserBalance[] TradeAccounts { get; set; } = Array.Empty<KucoinSubUserBalance>();
        /// <summary>
        /// ["<c>marginAccounts</c>"] Margin account balances
        /// </summary>
        [JsonPropertyName("marginAccounts")]
        public KucoinSubUserBalance[] MarginAccounts { get; set; } = Array.Empty<KucoinSubUserBalance>();
    }

    /// <summary>
    /// Sub user info
    /// </summary>
    [SerializationModel]
    public record KucoinSubUserBalance
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>available</c>"] Available quantity
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// ["<c>balance</c>"] Total balance
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal Total { get; set; }
        /// <summary>
        /// ["<c>holds</c>"] Frozen quantity
        /// </summary>
        [JsonPropertyName("holds")]
        public decimal Frozen { get; set; }
        /// <summary>
        /// ["<c>baseCurrency</c>"] Base asset
        /// </summary>
        [JsonPropertyName("baseCurrency")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>baseCurrencyPrice</c>"] Base asset price
        /// </summary>
        [JsonPropertyName("baseCurrencyPrice")]
        public decimal BaseAssetPrice { get; set; }
        /// <summary>
        /// ["<c>baseAmount</c>"] Base asset quantity
        /// </summary>
        [JsonPropertyName("baseAmount")]
        public decimal BaseAssetQuantity { get; set; }
    }
}
