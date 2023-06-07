using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Network details
    /// </summary>
    public class KucoinAssetNetwork
    {
        /// <summary>
        /// Network name
        /// </summary>
        [JsonProperty("chainName")]
        public string NetworkName { get; set; } = string.Empty;
        
        
         /// <summary>
        /// Network name
        /// </summary>
        [JsonProperty("chain")]
        public string Network { get; set; } = string.Empty;

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
        /// Contract address
        /// </summary>
        public string ContractAddress { get; set; } = string.Empty;
    }
}
