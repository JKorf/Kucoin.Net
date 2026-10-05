

using CryptoExchange.Net.Objects;
using Kucoin.Net.Enums;

namespace Kucoin.Net.Objects.Internal
{
    internal class KucoinUnifiedAuthRequest
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        [JsonPropertyName("op")]
        public string Operation { get; set; } = string.Empty;
        [JsonPropertyName("kc-api-key")]
        public string ApiKey { get; set; } = string.Empty;
        [JsonPropertyName("kc-api-timestamp")]
        public long Timestamp { get; set; }
        [JsonPropertyName("kc-api-sign")]
        public string Sign { get; set; } = string.Empty;
        [JsonPropertyName("kc-api-passphrase")]
        public string Passphrase { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault), JsonPropertyName("kc-api-partner")]
        public string? Partner { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault), JsonPropertyName("kc-api-partner-sign")]
        public string? PartnerSign { get; set; }
    }
}
