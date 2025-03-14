using CryptoExchange.Net.Converters.SystemTextJson;

using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Sub user balances
    /// </summary>
    [SerializationModel]
    public record KucoinSubUserBalances
    {
        /// <summary>
        /// The sub user id
        /// </summary>
        [JsonPropertyName("subUserId")]
        public string SubAccountId { get; set; } = string.Empty;
        /// <summary>
        /// The sub user name
        /// </summary>
        [JsonPropertyName("subName")]
        public string SubName { get; set; } = string.Empty;
        /// <summary>
        /// Main account balances
        /// </summary>
        [JsonPropertyName("mainAccounts")]
        public KucoinSubUserBalance[] MainAccounts { get; set; } = Array.Empty<KucoinSubUserBalance>();
        /// <summary>
        /// Trade account balances
        /// </summary>
        [JsonPropertyName("tradeAccounts")]
        public KucoinSubUserBalance[] TradeAccounts { get; set; } = Array.Empty<KucoinSubUserBalance>();
        /// <summary>
        /// Margin account balances
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
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Available quantity
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// Total balance
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal Total { get; set; }
        /// <summary>
        /// Frozen quantity
        /// </summary>
        [JsonPropertyName("holds")]
        public decimal Frozen { get; set; }
        /// <summary>
        /// Base asset
        /// </summary>
        [JsonPropertyName("baseCurrency")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// Base asset price
        /// </summary>
        [JsonPropertyName("baseCurrencyPrice")]
        public decimal BaseAssetPrice { get; set; }
        /// <summary>
        /// Base asset quantity
        /// </summary>
        [JsonPropertyName("baseAmount")]
        public decimal BaseAssetQuantity { get; set; }
    }
}
