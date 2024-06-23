using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Network details
    /// </summary>
    public record KucoinAssetNetwork
    {
        /// <summary>
        /// Network id
        /// </summary>
        [JsonProperty("chainId")]
        public string NetworkId { get; set; } = string.Empty;
        /// <summary>
        /// Network name
        /// </summary>
        [JsonProperty("chainName")]
        public string NetworkName { get; set; } = string.Empty;
        /// <summary>
        /// Min withdrawal quantity
        /// </summary>
        [JsonProperty("withdrawalMinSize")]
        public decimal WithdrawalMinQuantity { get; set; }
        /// <summary>
        /// Min withdrawal fee
        /// </summary>
        public decimal WithdrawalMinFee { get; set; }
        /// <summary>
        /// Is withdrawing enabled
        /// </summary>
        public bool IsWithdrawEnabled { get; set; }
        /// <summary>
        /// Is deposit enabled
        /// </summary>
        public bool IsDepositEnabled { get; set; }
        /// <summary>
        /// Confirmations needed on the network
        /// </summary>
        public int? Confirms { get; set; }
        /// <summary>
        /// The number of blocks (confirmations) for advance on-chain verification
        /// </summary>
        public int? Preconfirms { get; set; }
        /// <summary>
        /// Contract address
        /// </summary>
        public string ContractAddress { get; set; } = string.Empty;
        /// <summary>
        /// Deposit fee rate
        /// </summary>
        public decimal? DepositFeeRate { get; set; }
        /// <summary>
        /// Min deposit quantity
        /// </summary>
        [JsonProperty("depositMinSize")]
        public decimal? DepositMinQuantity { get; set; }
        /// <summary>
        /// Withdrawal fee rate
        /// </summary>
        [JsonProperty("withdrawFeeRate")]
        public decimal? WithdrawFeeRate { get; set; }
        /// <summary>
        /// Withdraw max fee
        /// </summary>
        [JsonProperty("withdrawMaxFee")]
        public decimal? WithdrawMaxFee { get; set; }

    }
}
