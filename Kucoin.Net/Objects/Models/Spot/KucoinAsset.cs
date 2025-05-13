using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Asset info
    /// </summary>
    [SerializationModel]
    public record KucoinAssetBase
    {
        /// <summary>
        /// The asset identifier
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The name of the asset
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The full name of the asset
        /// </summary>
        [JsonPropertyName("fullName")]
        public string FullName { get; set; } = string.Empty;
        /// <summary>
        /// The precision of the asset
        /// </summary>
        [JsonPropertyName("precision")]
        public int Precision { get; set; }
        /// <summary>
        /// Number of block confirmations
        /// </summary>
        [JsonPropertyName("confirms")]
        public int? Confirms { get; set; }
        /// <summary>
        /// Contract address
        /// </summary>
        [JsonPropertyName("contractAddress")]
        public string ContractAddress { get; set; } = string.Empty;
        /// <summary>
        /// Is margin enabled
        /// </summary>
        [JsonPropertyName("isMarginEnabled")]
        public bool IsMarginEnabled { get; set; }
        /// <summary>
        /// Is debit enabled
        /// </summary>
        [JsonPropertyName("isDebitEnabled")]
        public bool IsDebitEnabled { get; set; }
    }

    /// <summary>
    /// Asset info
    /// </summary>
    [SerializationModel]
    public record KucoinAsset: KucoinAssetBase
    {        
        /// <summary>
        /// The minimum quantity of a withdrawal
        /// </summary>
        [JsonPropertyName("withdrawalMinSize")]
        public decimal WithdrawalMinQuantity { get; set; }
        /// <summary>
        /// The minimum fee of a withdrawal
        /// </summary>
        [JsonPropertyName("withdrawalMinFee")]
        public decimal WithdrawalMinFee { get; set; }
        /// <summary>
        /// Is withdrawing enabled for this asset
        /// </summary>
        [JsonPropertyName("isWithdrawalEnabled")]
        public bool IsWithdrawEnabled { get; set; }
        /// <summary>
        /// Is depositing enabled for this asset
        /// </summary>
        [JsonPropertyName("isDepositEnabled")]
        public bool IsDepositEnabled { get; set; }
    }

    /// <summary>
    /// Asset details
    /// </summary>
    [SerializationModel]
    public record KucoinAssetDetails: KucoinAssetBase
    {
        /// <summary>
        /// Networks
        /// </summary>
        [JsonPropertyName("chains")]
        public KucoinAssetNetwork[] Networks { get; set; } = Array.Empty<KucoinAssetNetwork>();
    }
}
