using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Result
    /// </summary>
    public record KucoinUaResult
    {
        /// <summary>
        /// ["<c>clientOid</c>"] Client oid
        /// </summary>
        [JsonPropertyName("clientOid")]
        public string? ClientOid { get; set; }
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
    }


}
