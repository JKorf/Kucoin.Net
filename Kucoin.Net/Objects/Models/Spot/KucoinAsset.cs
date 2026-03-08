using System;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Asset info
    /// </summary>
    [SerializationModel]
    public record KucoinAssetBase
    {
        /// <summary>
        /// ["<c>currency</c>"] The asset identifier
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>name</c>"] The name of the asset
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fullName</c>"] The full name of the asset
        /// </summary>
        [JsonPropertyName("fullName")]
        public string FullName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>precision</c>"] The precision of the asset
        /// </summary>
        [JsonPropertyName("precision")]
        public int Precision { get; set; }
        /// <summary>
        /// ["<c>confirms</c>"] Number of block confirmations
        /// </summary>
        [JsonPropertyName("confirms")]
        public int? Confirms { get; set; }
        /// <summary>
        /// ["<c>contractAddress</c>"] Contract address
        /// </summary>
        [JsonPropertyName("contractAddress")]
        public string ContractAddress { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>isMarginEnabled</c>"] Is margin enabled
        /// </summary>
        [JsonPropertyName("isMarginEnabled")]
        public bool IsMarginEnabled { get; set; }
        /// <summary>
        /// ["<c>isDebitEnabled</c>"] Is debit enabled
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
        /// ["<c>withdrawalMinSize</c>"] The minimum quantity of a withdrawal
        /// </summary>
        [JsonPropertyName("withdrawalMinSize")]
        public decimal WithdrawalMinQuantity { get; set; }
        /// <summary>
        /// ["<c>withdrawalMinFee</c>"] The minimum fee of a withdrawal
        /// </summary>
        [JsonPropertyName("withdrawalMinFee")]
        public decimal WithdrawalMinFee { get; set; }
        /// <summary>
        /// ["<c>isWithdrawalEnabled</c>"] Is withdrawing enabled for this asset
        /// </summary>
        [JsonPropertyName("isWithdrawalEnabled")]
        public bool IsWithdrawEnabled { get; set; }
        /// <summary>
        /// ["<c>isDepositEnabled</c>"] Is depositing enabled for this asset
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
        /// ["<c>chains</c>"] Networks
        /// </summary>
        [JsonPropertyName("chains")]
        public KucoinAssetNetwork[] Networks { get; set; } = Array.Empty<KucoinAssetNetwork>();
    }
}
