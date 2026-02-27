using Kucoin.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Account mode
    /// </summary>
    public record KucoinUaAccountMode
    {
        /// <summary>
        /// Self account mode
        /// </summary>
        [JsonPropertyName("selfAccountMode")]
        public UnifiedAccountMode SelfAccountMode { get; set; }
        /// <summary>
        /// Unified sub accounts
        /// </summary>
        [JsonPropertyName("unifiedSubAccount")]
        public long[] UnifiedSubAccounts { get; set; } = [];
        /// <summary>
        /// Classic sub accounts
        /// </summary>
        [JsonPropertyName("classicSubAccount")]
        public long[] ClassicSubAccounts { get; set; } = [];
    }


}
