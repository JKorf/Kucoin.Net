namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Asset info
    /// </summary>
    public record KucoinUaAsset
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>name</c>"] Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fullName</c>"] Full name
        /// </summary>
        [JsonPropertyName("fullName")]
        public string FullName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>precision</c>"] Precision
        /// </summary>
        [JsonPropertyName("precision")]
        public int Precision { get; set; }
        /// <summary>
        /// ["<c>isMarginEnabled</c>"] Is margin enabled
        /// </summary>
        [JsonPropertyName("isMarginEnabled")]
        public bool? IsMarginEnabled { get; set; }
        /// <summary>
        /// ["<c>isDebitEnabled</c>"] Is debit enabled
        /// </summary>
        [JsonPropertyName("isDebitEnabled")]
        public bool? IsDebitEnabled { get; set; }
        /// <summary>
        /// ["<c>items</c>"] Networks
        /// </summary>
        [JsonPropertyName("items")]
        public KucoinUaAssetNetwork[] Networks { get; set; } = [];
        [JsonInclude(), JsonPropertyName("list")]
        internal KucoinUaAssetNetwork[] NetworksInt
        {
            set => Networks = value;
        }
    }

    /// <summary>
    /// Network info
    /// </summary>
    public record KucoinUaAssetNetwork
    {
        /// <summary>
        /// ["<c>chainName</c>"] Network name
        /// </summary>
        [JsonPropertyName("chainName")]
        public string NetworkName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>minWithdrawSize</c>"] Min withdraw quantity
        /// </summary>
        [JsonPropertyName("minWithdrawSize")]
        public decimal? MinWithdrawQuantity { get; set; }
        /// <summary>
        /// ["<c>minDepositSize</c>"] Min deposit quantity
        /// </summary>
        [JsonPropertyName("minDepositSize")]
        public decimal? MinDepositQuantity { get; set; }
        /// <summary>
        /// ["<c>withdrawFeeRate</c>"] Withdraw fee rate
        /// </summary>
        [JsonPropertyName("withdrawFeeRate")]
        public decimal? WithdrawFeeRate { get; set; }
        /// <summary>
        /// ["<c>minWithdrawFee</c>"] Min withdraw fee
        /// </summary>
        [JsonPropertyName("minWithdrawFee")]
        public decimal? MinWithdrawFee { get; set; }
        /// <summary>
        /// ["<c>isWithdrawEnabled</c>"] Is withdraw enabled
        /// </summary>
        [JsonPropertyName("isWithdrawEnabled")]
        public bool IsWithdrawEnabled { get; set; }
        /// <summary>
        /// ["<c>isDepositEnabled</c>"] Is deposit enabled
        /// </summary>
        [JsonPropertyName("isDepositEnabled")]
        public bool IsDepositEnabled { get; set; }
        /// <summary>
        /// ["<c>confirms</c>"] Confirms
        /// </summary>
        [JsonPropertyName("confirms")]
        public int Confirms { get; set; }
        /// <summary>
        /// ["<c>preConfirms</c>"] Pre confirms
        /// </summary>
        [JsonPropertyName("preConfirms")]
        public int? PreConfirms { get; set; }
        /// <summary>
        /// ["<c>contractAddress</c>"] Contract address
        /// </summary>
        [JsonPropertyName("contractAddress")]
        public string? ContractAddress { get; set; }
        /// <summary>
        /// ["<c>withdrawPrecision</c>"] Withdraw precision
        /// </summary>
        [JsonPropertyName("withdrawPrecision")]
        public int WithdrawPrecision { get; set; }
        /// <summary>
        /// ["<c>maxWithdrawSize</c>"] Max withdraw quantity
        /// </summary>
        [JsonPropertyName("maxWithdrawSize")]
        public decimal? MaxWithdrawQuantity { get; set; }
        /// <summary>
        /// ["<c>maxDepositSize</c>"] Max deposit quantity
        /// </summary>
        [JsonPropertyName("maxDepositSize")]
        public decimal? MaxDepositQuantity { get; set; }
        /// <summary>
        /// ["<c>isMemoRequired</c>"] Is memo required
        /// </summary>
        [JsonPropertyName("isMemoRequired")]
        public bool IsMemoRequired { get; set; }
        [JsonInclude, JsonPropertyName("needTag")]
        internal bool IsMemoRequiredInt
        {
            set => IsMemoRequired = value;
        }
        /// <summary>
        /// ["<c>chainId</c>"] Network id
        /// </summary>
        [JsonPropertyName("chainId")]
        public string NetworkId { get; set; } = string.Empty;
    }


}
