using System;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Converters;
using Kucoin.Net.Enums;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Transfer info
    /// </summary>
    public class KucoinTransfer : KucoinTransactionBase
    {
        /// <summary>
        /// Apply id
        /// </summary>
        [JsonProperty("applyId")]
        public new string Id => base.Id;
        //public string ApplyId { get; set; } = string.Empty;
        /// <summary>
        /// Offset
        /// </summary>
        [JsonProperty("offset")]
        public long Offset { get; set; }
    }
}
