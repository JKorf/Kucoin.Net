

using CryptoExchange.Net.Objects;
using Kucoin.Net.Enums;

namespace Kucoin.Net.Objects.Internal
{
    internal class KucoinUnifiedOpRequest
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("op")]
        public string Operation { get; set; }
        [JsonPropertyName("args")]
        public Parameters Parameters { get; set; }
        
        public KucoinUnifiedOpRequest(string id, string op, Parameters parameters)
        {
            Id = id;
            Operation = op;
            Parameters = parameters;
        }
    }
}
