using Kucoin.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Models.Unified
{
    internal record KucoinUaResponse<T>
    {
        [JsonPropertyName("tradeType")]
        public ProductType ProductType { get; set; }
        [JsonPropertyName("list")]
        public T Data { get; set; } = default!;
    }
}
