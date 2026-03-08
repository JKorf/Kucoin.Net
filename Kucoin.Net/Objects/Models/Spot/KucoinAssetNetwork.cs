namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Network details
    /// </summary>
    [SerializationModel]
    public record KucoinAssetNetwork
    {
        /// <summary>
        /// ["<c>chainId</c>"] Network id
        /// </summary>
        [JsonPropertyName("chainId")]
        public string NetworkId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>chainName</c>"] Network name
        /// </summary>
        [JsonPropertyName("chainName")]
        public string NetworkName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>withdrawalMinSize</c>"] Min withdrawal quantity
        /// </summary>
        [JsonPropertyName("withdrawalMinSize")]
        public decimal WithdrawalMinQuantity { get; set; }
        /// <summary>
        /// ["<c>withdrawalMinFee</c>"] Min withdrawal fee
        /// </summary>
        [JsonPropertyName("withdrawalMinFee")]
        public decimal WithdrawalMinFee { get; set; }
        /// <summary>
        /// ["<c>isWithdrawEnabled</c>"] Is withdrawing enabled
        /// </summary>
        [JsonPropertyName("isWithdrawEnabled")]
        public bool IsWithdrawEnabled { get; set; }
        /// <summary>
        /// ["<c>isDepositEnabled</c>"] Is deposit enabled
        /// </summary>
        [JsonPropertyName("isDepositEnabled")]
        public bool IsDepositEnabled { get; set; }
        /// <summary>
        /// ["<c>confirms</c>"] Confirmations needed on the network
        /// </summary>
        [JsonPropertyName("confirms")]
        public int? Confirms { get; set; }
        /// <summary>
        /// ["<c>preConfirms</c>"] The number of blocks (confirmations) for advance on-chain verification
        /// </summary>
        [JsonPropertyName("preConfirms")]
        public int? Preconfirms { get; set; }
        /// <summary>
        /// ["<c>contractAddress</c>"] Contract address
        /// </summary>
        [JsonPropertyName("contractAddress")]
        public string ContractAddress { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>depositFeeRate</c>"] Deposit fee rate
        /// </summary>
        [JsonPropertyName("depositFeeRate")]
        public decimal? DepositFeeRate { get; set; }
        /// <summary>
        /// ["<c>depositMinSize</c>"] Min deposit quantity
        /// </summary>
        [JsonPropertyName("depositMinSize")]
        public decimal? DepositMinQuantity { get; set; }
        /// <summary>
        /// ["<c>withdrawFeeRate</c>"] Withdrawal fee rate
        /// </summary>
        [JsonPropertyName("withdrawFeeRate")]
        public decimal? WithdrawFeeRate { get; set; }
        /// <summary>
        /// ["<c>withdrawMaxFee</c>"] Withdraw max fee
        /// </summary>
        [JsonPropertyName("withdrawMaxFee")]
        public decimal? WithdrawMaxFee { get; set; }
        /// <summary>
        /// ["<c>maxDeposit</c>"] Max deposit quantity (only for lightning network)
        /// </summary>
        [JsonPropertyName("maxDeposit")]
        public decimal? MaxDeposit { get; set; }
        /// <summary>
        /// ["<c>maxWithdraw</c>"] Maximum amount of single withdrawal
        /// </summary>
        [JsonPropertyName("maxWithdraw")]
        public decimal? MaxWithdraw { get; set; }
        /// <summary>
        /// ["<c>needTag</c>"] Needs a tag
        /// </summary>
        [JsonPropertyName("needTag")]
        public bool? NeedTag { get; set; }
        /// <summary>
        /// ["<c>withdrawPrecision</c>"] Maximum withdraw precision
        /// </summary>
        [JsonPropertyName("withdrawPrecision")]
        public decimal? WithdrawPrecision { get; set; }

    }
}
