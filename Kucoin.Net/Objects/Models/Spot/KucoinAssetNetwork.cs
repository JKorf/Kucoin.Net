using CryptoExchange.Net.Converters.SystemTextJson;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Network details
    /// </summary>
    [SerializationModel]
    public record KucoinAssetNetwork
    {
        /// <summary>
        /// Network id
        /// </summary>
        [JsonPropertyName("chainId")]
        public string NetworkId { get; set; } = string.Empty;
        /// <summary>
        /// Network name
        /// </summary>
        [JsonPropertyName("chainName")]
        public string NetworkName { get; set; } = string.Empty;
        /// <summary>
        /// Min withdrawal quantity
        /// </summary>
        [JsonPropertyName("withdrawalMinSize")]
        public decimal WithdrawalMinQuantity { get; set; }
        /// <summary>
        /// Min withdrawal fee
        /// </summary>
        [JsonPropertyName("withdrawalMinFee")]
        public decimal WithdrawalMinFee { get; set; }
        /// <summary>
        /// Is withdrawing enabled
        /// </summary>
        [JsonPropertyName("isWithdrawEnabled")]
        public bool IsWithdrawEnabled { get; set; }
        /// <summary>
        /// Is deposit enabled
        /// </summary>
        [JsonPropertyName("isDepositEnabled")]
        public bool IsDepositEnabled { get; set; }
        /// <summary>
        /// Confirmations needed on the network
        /// </summary>
        [JsonPropertyName("confirms")]
        public int? Confirms { get; set; }
        /// <summary>
        /// The number of blocks (confirmations) for advance on-chain verification
        /// </summary>
        [JsonPropertyName("preConfirms")]
        public int? Preconfirms { get; set; }
        /// <summary>
        /// Contract address
        /// </summary>
        [JsonPropertyName("contractAddress")]
        public string ContractAddress { get; set; } = string.Empty;
        /// <summary>
        /// Deposit fee rate
        /// </summary>
        [JsonPropertyName("depositFeeRate")]
        public decimal? DepositFeeRate { get; set; }
        /// <summary>
        /// Min deposit quantity
        /// </summary>
        [JsonPropertyName("depositMinSize")]
        public decimal? DepositMinQuantity { get; set; }
        /// <summary>
        /// Withdrawal fee rate
        /// </summary>
        [JsonPropertyName("withdrawFeeRate")]
        public decimal? WithdrawFeeRate { get; set; }
        /// <summary>
        /// Withdraw max fee
        /// </summary>
        [JsonPropertyName("withdrawMaxFee")]
        public decimal? WithdrawMaxFee { get; set; }
        /// <summary>
        /// Max deposit quantity (only for lightning network)
        /// </summary>
        [JsonPropertyName("maxDeposit")]
        public decimal? MaxDeposit { get; set; }
        /// <summary>
        /// Maximum amount of single withdrawal
        /// </summary>
        [JsonPropertyName("maxWithdraw")]
        public decimal? MaxWithdraw { get; set; }
        /// <summary>
        /// Needs a tag
        /// </summary>
        [JsonPropertyName("needTag")]
        public bool? NeedTag { get; set; }
        /// <summary>
        /// Maximum withdraw precision
        /// </summary>
        [JsonPropertyName("withdrawPrecision")]
        public decimal? WithdrawPrecision { get; set; }

    }
}
