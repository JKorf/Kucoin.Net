using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Asset info
    /// </summary>
    public class KucoinAssetBase
    {
        /// <summary>
        /// The asset identifier
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The name of the asset
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The full name of the asset
        /// </summary>
        public string FullName { get; set; } = string.Empty;
        /// <summary>
        /// The precision of the asset
        /// </summary>
        public int Precision { get; set; }
        /// <summary>
        /// Number of block confirmations
        /// </summary>
        public int? Confirms { get; set; }
        /// <summary>
        /// Contract address
        /// </summary>
        public string ContractAddress { get; set; } = string.Empty;
        /// <summary>
        /// Is margin enabled
        /// </summary>
        public bool IsMarginEnabled { get; set; }
        /// <summary>
        /// Is debit enabled
        /// </summary>
        public bool IsDebitEnabled { get; set; }
    }

    /// <summary>
    /// Asset info
    /// </summary>
    public class KucoinAsset: KucoinAssetBase
    {        
        /// <summary>
        /// The minimum quantity of a withdrawal
        /// </summary>
        [JsonProperty("withdrawalMinSize")]
        public decimal WithdrawalMinQuantity { get; set; }
        /// <summary>
        /// The minimum fee of a withdrawal
        /// </summary>
        public decimal WithdrawalMinFee { get; set; }
        /// <summary>
        /// Is withdrawing enabled for this asset
        /// </summary>
        public bool IsWithdrawEnabled { get; set; }
        /// <summary>
        /// Is depositing enabled for this asset
        /// </summary>
        public bool IsDepositEnabled { get; set; }
    }

    /// <summary>
    /// Asset details
    /// </summary>
    public class KucoinAssetDetails: KucoinAssetBase
    {
        /// <summary>
        /// Networks
        /// </summary>
        [JsonProperty("chains")]
        public IEnumerable<KucoinAssetNetwork> Networks { get; set; } = Array.Empty<KucoinAssetNetwork>();
    }


    public class KucoinAssetApi
    {
        public decimal WithdrawMinFee { get; set; }
        public string ChainName { get; set; }
        public bool PreDepositTipEnabled { get; set; }
        [JsonProperty("chain")]
        public string Network { get; set; }
        public bool IsChainEnabled { get; set; }
        public string WithdrawDisabledTip { get; set; }
        public int WalletPrecision { get; set; }
        public string ChainFullName { get; set; }
        public string OrgAddress { get; set; }
        public bool IsDepositEnabled { get; set; }
        /// <summary>
        /// The minimum quantity of a withdrawal
        /// </summary>
        [JsonProperty("withdrawMinSize")]
        public decimal WithdrawMinQuantity { get; set; }

        public string DepositDisabledTip { get; set; }

        public string TxUrl { get; set; }

        public string UserAddressName { get; set; }

        public int ConfirmationCount { get; set; }

        public string WithdrawFeeRate { get; set; }

        /// <summary>
        /// The asset identifier
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;

        public bool IsWithdrawEnabled { get; set; }

        public string Status { get; set; }

    }

}
