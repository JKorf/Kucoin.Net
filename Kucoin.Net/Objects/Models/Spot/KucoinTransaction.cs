using System;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Converters;
using Kucoin.Net.Enums;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models
{
    /// <summary>
    /// Common transaction info
    /// </summary>
    public class KucoinTransactionBase
    {
        /// <summary>
        /// The quantity as a quoted decimal
        /// </summary>
        [JsonProperty("amount")]
        internal object quantity { get; set; } = 0;
        /// <summary>
        /// The quantity as a decimal
        /// </summary>
        public decimal Quantity
        {
            get
            {
                if (quantity is string)
                    quantity = decimal.Parse((string)quantity);

                return (decimal)quantity;
            }
        }
        /// <summary>
        /// The asset of the transaction
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; } = string.Empty;
        /// <summary>
        /// The status of the transaction
        /// </summary>
        [JsonConverter(typeof(TransactionStatusConverter))]
        public TransactionStatus Status { get; set; }
        /// <summary>
        /// The wallet transaction id
        /// </summary>
        [JsonProperty("walletTxId")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// The time the transaction was created
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("createdAt")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Whether this is an internal transaction
        /// </summary>
        public bool IsInner { get; set; }
    }
    /// <summary>
    /// Transaction info
    /// </summary>
    public class KucoinTransaction : KucoinTransactionBase
    {
        /// <summary>
        /// The wallet address
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// A memo for this transaction
        /// </summary>
        [JsonProperty("memo")]
        public string Memo { get; set; } = string.Empty;
        /// <summary>
        /// The fee as a decimal
        /// </summary>
        [JsonProperty("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// A remark for this transaction
        /// </summary>
        [JsonProperty("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// When the transaction was last updated
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("updatedAt")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// The id of the withdrawal
        /// </summary>
        [JsonProperty("withdrawalId")]
        public string? WithdrawalId { get; set; } = string.Empty;
        ///// <summary>
        ///// Reason if failed
        ///// </summary>
        //[JsonProperty("reason")]
        //public string? Reason { get; set; } = string.Empty;
    }
}
