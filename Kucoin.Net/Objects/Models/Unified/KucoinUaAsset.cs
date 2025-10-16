using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Asset info
    /// </summary>
    public record KucoinUaAsset
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Full name
        /// </summary>
        [JsonPropertyName("fullName")]
        public string FullName { get; set; } = string.Empty;
        /// <summary>
        /// Precision
        /// </summary>
        [JsonPropertyName("precision")]
        public int Precision { get; set; }
        /// <summary>
        /// Confirms
        /// </summary>
        [JsonPropertyName("confirms")]
        public int? Confirms { get; set; }
        /// <summary>
        /// Contract address
        /// </summary>
        [JsonPropertyName("contractAddress")]
        public string? ContractAddress { get; set; }
        /// <summary>
        /// Networks
        /// </summary>
        [JsonPropertyName("list")]
        public KucoinUaAssetNetwork[] Networks { get; set; } = [];
    }

    /// <summary>
    /// Network info
    /// </summary>
    public record KucoinUaAssetNetwork
    {
        /// <summary>
        /// Network name
        /// </summary>
        [JsonPropertyName("chainName")]
        public string NetworkName { get; set; } = string.Empty;
        /// <summary>
        /// Min withdraw quantity
        /// </summary>
        [JsonPropertyName("minWithdrawSize")]
        public decimal? MinWithdrawQuantity { get; set; }
        /// <summary>
        /// Min deposit quantity
        /// </summary>
        [JsonPropertyName("minDepositSize")]
        public decimal? MinDepositQuantity { get; set; }
        /// <summary>
        /// Withdraw fee rate
        /// </summary>
        [JsonPropertyName("withdrawFeeRate")]
        public decimal? WithdrawFeeRate { get; set; }
        /// <summary>
        /// Min withdraw fee
        /// </summary>
        [JsonPropertyName("minWithdrawFee")]
        public decimal? MinWithdrawFee { get; set; }
        /// <summary>
        /// Is withdraw enabled
        /// </summary>
        [JsonPropertyName("isWithdrawEnabled")]
        public bool IsWithdrawEnabled { get; set; }
        /// <summary>
        /// Is deposit enabled
        /// </summary>
        [JsonPropertyName("isDepositEnabled")]
        public bool IsDepositEnabled { get; set; }
        /// <summary>
        /// Confirms
        /// </summary>
        [JsonPropertyName("confirms")]
        public int Confirms { get; set; }
        /// <summary>
        /// Pre confirms
        /// </summary>
        [JsonPropertyName("preConfirms")]
        public int? PreConfirms { get; set; }
        /// <summary>
        /// Contract address
        /// </summary>
        [JsonPropertyName("contractAddress")]
        public string? ContractAddress { get; set; }
        /// <summary>
        /// Withdraw precision
        /// </summary>
        [JsonPropertyName("withdrawPrecision")]
        public int WithdrawPrecision { get; set; }
        /// <summary>
        /// Max withdraw quantity
        /// </summary>
        [JsonPropertyName("maxWithdrawSize")]
        public decimal? MaxWithdrawQuantity { get; set; }
        /// <summary>
        /// Max deposit quantity
        /// </summary>
        [JsonPropertyName("maxDepositSize")]
        public decimal? MaxDepositQuantity { get; set; }
        /// <summary>
        /// Need tag
        /// </summary>
        [JsonPropertyName("needTag")]
        public bool NeedTag { get; set; }
        /// <summary>
        /// Network id
        /// </summary>
        [JsonPropertyName("chainId")]
        public string NetworkId { get; set; } = string.Empty;
    }


}
