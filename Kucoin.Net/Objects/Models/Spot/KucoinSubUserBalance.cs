using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Sub user balances
    /// </summary>
    public record KucoinSubUserBalances
    {
        /// <summary>
        /// The sub user id
        /// </summary>
        [JsonProperty("subUserId")]
        public string SubAccountId { get; set; } = string.Empty;
        /// <summary>
        /// The sub user name
        /// </summary>
        [JsonProperty("subName")]
        public string SubName { get; set; } = string.Empty;
        /// <summary>
        /// Main account balances
        /// </summary>
        [JsonProperty("mainAccounts")]
        public IEnumerable<KucoinSubUserBalance> MainAccounts { get; set; } = Array.Empty<KucoinSubUserBalance>();
        /// <summary>
        /// Trade account balances
        /// </summary>
        [JsonProperty("tradeAccounts")]
        public IEnumerable<KucoinSubUserBalance> TradeAccounts { get; set; } = Array.Empty<KucoinSubUserBalance>();
        /// <summary>
        /// Margin account balances
        /// </summary>
        [JsonProperty("marginAccounts")]
        public IEnumerable<KucoinSubUserBalance> MarginAccounts { get; set; } = Array.Empty<KucoinSubUserBalance>();
    }

    /// <summary>
    /// Sub user info
    /// </summary>
    public record KucoinSubUserBalance
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Available quantity
        /// </summary>
        [JsonProperty("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// Total balance
        /// </summary>
        [JsonProperty("balance")]
        public decimal Total { get; set; }
        /// <summary>
        /// Frozen quantity
        /// </summary>
        [JsonProperty("holds")]
        public decimal Frozen { get; set; }
        /// <summary>
        /// Base asset
        /// </summary>
        [JsonProperty("baseCurrency")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// Base asset price
        /// </summary>
        [JsonProperty("baseCurrencyPrice")]
        public decimal BaseAssetPrice { get; set; }
        /// <summary>
        /// Base asset quantity
        /// </summary>
        [JsonProperty("baseAmount")]
        public decimal BaseAssetQuantity { get; set; }
    }
}
