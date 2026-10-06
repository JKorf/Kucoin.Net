using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Internal
{
    internal class KucoinUnifiedAuthResult
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        [JsonPropertyName("result")]
        public bool Result { get; set; }
        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}
